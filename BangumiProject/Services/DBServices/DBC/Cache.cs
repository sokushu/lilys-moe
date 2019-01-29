using BangumiProject.Services.DBServices.Interface;
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
        public Cache(char[] Key, IMemoryCache _memoryCache)
        {
            Keys.Add(Key);
            this.Key = Key;
            this._memoryCache = _memoryCache;
        }

        public Cache()
        {

        }

        public ICache BuildKey(string Key)
        {
            return new Cache(Key.ToCharArray(), _memoryCache);
        }

        public Data GetCache<Data>()
        {
            if (!_memoryCache.TryGetValue(Key, out Data data))
            {
                
            }
            return data;
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

    public class CacheFormDB<Data>
    {
        private bool HasKey { get; set; }
        private Data Value { get; set; }
        public CacheFormDB(bool HasKey, Data data)
        {
            this.HasKey = HasKey;
            this.Value = data;
        }

        public Data GetCacheFormDB(Func<string> Linq)
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

                }
            }
            return default(Data);
        }

    }
}
