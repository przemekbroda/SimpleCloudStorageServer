using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Dto
{
    public class UserForDeleteDto
    {
        public long Id { get; set; }
        public DateTime DeletionAt { get; set; }
    }
}
