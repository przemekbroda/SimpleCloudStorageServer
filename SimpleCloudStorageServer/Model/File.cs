using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Model
{
    [Table("file")]
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string UrlPath { get; set; }
        public bool IsPrivate { get; set; }
        public DateTime AddedAt { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
    }
}
