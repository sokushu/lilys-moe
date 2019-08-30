using BangumiProjectDBServices.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangumiProjectDBServices.Services
{
    public class CoreServices : IServices
    {
        private static HashSet<string> Keys = new HashSet<string>();
        private static HashSet<int> AnimeIDs = new HashSet<int>();
        private static bool AddED = false;

        public IMemoryCache MemoryCache { get; }

        public CoreContext DB { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MemoryCache"></param>
        /// <param name="DB"></param>
        public CoreServices(
            IMemoryCache MemoryCache,
            CoreContext DB
            )
        {
            this.MemoryCache = MemoryCache;
            this.DB = DB;
            if (AddED == false)
            {
                lock (this)
                {
                    if (AddED == false)
                    {
                        //DB.Anime.Select(ani => ani.AnimeID).ToList().ForEach(id =>
                        //{
                        //    AnimeIDs.Add(id);
                        //});
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
            var Cache = MemoryCache.GetOrCreate(Key, cache => cache.Value = ToFirst(func));
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
            var Cache = MemoryCache.GetOrCreate(Key, cache => cache.Value = ToList(func));
            return (List<T>)Cache;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public ICacheEntry GetCacheEntry(string Key)
        {
            return MemoryCache.CreateEntry(Key);
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
        public IServices Save_Updata<T>(string Key, T obj) where T : class
        {
            MemoryCache.Set(Key, obj);
            DB.Set<T>().Update(obj);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            DB.SaveChanges();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IServices Add<T>(T obj) where T : class
        {
            DB.Add(obj);
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        public void Save_Remove(string Key)
        {
            MemoryCache.Remove(Key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public IServices Remove<T>(T obj) where T : class
        {
            DB.Remove(obj);
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
            return func.Invoke(DB.Set<T>()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        public T ToFirst<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class
        {
            return func.Invoke(DB.Set<T>()).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        public T GetCache<T>(string Key)
        {
            return MemoryCache.Get<T>(Key);
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
            return MemoryCache.TryGetValue(Key, out t);
        }
    }
}
