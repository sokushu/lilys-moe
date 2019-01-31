using Microsoft.EntityFrameworkCore;
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
        /// <param name="Method"></param>
        /// <returns></returns>
        T Save_ToFirst<T>(string Key, Func<DbSet<T>, IQueryable<T>> func) where T : class;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        ICacheEntry GetCacheEntry(string Key);

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
