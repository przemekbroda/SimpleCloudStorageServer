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
            var path = Path.Combine(_storagePath, directoryName);

            if (Directory.Exists(path))
            {
                throw new DirectoryAlreadyExistsException("directory already exists");
            }

            Directory.CreateDirectory(path);
        }

        public void DeleteFile(string path)
        {
            var fullPath = Path.Combine(_storagePath, path);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<byte[]> GetFile(string path)
        {
            var fullPath = Path.Combine(_storagePath, path); 

            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException();
            }

            return await File.ReadAllBytesAsync(fullPath);
        }

        public void RemoveDirectory(string directoryName)
        {
            var path = Path.Combine(_storagePath, directoryName);

            if (!Directory.Exists(path))
            {
                throw new DirectoryNotFoundException("Directory has not been found");
            }

            Directory.Delete(path);
        }

        public async Task<string> SaveFile(string directory, string fileName, IFormFile formFile)
        {
            var shortPath = Path.Combine(directory, fileName + Path.GetExtension(formFile.FileName));

            var fullPath = Path.Combine(_storagePath, shortPath);

            if (File.Exists(fullPath))
            {
                throw new FileAlreadyExistsException();
            }

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }

            return shortPath;
        }

    }
}
