using BangumiProject.Services.DBServices.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices
{
    public abstract class CommDBService : IDBComm
    {
        public bool AutoCache { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void AddAnimeID(int id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public Task AddAsyncNoSave<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public void DBUpdate<T>(IEnumerable<char[]> Keys) where T : class
        {
            throw new NotImplementedException();
        }

        public bool GetDate<T>(KEY kEY, out T t)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDateOneAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T[]> GetDateToArrayAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<List<T>> GetDateToListAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetLastAsync<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public bool HasAnimeID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(KEY kEY)
        {
            throw new NotImplementedException();
        }

        public void RemoveAnimeID(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveFormDB<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromDBAsync<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(KEY kEY, T Value)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(IEnumerable<T> t) where T : class
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
