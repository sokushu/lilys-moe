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
            var Cache = _memoryCache.GetOrCreate(Key, cache => cache.Value = ToFirst(func));
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
            var Cache = _memoryCache.GetOrCreate(Key, cache => cache.Value = ToList(func));
            return (List<T>)Cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public ICacheEntry GetCacheEntry(string Key)
        {
            return _memoryCache.CreateEntry(Key);
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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="obj"></param>
        public IDBCore Save_Updata<T>(string Key, T obj) where T : class
        {
            GetCacheEntry(Key).Value = obj;
            _db.Set<T>().Update(obj);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IDBCore Add<T>(T obj) where T : class
        {
            _db.Add(obj);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        public void Save_Remove(string Key)
        {
            _memoryCache.Remove(Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IDBCore Remove<T>(T obj) where T : class
        {
            _db.Remove(obj);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public List<T> ToList<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            return func.Invoke(_db.Set<T>()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T ToFirst<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            return func.Invoke(_db.Set<T>()).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        public T GetCache<T>(string Key)
        {
            return _memoryCache.Get<T>(Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool TryGet<T>(string Key, out T t)
        {
            return _memoryCache.TryGetValue(Key, out t);
        }
    }
}
