using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Dto
{
    public class UserForDetailsDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string AppId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletionAt { get; set; }
    }
}
