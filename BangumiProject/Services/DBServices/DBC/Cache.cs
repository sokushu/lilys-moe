using BangumiProject.Models;
using BangumiProject.Services.DBServices.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.DBC
{
    /// <summary>
    /// 
    /// </summary>
    public class Cache : ICache
    {
        private string Key { get; set; }
        private readonly IMemoryCache _memoryCache;
        private static HashSet<string> Keys { set; get; } = new HashSet<string>();

        public BangumiProjectContext _db { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_db"></param>
        public Cache(string Key, IMemoryCache _memoryCache, BangumiProjectContext _db)
        {
            this.Key = Key;
            this._memoryCache = _memoryCache;
            this._db = _db;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public ICache BuildKey(string Key)
        {
            return new Cache(Key, _memoryCache, _db);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Data"></typeparam>
        /// <returns></returns>
        public IReadCacheFormDB<Data> GetCache<Data>() where Data : class
        {
            if (!_memoryCache.TryGetValue(Key, out Data data))
            {
                return new CacheFormDB<Data>(Keys.Contains(Key), data, this);
            }
            return new CacheFormDB<Data>(data);
        }

        /// <summary>
        /// 
        /// </summary>
        public void RemoveCache()
        {
            _memoryCache.Remove(Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Data"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public Data SaveCache<Data>(Data model) where Data : class
        {
            Keys.Add(Key);
            return _memoryCache.Set(Key, model);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="Data"></typeparam>
    public class CacheFormDB<Data> : IReadCacheFormDB<Data> where Data : class
    {
        private bool HasKey { get; set; }
        private Data Value { get; set; }
        private ICache cache { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="HasKey"></param>
        /// <param name="data"></param>
        /// <param name="cache"></param>
        public CacheFormDB(bool HasKey, Data data, ICache cache)
        {
            this.HasKey = HasKey;
            this.Value = data;
            this.cache = cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        public CacheFormDB(Data data)
        {
            this.Value = data;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<Data> GetCacheFormDB(Func<DbSet<Data>, List<Data>> func)
        {
            if (Value != null)
            {
                return new List<Data> { Value };
            }
            else
            {
                if (HasKey)
                {
                    return null;
                }
                else
                {
                    var List = func.Invoke(cache._db.Set<Data>());
                    return cache.SaveCache(List);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <returns></returns>
        public Data GetCacheFormDB(Func<DbSet<Data>, Data> func)
        {
            if (Value != null)
            {
                return Value;
            }
            else
            {
                if (HasKey)
                {
                    return default(Data);
                }
                else
                {

                    Value = func.Invoke(cache._db.Set<Data>());
                    return cache.SaveCache(Value);
                }
            }
        }
    }
}
