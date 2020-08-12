using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Service
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user, DateTime expireAt);
        string GenerateRefreshToken(User user, DateTime expireAt);
        bool ValidateRefreshToken(string refreshToken, out int id);
    }
}
