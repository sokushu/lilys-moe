using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
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

        public List<string> AnimeTagName { get; private set; }

        public List<int> AnimeYear { get; private set; }
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
            HashSet<int> year = (AnimeYear = WeekSwitch.GetAnimeYears(animes.ToList())).ToHashSet();
            if (!year.Contains(Year))
            {
                return new List<Anime>();
            }
            //动画的类型
            var anime = WeekSwitch.SwitchAnimeByType(animes, AnimeType, AnimeTypeAll);
            //选择是否完结
            anime = WeekSwitch.SwitchAnimeByStats(anime, AnimeStats);
            //根据动画年份进行选择
            anime = WeekSwitch.SwitchAnime(anime, WeekSwitch.SwitchType.Year, Year)[0];
            if (animeTag != null)
            {
                //动画的类型
                var Tags = WeekSwitch.SwitchTagByName(animeTag);
                //读取动画的TagName
                AnimeTagName = Tags.Keys.ToList();
                var tags = WeekSwitch.GetAnimeIds(Tags[TypeName]);

                anime = anime.Where(anim => tags.Contains(anim.AnimeID)).ToList();
            }
            return anime;
        }

        public static AnimeStats GetAnimeStats(int type)
        {
            switch (type)
            {
                case 0:
                    return Process.AnimeStats.End;
                case 1:
                    return Process.AnimeStats.Play;
                default:
                    return Process.AnimeStats.All;
            }
        }
        public static AnimeType GetAnimeType(int type)
        {
            switch (type)
            {
                case 0:
                    return AnimeType.TVAnime;
                case 1:
                    return AnimeType.OVA;
                case 2:
                    return AnimeType.MovieAnime;
                case 3:
                    return AnimeType.Other;
                default:
                    return default(AnimeType);
            }
        }
        public static bool GetAnimeTypeAll(int type)
        {
            return type > 0;
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
        End, Play, All
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
