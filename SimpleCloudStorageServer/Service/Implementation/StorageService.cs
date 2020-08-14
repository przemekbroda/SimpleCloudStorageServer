using AutoMapper;
using Microsoft.AspNetCore.Http;
using SimpleCloudStorageServer.Dto;
using SimpleCloudStorageServer.Helper;
using SimpleCloudStorageServer.Helper.Exceptions;
using SimpleCloudStorageServer.Model;
using SimpleCloudStorageServer.Repository;
using SimpleCloudStorageServer.Service.Implementation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public class StorageService : IStorageService
    {

        private readonly IFileService _fileService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileRepository _fileRepository;
        private readonly IFileCachedService _fileCachedService;

        public StorageService(IFileService fileService, IUserRepository userRepository, IMapper mapper, IFileRepository fileRepository, IFileCachedService fileCachedService) 
        {
            _fileService = fileService;
            _userRepository = userRepository;
            _mapper = mapper;
            _fileRepository = fileRepository;
            _fileCachedService = fileCachedService;
        }

        public async Task<Tuple<byte[], string>> GetFile(string appId, string fileName)
        {
            var file = await _fileCachedService.GetFile(appId, fileName);

            if (file == null || file.IsPrivate)
            {
                throw new NotFoundException("Such file has not been found");
            }

            return new Tuple<byte[], string>(await _fileService.GetFile(file.FilePath), file.FileName);
        }

        public async Task<FileForUploadDto> RemoveFile(long userId, string fileName)
        {
            var file = await _fileRepository.GetFileByUserIdAndFileName(userId, fileName);

            if (file == null)
            {
                throw new FileNotFoundException();
            }

            _fileService.DeleteFile(file.FilePath);

            _fileRepository.Remove(file);

            await _fileRepository.SaveChanges();

            _fileCachedService.RemoveFile(file.User.AppId, fileName);

            return _mapper.Map<FileForUploadDto>(file);
        }

        public async Task<object> UpdateFile(long userId, string fileName, FileForUpdateDto updateDto)
        {
            var file = await _fileRepository.GetFileByUserIdAndFileName(userId, fileName);

            if (file == null)
            {
                throw new FileNotFoundException();
            }

            _mapper.Map(updateDto, file);

            await _fileRepository.SaveChanges();

            _fileCachedService.SetFile(file.User.AppId, fileName, file);

            return _mapper.Map<FileForUploadDto>(file);
        }

        public async Task<FileForUploadDto> UploadFile(IFormFile file, long userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);

            if (user == null)
            {
                throw new BusinessException("Could not find a user");
            }

            if (file.Length <= 0)
            {
                throw new BusinessException("File is empty");
            }

            string filePath = "";

            while (true)
            {
                try
                {
                    filePath = await _fileService.SaveFile(user.MainDirectory, RandomStringUtils.GenerateRandomString(), file);
                    break;
                }
                catch (FileAlreadyExistsException) { }
                catch (Exception e)
                {
                    throw e;
                }
            }

            var fileRecord = new Model.File
            {
                FilePath = filePath,
                AddedAt = DateTime.UtcNow,
                IsPrivate = false,
                FileName = Path.GetFileName(filePath),
                User = user,
            };

            fileRecord.UrlPath = $"http://localhost:5000/storage/{user.AppId}/{fileRecord.FileName}";

            await _fileRepository.AddAsync(fileRecord);

            await _fileRepository.SaveChanges();

            _fileCachedService.SetFile(user.AppId, fileRecord.FileName, fileRecord);

            return _mapper.Map<FileForUploadDto>(fileRecord);
        }


    }
}
