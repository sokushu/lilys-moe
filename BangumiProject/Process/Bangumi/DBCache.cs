using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Bangumi
{
    /// <summary>
    /// 
    /// </summary>
    public class DBCache
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMemoryCache _memoryCache;
        /// <summary>
        /// 
        /// </summary>
        private readonly BangumiProjectContext _DB;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        public DBCache(IMemoryCache _memoryCache, BangumiProjectContext _DB)
        {
            this._memoryCache = _memoryCache;
            this._DB = _DB;
        }

        /// <summary>
        /// 保存指定key的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="key">读取并保存指定的key的数据</param>
        /// <returns></returns>
        public async Task<T> SaveAsync<T>(string key) where T : class
        {
            if (!_memoryCache.TryGetValue(key, out T v))//如果没有值
            {
                ISwitchFunction function = new Impl1();
                return await Switch<T>(key, function)(key, _memoryCache, _DB);
            }
            return v;
        }
        private VoidSwitch<T> Switch<T>(string key, ISwitchFunction function)where T : class
        {
            switch (key)
            {
                case Final.Cache_AllAnime:
                    return new VoidSwitch<T>(function.Cache_AllAnimeAsync<T>);
                case Final.Cache_AllAnimeTags:
                    return new VoidSwitch<T>(function.Cache_AllAnimeTags<T>);
                case Final.Cache_AllAnimeYear:
                    return new VoidSwitch<T>(function.Cache_AllAnimeYearAsync<T>);
                default:
                    return default(VoidSwitch<T>);
            }
            
        }

        /// <summary>
        /// 得到一个Key所对应的值
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="key">Key</param>
        /// <returns></returns>
        public async Task<T> GetValueAsync<T>(string key) where T :class
        {
            if (!_memoryCache.TryGetValue(key, out T v))
            {
                return await SaveAsync<T>(key);
            }
            return v;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        private delegate Task<T> VoidSwitch<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB);

        /// <summary>
        /// 我们用的C#中的委托，
        /// 
        /// 实现SwitchFunction接口
        /// ======================
        /// 
        /// 将缓存中的数据保存到数据库中
        /// 
        /// </summary>
        /// <param name="key"></param>
        public async Task Updata_DBAsync<T>(string key) where T : class 
        {
            if (!_memoryCache.TryGetValue(key, out T v))//如果没有值
            {
                ISwitchFunction function = new Impl2();
                await Switch<T>(key, function)(key, _memoryCache, _DB);
            }
        }

    }

    /// <summary>
    /// 
    /// </summary>
    public interface ISwitchFunction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        /// <returns></returns>
        Task<T> Cache_AllAnimeAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        /// <returns></returns>
        Task<T> Cache_AllAnimeTags<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        /// <returns></returns>
        Task<T> Cache_AllAnimeYearAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class;
    }

    /// <summary>
    /// 保存到缓存的实现
    /// </summary>
    public struct Impl1 : ISwitchFunction
    {
        public async Task<T> Cache_AllAnimeAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            var AllAnime = await _DB.Anime.ToListAsync();
            _memoryCache.Set(key, AllAnime);
            return AllAnime as T;
        }

        public async Task<T> Cache_AllAnimeTags<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            var AllTags = await _DB.AnimeTag.ToListAsync();
            _memoryCache.Set(key, AllTags);
            return AllTags as T;
        }

        public async Task<T> Cache_AllAnimeYearAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            WeekSwitch weekSwitch = new WeekSwitch();
            var Animes = await _DB.Anime.ToListAsync();
            var AllAnimeYear = weekSwitch.GetAnimeYears(Animes);
            _memoryCache.Set(key, AllAnimeYear);
            return AllAnimeYear as T;
        }
    }

    /// <summary>
    /// 从缓存更新到数据库的实现
    /// </summary>
    public struct Impl2 : ISwitchFunction
    {
        public async Task<T> Cache_AllAnimeAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            var Animes = _memoryCache.Get<List<Anime>>(key);
            _DB.Anime.UpdateRange(Animes);
            await _DB.SaveChangesAsync();
            return Animes as T;
        }

        public async Task<T> Cache_AllAnimeTags<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            var AnimeTags = _memoryCache.Get<List<AnimeTag>>(key);
            _DB.AnimeTag.UpdateRange(AnimeTags);
            await _DB.SaveChangesAsync();
            return AnimeTags as T;
        }

        public Task<T> Cache_AllAnimeYearAsync<T>(string key, IMemoryCache _memoryCache, BangumiProjectContext _DB) where T : class
        {
            throw new NotSupportedException("因为年份是从所用动画中抽出的，所以不支持写入数据库");
        }
    }

}
