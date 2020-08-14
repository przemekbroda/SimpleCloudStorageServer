using Microsoft.EntityFrameworkCore;
using SimpleCloudStorageServer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public class FileRepository : BaseRepository<File>, IFileRepository
    {

        private DataContext _context;

        public FileRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<File> GetFileByUserAppIdAndFileName(string appId, string fileName)
        {
            return await _context
                .Files
                .Where(f => f.FileName == fileName && f.User.AppId == appId)
                .FirstOrDefaultAsync();
        }

        public async Task<File> GetFileByUserIdAndFileName(long userId, string fileName)
        {
            return await _context
                .Files
                .Include(f => f.User)
                .Where(f => f.UserId == userId && f.FileName == fileName)
                .FirstOrDefaultAsync();
        }
    }
}
