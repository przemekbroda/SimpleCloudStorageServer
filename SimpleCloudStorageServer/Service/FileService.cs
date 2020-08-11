using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SimpleCloudStorageServer.Helper.Exceptions;
using System;
using System.IO;

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
            var path = $"${_storagePath}/${directoryName}"; 

            if (Directory.Exists(path))
            {
                throw new DirectoryAlreadyExistsException("directory already exists");
            }

            Directory.CreateDirectory(path);
        }

        public void DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public void GetFile(string path)
        {
            throw new NotImplementedException();
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

        public void SaveFile(string path, string fileName, IFormFile formFile)
        {
            throw new NotImplementedException();
        }

    }
}
