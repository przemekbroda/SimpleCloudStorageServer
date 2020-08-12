﻿using Microsoft.AspNetCore.Http;
using SimpleCloudStorageServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public interface IStorageService
    {
        public Task<Tuple<byte[], string>> GetFile(string appId, string fileName);
        public Task<FileForUploadDto> UploadFile(IFormFile file, long userId);
        public void RemoveFile();
    }
}
