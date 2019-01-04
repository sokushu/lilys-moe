using BangumiProject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services
{
    public interface ICommDB
    {
        bool AutoCache { set; get; }

        Task<List<T>> GetDateToListAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;
        void SetCache<T>(char[] key, T Value);
        bool GetDate<T>(char[] key, out T t);
        void DBUpdate<T>() where T : class;
        void Remove(char[] key);
        Task SaveChangesAsync();
        Task UpdateAsync<T>(IEnumerable<T> t) where T : class;
        Task UpdateAsync<T>(T t) where T : class;
        Task<T[]> GetDateToArrayAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;
        Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> func) where T : class;
        Task<T> GetDateOneAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;

    }
}
