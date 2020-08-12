using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SimpleCloudStorageServer.Helper.Exceptions;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public class FileService : IFileService
    {

        private readonly string _storagePath;

        public FileService(IConfiguration configuration)
        {
            _storagePath = configuration.GetValue<string>("DefaultStoragePath");
        }

        public void CreateDirectory(string directoryName)
        {
            var path = $"{_storagePath}\\{directoryName}"; 

            if (Directory.Exists(path))
            {
                throw new DirectoryAlreadyExistsException("directory already exists");
            }

            Directory.CreateDirectory(path);
        }

        public void DeleteFile(string path)
        {

        }

        public async Task<byte[]> GetFile(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            return await File.ReadAllBytesAsync(path);
        }

        public void RemoveDirectory(string directoryName)
        {
            var path = $"${_storagePath}/${directoryName}";

            if (!Directory.Exists(directoryName))
            {
                throw new DirectoryNotFoundException("Directory has not been found");
            }

            Directory.Delete(directoryName);
        }

        public async Task<string> SaveFile(string directory, string fileName, IFormFile formFile)
        {
            var filePath = Path.Combine(_storagePath, directory, fileName + Path.GetExtension(formFile.FileName));

            if (File.Exists(filePath))
            {
                throw new FileAlreadyExistsException();
            }

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return filePath;
        }

    }
}
