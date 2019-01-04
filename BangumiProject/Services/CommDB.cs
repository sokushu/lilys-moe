using BangumiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services
{
    public abstract class CommDB : ICommDB
    {
        public bool AutoCache { set; get; }
        private readonly IMemoryCache _memoryCache;
        private readonly BangumiProjectContext _db;
        private readonly HashSet<char[]> hashSet = new HashSet<char[]>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_memoryCache"></param>
        /// <param name="_db"></param>
        /// <param name="_authorizationService"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_userManager"></param>
        public CommDB(
            IMemoryCache _memoryCache, 
            BangumiProjectContext _db
            )
        {
            this._db = _db;
            this._memoryCache = _memoryCache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void DBUpdate<T>() where T : class
        {
            foreach (var key in hashSet)
            {
                if (_memoryCache.TryGetValue(key, out T t))
                {
                    _db.Update(t);
                }
            }
            _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="Value"></param>
        public virtual void SetCache<T>(char[] key, T Value)
        {
            hashSet.Add(key);
            _memoryCache.Set(key, Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual async Task<List<T>> GetDateToListAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            return await func.Invoke(_db.Set<T>()).ToListAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<T> GetDateOneAsync<T>(Func<DbSet<T>, IQueryable<T>> func)where T : class
        {
            return await func.Invoke(_db.Set<T>()).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual async Task<T> GetFirstAsync<T>(Expression<Func<T, bool>> func) where T : class
        {
            return await _db.Set<T>().FirstOrDefaultAsync(func);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public virtual async Task<T[]> GetDateToArrayAsync<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            return await func.Invoke(_db.Set<T>()).ToArrayAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync<T>(T t) where T : class
        {
            _db.Update(t);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task UpdateAsync<T>(IEnumerable<T> t) where T : class
        {
            _db.Set<T>().UpdateRange(t);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual bool GetDate<T>(char[] key, out T t)
        {
            return _memoryCache.TryGetValue(key, out t);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(char[] key)
        {
            _memoryCache.Remove(key);
        }
    }
}
