using BangumiProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Timers;

namespace BangumiProject.Process
{
    /// <summary>
    /// 数据缓存的工具类
    /// </summary>
    public class MemoryCacheHelper
    {
        public static HashSet<KEY> KeySet = new HashSet<KEY>();
        public static IMemoryCache _memoryCache { get; private set; }
        public static BangumiProjectContext _bangumiProjectContext { get; private set; }
        public static MoeMushiContext _moeMushi { get; private set; }
        public static UserManager<Users> _userManager { get; private set; }
        public static SignInManager<Users> _signInManager { get; private set; }
        private const long Time = 1000 * 60 * 60 * 6;//垃圾回收时间
        private static readonly Timer Timer_GC = new Timer//垃圾回收计时器
        {
            Enabled = true,
            Interval = Time
        };
        static MemoryCacheHelper()
        {
            Timer_GC.Elapsed += GC;
            Timer_GC.Start();
        }
        private static void GC(object obj, ElapsedEventArgs e)
        {
            List<KEY> ks = KeySet.ToList().OrderBy(key => key.I).ToList();
            int c = ks.Count;
            if (c < 500)
            {
                return;
            }
            else
            {
                for (int i = 500; i < c; i++)
                {
                    _memoryCache.Remove(ks[i].Key);
                }
            }
        }
        public MemoryCacheHelper(
            IMemoryCache _memoryCache, 
            BangumiProjectContext _bangumiProjectContext,
            MoeMushiContext _moeMushi
            )
        {
            if (MemoryCacheHelper._memoryCache == null)
            {
                MemoryCacheHelper._memoryCache = _memoryCache;
                MemoryCacheHelper._bangumiProjectContext = _bangumiProjectContext;
                MemoryCacheHelper._moeMushi = _moeMushi;
            }
        }
        public MemoryCacheHelper(){}

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T FormDBSetCache<T>(string key, Func<BangumiProjectContext, T> func)
        {
            return FormDBSetCache(key, CacheType.Other, func);
        }

        public T FormDBSetCache<T>(string key, CacheType cacheType, Func<BangumiProjectContext, T> func)
        {
            char[] k = key.ToCharArray();
            T Date = func.Invoke(_bangumiProjectContext);
            _memoryCache.Remove(k);
            KeySet.Add(new KEY { Key = k, Type = cacheType });
            return _memoryCache.Set(k, Date);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public async Task<T> FormDBSetCacheAsync<T>(string key, Func<BangumiProjectContext, Task<T>> func)
        {
            return await FormDBSetCacheAsync(key, CacheType.Other, func);
        }

        public async Task<T> FormDBSetCacheAsync<T>(string key, CacheType cacheType, Func<BangumiProjectContext, Task<T>> func)
        {
            char[] k = key.ToCharArray();
            T date = await func.Invoke(_bangumiProjectContext);
            _memoryCache.Remove(k);
            KeySet.Add(new KEY { Key = k, Type = cacheType });
            return _memoryCache.Set(k, date);
        }

        /// <summary>
        /// 
        /// </summary>
        public async Task UpdateAllCacheAsync()
        {
            foreach (KEY key in KeySet)
            {
                switch (key.Type)
                {
                    case CacheType.AnimeOne:
                        if (_memoryCache.TryGetValue(key.Key, out Anime anime))
                        {
                            _bangumiProjectContext.Anime.Update(anime);
                        }
                        break;
                    case CacheType.BlogOne:
                        if (_memoryCache.TryGetValue(key.Key, out Blog blog))
                        {
                            _bangumiProjectContext.Blogs.Update(blog);
                        }
                        break;
                    case CacheType.Other:
                        break;
                    default:
                        break;
                }
            }
            await _bangumiProjectContext.SaveChangesAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool TryGet<T>(string key, out T t)
        {
            char[] k = key.ToCharArray();
            KEY kEY = KeySet.Where(ks => ks.Key == k).FirstOrDefault();
            KeySet.Remove(kEY);
            kEY.I = kEY.I + 1;
            KeySet.Add(kEY);
            return _memoryCache.TryGetValue(k, out t);
        }

    }

    public struct KEY
    {
        public char[] Key { get; set; }
        public CacheType Type { get; set; }
        /// <summary>
        /// 被调用的次数
        /// </summary>
        public int I { get; set; }
    }
    public enum CacheType
    {
        AnimeOne,
        BlogOne,
        Other,
        Test,
    }
    public static class Keys
    {
        /// <summary>
        /// 根据得到一个动画
        /// 包含动画下的所有数据
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Anime_One(int ID)
        {
            return $"Anime{ID}";
        }
        /// <summary>
        /// 所有的动画
        /// 不包含字段的list数据，就是外键
        /// </summary>
        /// <returns></returns>
        public static string Anime_All()
        {
            return "AnimeAll";
        }
        /// <summary>
        /// 所有的年份
        /// </summary>
        /// <returns></returns>
        public static string Anime_AllYear()
        {
            return "AnimeAllYear";
        }
        /// <summary>
        /// 所有的标签
        /// </summary>
        /// <returns></returns>
        public static string Anime_AllTags()
        {
            return "AnimeAllTags";
        }



        /// <summary>
        /// 根据ID得到一篇博客
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static string Blog_One(int ID)
        {
            return $"Blog{ID}";
        }
        /// <summary>
        /// 不包含字段的list数据，就是外键
        /// </summary>
        /// <returns></returns>
        public static string Blog_All()
        {
            return "Blog_All";
        }
    }
}
