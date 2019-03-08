﻿using BangumiProject.Models;
using BaseProject.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.DBService
{
    public class CoreServices : IServices
    {
        private static HashSet<string> Keys = new HashSet<string>();
        private static HashSet<int> AnimeIDs = new HashSet<int>();
        private static bool AddED = false;
        public UserManager<User> UserManager { get; }

        public SignInManager<User> SignInManager { get; }

        public IAuthorizationService AuthorizationService { get; }

        public IMemoryCache MemoryCache { get; }

        public BangumiProjectContext DB { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MemoryCache"></param>
        /// <param name="DB"></param>
        public CoreServices(
            UserManager<User> UserManager,
            SignInManager<User> SignInManager,
            IAuthorizationService AuthorizationService,
            IMemoryCache MemoryCache,
            BangumiProjectContext DB
            )
        {
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.AuthorizationService = AuthorizationService;
            this.MemoryCache = MemoryCache;
            this.DB = DB;
            if (AddED == false)
            {
                lock (this)
                {
                    if (AddED == false)
                    {
                        DB.Anime.Select(ani => ani.AnimeID).ToList().ForEach(id =>
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
