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
    public class DBCore : IDBCore
    {
        
        private readonly IMemoryCache _memoryCache;
        private readonly BangumiProjectContext _db;
        private static HashSet<string> Keys = new HashSet<string>();
        private static HashSet<int> AnimeIDs = new HashSet<int>();
        private static bool AddED = false;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_memoryCache"></param>
        /// <param name="_db"></param>
        public DBCore(
            IMemoryCache _memoryCache,
            BangumiProjectContext _db
            )
        {
            this._memoryCache = _memoryCache;
            this._db = _db;
            if (AddED == false)
            {
                lock (this)
                {
                    if (AddED == false)
                    {
                        _db.Anime.Select(ani => ani.AnimeID).ToList().ForEach(id =>
                        {
                            AnimeIDs.Add(id);
                        });
                        AddED = true;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T Save_ToFirst<T>(string Key, Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            //先尝试直接从缓存中读取
            Keys.Add(Key);
            var Cache = _memoryCache.GetOrCreate(Key, cache => cache.Value = (func.Invoke(_db.Set<T>()).FirstOrDefault()));
            return (T)Cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<T> Save_ToList<T>(string Key, Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            //先尝试直接从缓存中读取
            Keys.Add(Key);
            var Cache = _memoryCache.GetOrCreate(Key, cache => cache.Value = (func.Invoke(_db.Set<T>()).ToList()));
            return (List<T>)Cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public ICacheEntry GetCacheEntry(string Key)
        {
            return _memoryCache.GetOrCreate(Key, cache => cache);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public bool HasAnimeID(int ID)
        {
            return AnimeIDs.Contains(ID);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void ADDAnimeID(int ID)
        {
            AnimeIDs.Add(ID);
        }
    }
}
