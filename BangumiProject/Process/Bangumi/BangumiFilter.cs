using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Bangumi
{
    /// <summary>
    /// 动画过滤用
    /// </summary>
    public class BangumiFilter
    {
        /// <summary>
        /// 动画的类型
        /// </summary>
        public AnimeType AnimeType { get; set; } = AnimeType.TVAnime;
        public bool AnimeTypeAll { get; set; }
        /// <summary>
        /// 动画的年份
        /// </summary>
        public int Year { get; set; } = 0;
        /// <summary>
        /// 动画是否已经完结
        /// </summary>
        public AnimeStats AnimeStats { get; set; } = AnimeStats.All;
        /// <summary>
        /// 动画的类型
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        private IFilterCheck filterCheck;
        /// <summary>
        /// 初始化
        /// </summary>
        public BangumiFilter(IFilterCheck filterCheck)
        {
            this.filterCheck = filterCheck;
        }

        public BangumiFilter()
        {
            filterCheck = new FilterCheck();
        }
        /// <summary>
        /// 
        /// </summary>
        private WeekSwitch WeekSwitch = new WeekSwitch();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="animes"></param>
        public List<Anime> Filter(ICollection<Anime> animes, ICollection<AnimeTag> animeTag)
        {
            Check();
            //动画的类型
            var anime = WeekSwitch.SwitchAnimeByType(animes, AnimeType, AnimeTypeAll);
            //选择是否完结
            anime = WeekSwitch.SwitchAnimeByStats(anime, AnimeStats);
            //根据动画年份进行选择
            anime = WeekSwitch.SwitchAnime(anime, WeekSwitch.SwitchType.Year, Year)[0];
            if (animeTag != null)
            {
                //动画的类型
                var tags = WeekSwitch.GetTagAnimeIDs(animeTag, TypeName);

                anime = anime.Where(anim => tags.Contains(anim.AnimeID)).ToList();
            }
            return anime;
        }

        /// <summary>
        /// 
        /// </summary>
        private void Check()
        {
            int y = Year;
            string t = TypeName;
            filterCheck.YearCheck(ref y);
            filterCheck.TypeNameCheck(ref t);
            Year = y;
            TypeName = t;
        }

        private struct FilterCheck : IFilterCheck
        {
            public bool TypeNameCheck(ref string TypeName)
            {
                return true;
            }

            public bool YearCheck(ref int year)
            {
                if (year != 0 || (year < 1900 || year > DateTime.Now.Year + 10))
                {
                    year = 0;
                    return false;
                }
                else
                {
                    return true;
                }

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public enum AnimeStats
    {
        End,Play,All
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IFilterCheck
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        bool YearCheck(ref int year);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="TypeName"></param>
        /// <returns></returns>
        bool TypeNameCheck(ref string TypeName);
    }
}
