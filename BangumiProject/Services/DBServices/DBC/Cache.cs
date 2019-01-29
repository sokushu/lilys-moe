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
    public class Cache : ICache
    {
        private char[] Key { get; set; }
        private readonly IMemoryCache _memoryCache;
        private static HashSet<char[]> Keys { set; get; } = new HashSet<char[]>();

        public BangumiProjectContext _db { get; private set; }

        public Cache(char[] Key, IMemoryCache _memoryCache, BangumiProjectContext _db)
        {
            Keys.Add(Key);
            this.Key = Key;
            this._memoryCache = _memoryCache;
            this._db = _db;
        }

        public ICache BuildKey(string Key)
        {
            return new Cache(Key.ToCharArray(), _memoryCache, _db);
        }

        public CacheFormDB<Data> GetCache<Data>()
        {
            if (!_memoryCache.TryGetValue(Key, out Data data))
            {
                return new CacheFormDB<Data>(Keys.Contains(Key), data, this);
            }
            return new CacheFormDB<Data>(data);
        }

        public void RemoveCache()
        {
            _memoryCache.Remove(Key);
        }

        public Data SaveCache<Data>(Data model)
        {
            return _memoryCache.Set(Key, model);
        }
    }

    public class CacheFormDB<Data> : IReadCacheFormDB<Data> where Data : class
    {
        private bool HasKey { get; set; }
        private Data Value { get; set; }
        private ICache cache { get; set; }
        public CacheFormDB(bool HasKey, Data data, ICache cache)
        {
            this.HasKey = HasKey;
            this.Value = data;
            this.cache = cache;
        }

        public CacheFormDB(Data data)
        {
            this.Value = data;
        }

        public Data GetCacheFormDB(Func<DbSet<Data>, IQueryable<Data>> func)
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
                    
                    Value = cache._db.Set<Data>()
                    cache.SaveCache(Value);
                }
            }
            return default(Data);
        }

    }
}
