using Microsoft.EntityFrameworkCore;
using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<User> GetUserByAppId(string appId)
        {
            return await _context
                .Users
                .Where(u => u.AppId == appId)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByMainDirectory(string userDirectory)
        {
            return await _context
                .Users
                .Where(u => u.MainDirectory == userDirectory)
                .FirstOrDefaultAsync();
        }

        public async Task<User> GetUserByUsername(string username)
        {
            return await _context
                .Users
                .Where(u => u.Username.ToLower() == username.ToLower())
                .FirstOrDefaultAsync();
        }




    }
}
