using BangumiProject.Areas.Bangumi.Interface;
using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    public class AnimeFilterByYear : IBangumiCase
    {
        public int Year { get; set; }
        public AnimeFilterByYear(int Year)
        {
            this.Year = Year;
        }

        /// <summary>
        /// 对动画数据进行过滤
        /// </summary>
        /// <param name="animes"></param>
        /// <returns></returns>
        public List<Anime> AnimeFilter(List<Anime> animes)
        {
            Span<Anime> Animes = animes.ToArray();
            int ArrLen = Animes.Length;
            return Get(ArrLen, Animes).ToList();
        }
        private IEnumerable<Anime> Get(int ArrLen, Span<Anime> Animes)
        {
            for (int i = 0; i < ArrLen; i++)
            {
                if (Animes[i].AnimePlayTime.Year == Year)
                {
                    yield return(Animes[i]);
                }
            }
        }
    }
}
