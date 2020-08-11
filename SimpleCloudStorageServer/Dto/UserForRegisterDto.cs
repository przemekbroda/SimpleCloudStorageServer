using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        [StringLength(24, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        [StringLength(36, MinimumLength = 8)]
        public string Password { get; set; }

    }
}
