using SimpleCloudStorageServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public interface IUserService
    {
        public Task<UserForRegisterResultDto> CreateUser(UserForRegisterDto detailsDto);
        public Task<UserForDetailsDto> GetUser(long userId);
        public Task<UserForDeleteDto> ApplyDeleteActionToUser(long userId);
        public Task<LoginResultDto> Login(LoginDto loginDto);
    }
}
