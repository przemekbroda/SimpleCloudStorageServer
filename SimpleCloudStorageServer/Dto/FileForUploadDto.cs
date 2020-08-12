using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Dto
{
    public class FileForUploadDto
    {
        public string FileName { get; set; }
        public string UrlPath { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
