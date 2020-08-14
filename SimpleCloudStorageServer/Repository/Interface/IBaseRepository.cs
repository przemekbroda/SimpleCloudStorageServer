using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleCloudStorageServer.Repository
{
    public interface IBaseRepository<T>
    {
        public Task AddAsync(T entity);
        public Task<T> GetByIdAsync(long id);
        public Task<List<T>> GetAllAsList();
        public void Remove(T entity);
        public Task<bool> SaveChanges();
        
    }
}
