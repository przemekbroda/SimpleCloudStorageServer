using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Model
{
    [Table("user")]
    public class User : BaseEntity
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] ApiKeyHash { get; set; }
        public byte[] ApiKeySalt { get; set; }
        public string AppId { get; set; }
        public string MainDirectory { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletionAt { get; set; }
        public ICollection<File> Files { get; set; }

    }
}
