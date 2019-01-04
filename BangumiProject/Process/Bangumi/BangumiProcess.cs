using BangumiProject.Areas.Bangumi.Models;
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
    public class BangumiProcess
    {
        public BangumiProcess() { }

        /// <summary>
        /// 计算动画最新集数（动画集数机制变更）
        /// 
        /// 动画最新集数，通过开播日期来计算，例如开播日期是12月6日星期四，
        /// 那么，在下个星期四也就是13号，会自动更新第二集，直到设置状态为完结动画状态，
        /// 如果遇到动画事故等需要停播的情况，会有对应的设置的
        /// </summary>
        /// <param name="anime">要处理的动画</param>
        public bool AnimeNumUpdata(ref Anime anime)
        {
            if (anime.IsEnd == false)//这里只选取没有完结的动画做修改
            {
                //获取数据库中的动画集数
                var AnimeNumber = anime.AnimeNum;
                //计算现在实际应该更新到哪里，与数据库中的数据做对比
                var StartDate = anime.AnimePlayTime;
                var Tsu = StartDate.AddDays((AnimeNumber) * 7);
                //遇到特殊情况的处理对策（例如，临时停播，放送事故造成的变更）
                bool Stop = false;
                if (DateTime.Compare(DateTime.Now, Tsu) >= 0)//如果今天的日期大于数据库储存的（集数的下一集的日期），那就开始计算吧
                {
                    int num = 0;
                    while (true)
                    {
                        if (DateTime.Compare(DateTime.Now, Tsu) >= 0)
                        {
                            if (!Stop)//如果不是停播的话，动画加一
                            {
                                AnimeNumber = AnimeNumber + 1;
                            }
                            Tsu = Tsu.AddDays(7);
                        }
                        else
                        {
                            num = AnimeNumber;
                            break;
                        }
                    }

                    anime.AnimeNum = num;
                    //动画被修改
                    return true;
                }
            }
            //动画没有被修改
            return false;
        }

        /// <summary>
        /// 计算动画的停播
        /// </summary>
        private void AnimeNumStop()
        {

        }

        /// <summary>
        /// 从数据库中读取动画数据和相关数据，并保存到缓存中
        /// </summary>
        /// <param name="id"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        /// <returns></returns>
        public async Task<Anime> Anime_CacheSetOrGetAsync(int id, IMemoryCache _memoryCache, BangumiProjectContext _DB)
        {
            if (!_memoryCache.TryGetValue(id, out Anime anime))
            {
                /*
                 * 如果缓存中没有的话，就从数据库中读取数据
                 * 将关于这部动画的所有数据读取到缓存中
                 */
                // 读取当前用户在访问的动画数据
                anime = await _DB.Anime
                    .Include(OneAnime => OneAnime.Tags)
                    .Include(OneAnime => OneAnime.Souce)
                    .FirstOrDefaultAsync(OneAnime => OneAnime.AnimeID.Equals(id));

                if (anime == null)
                {
                    return null;
                }

                //加载动画评论
                var comms = await _DB.AnimeComms
                    .Include(animecomm => animecomm.Users)
                    .Where(animecomm => animecomm.Anime.Equals(anime))
                    .OrderByDescending(animecomm => animecomm.Time)
                    .ToListAsync();
                var blogss = await _DB.Blogs.Where(blog => blog.AnimeID.Equals(anime.AnimeID)).OrderByDescending(blog => blog.Time).ToListAsync();

                anime.AnimeComms = comms;

                //将读取的动画数据加入缓存中
                _memoryCache.Set(id, anime);
                //将动画相关blog的数据也写入缓存
                _memoryCache.Set($"Blogs{id}", blogss);
            }
            return anime;
        }

        /// <summary>
        /// 将缓存更新至最新状态
        /// </summary>
        /// <param name="anime"></param>
        /// <param name="AnimeID"></param>
        /// <param name="_memoryCache"></param>
        /// <param name="_DB"></param>
        public void Anime_CacheUpdataAll(Anime anime, int AnimeID, IMemoryCache _memoryCache)
        {
            _memoryCache.Set(AnimeID, anime);
            if (_memoryCache.TryGetValue(Final.Cache_AllAnime, out List<Anime> animes))
            {
                int num = animes.RemoveAll(a => a.AnimeID == AnimeID);
                animes.Add(anime);
                _memoryCache.Set(Final.Cache_AllAnime, animes);
            }
        }
    }
}
