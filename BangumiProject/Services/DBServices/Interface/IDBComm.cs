using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface IDBComm
    {
        bool AutoCache { set; get; }
        Task<List<T>> GetDateToListAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;
        void SetCache<T>(KEY kEY, T Value);
        bool GetDate<T>(KEY kEY, out T t);
        void DBUpdate<T>(IEnumerable<char[]> Keys) where T : class;
        void Remove(KEY kEY);
        Task SaveChangesAsync();
        Task UpdateAsync<T>(IEnumerable<T> t) where T : class;
        Task UpdateAsync<T>(T t) where T : class;
        Task<T[]> GetDateToArrayAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> func) where T : class;
        Task<T> GetDateOneAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;
        bool HasAnimeID(int id);
        void AddAnimeID(int id);
        void RemoveAnimeID(int id);
        Task AddAsync<T>(T t) where T : class;
        Task AddAsyncNoSave<T>(T t) where T : class;
        Task<T> GetLastAsync<T>() where T : class;
        Task RemoveFromDBAsync<T>(T t) where T : class;
        void RemoveFormDB<T>(T t) where T : class;
    }
}
