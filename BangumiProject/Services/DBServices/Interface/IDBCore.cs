﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface IDBCore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <param name="func"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        List<T> Save_ToList<T>(string Key, Func<DbSet<T>, IQueryable<T>> func) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        List<T> ToList<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <param name="Method"></param>
        /// <returns></returns>
        T Save_ToFirst<T>(string Key, Func<DbSet<T>, IQueryable<T>> func) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="func"></param>
        /// <returns></returns>
        T ToFirst<T>(Func<DbSet<T>, IQueryable<T>> func) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="obj"></param>
        IDBCore Save_Updata<T>(string Key, T obj) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        void Save_Remove(string Key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        IDBCore Add<T>(T obj) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        IDBCore Remove<T>(T obj) where T : class;

        /// <summary>
        /// 
        /// </summary>
        void Commit();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        ICacheEntry GetCacheEntry(string Key);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        bool TryGet<T>(string Key, out T t);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        T GetCache<T>(string Key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        bool HasAnimeID(int ID);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        void ADDAnimeID(int ID);
    }
}
