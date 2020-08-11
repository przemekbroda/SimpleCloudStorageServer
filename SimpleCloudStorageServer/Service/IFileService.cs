using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public interface IFileService
    {
        void CreateDirectory(string directoryName);
        void RemoveDirectory(string directoryName);
        void SaveFile(string path, string fileName, IFormFile formFile);
        void DeleteFile(string path);
        void GetFile(string path);
    }
}
