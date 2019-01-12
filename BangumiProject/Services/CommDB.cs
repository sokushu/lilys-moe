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
    public class CommDB : ICommDB
    {
        public bool AutoCache { set; get; }
        private readonly IMemoryCache _memoryCache;
        private readonly BangumiProjectContext _db;
        private readonly static HashSet<KEY> hashSet = new HashSet<KEY>();
        private readonly static HashSet<int> AnimeIDs = new HashSet<int>();
        private static bool AnimeIDADD = false;
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
            //开始加载所有的现存的动画ID
            if (AnimeIDADD == false)
            {
                AnimeIDADD = true;
                _db.Anime.Select(a => a.AnimeID).ToList().ForEach(id => 
                {
                    AnimeIDs.Add(id);
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual async Task AddAsync<T>(T t)where T : class
        {
            await _db.AddAsync(t);
            await _db.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void DBUpdate<T>(IEnumerable<char[]> Keys) where T : class
        {
            foreach (var key in Keys)
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
        public virtual void SetCache<T>(KEY kEY, T Value)
        {
            hashSet.Add(kEY);
            _memoryCache.Set(kEY.Key, Value);
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
        public virtual bool GetDate<T>(KEY kEY, out T t)
        {
            bool Return = false;
            if (!(Return = _memoryCache.TryGetValue(kEY.Key, out t)))
            {
                //已经赋值返回True（赋值Null）
                return hashSet.Contains(kEY);
            }
            return Return;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(KEY key)
        {
            _memoryCache.Remove(key.Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void AddAnimeID(int id)
        {
            AnimeIDs.Add(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool HasAnimeID(int id)
        {
            return AnimeIDs.Contains(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void RemoveAnimeID(int id)
        {
            AnimeIDs.Remove(id);
        }

        public async Task AddAsyncNoSave<T>(T t) where T : class
        {
            await _db.AddAsync(t);
        }

        public async Task<T> GetLastAsync<T>() where T : class
        {
            return await _db.Set<T>().LastAsync();
        }
    }
    public struct KEY
    {
        public char[] Key { get; set; }
        public CacheType Type { get; set; }
        /// <summary>
        /// 被调用的次数
        /// </summary>
        public int I { get; set; }
    }
    public enum CacheType
    {
        AnimeOne,
        BlogOne,
        Other,
        Test,
    }
}
