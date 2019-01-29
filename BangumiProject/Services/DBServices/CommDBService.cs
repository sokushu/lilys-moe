using BangumiProject.Models;
using BangumiProject.Services.DBServices.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices
{
    public abstract class CommDBService : ICommDB, IDCache
    {
        /// <summary>
        /// 
        /// </summary>
        public bool AutoCache { get; set; } = true;
        private static bool AnimeIDADD = false;
        /// <summary>
        /// 
        /// </summary>
        private static HashSet<int> CacheIDH { get; set; } = new HashSet<int>();
        private readonly IMemoryCache _memoryCache;
        private readonly BangumiProjectContext _db;

        public CommDBService(
            IMemoryCache _memoryCache,
            BangumiProjectContext _db
            )
        {
            this._memoryCache = _memoryCache;
            this._db = _db;
            //开始加载所有的现存的动画ID
            if (AnimeIDADD == false)
            {
                lock (this)
                {
                    if (AnimeIDADD == false)
                    {
                        AnimeIDADD = true;
                        _db.Anime.Select(a => a.AnimeID).ToList().ForEach(id =>
                        {
                            CacheIDH.Add(id);
                        });
                    }
                }
            }
        }

        public Task<List<T>> GetDateToListAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(KEY kEY, T Value)
        {
            throw new NotImplementedException();
        }

        public bool GetDate<T>(KEY kEY, out T t)
        {
            throw new NotImplementedException();
        }

        public void DBUpdate<T>(IEnumerable<char[]> Keys) where T : class
        {
            throw new NotImplementedException();
        }

        public void Remove(KEY kEY)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
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

        public Task<T[]> GetDateToArrayAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetDateOneAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            throw new NotImplementedException();
        }

        public bool HasAnimeID(int id)
        {
            throw new NotImplementedException();
        }

        public void AddAnimeID(int id)
        {
            throw new NotImplementedException();
        }

        public void RemoveAnimeID(int id)
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

        public Task<T> GetLastAsync<T>() where T : class
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromDBAsync<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public void RemoveFormDB<T>(T t) where T : class
        {
            throw new NotImplementedException();
        }

        public void CacheID(int ID)
        {
            throw new NotImplementedException();
        }

        public bool HasID(int ID)
        {
            throw new NotImplementedException();
        }

        public void RemoveID(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
