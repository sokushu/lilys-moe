using BangumiProject.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
{
    public class WeekSwitch
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="animes"></param>
        public WeekSwitch()
        {
            
        }

        public int SeasonNow { get; }
        /// <summary>
        /// 
        /// </summary>
        public enum SwitchType
        {
            /// <summary>
            /// 按周分
            /// </summary>
            Week,
            /// <summary>
            /// 按年份
            /// </summary>
            Year,
            /// <summary>
            /// 按季度分
            /// </summary>
            Season,
        }

        /// <summary>
        /// 根据动画开播的日期，来对动画进行分类（星期分类）
        /// </summary>
        /// <param name="animes">要分类的动画集合</param>
        /// <returns>返回分类好的数据</returns>
        public List<List<Anime>> SwitchAnime(List<Anime> animes, SwitchType type, int year = 0)
        {
            switch (type)
            {
                case SwitchType.Week:
                    //创建装数据的List
                    //共7个日子
                    Span<List<Anime>> ss = new Span<List<Anime>>(new List<Anime>[]
                    {
                        new List<Anime>(),//星期天
                        new List<Anime>(),//星期一
                        new List<Anime>(),//星期二
                        new List<Anime>(),//星期三
                        new List<Anime>(),//星期四
                        new List<Anime>(),//星期五
                        new List<Anime>(),//星期六
                    });
                    //如果传过来的参数是Null的话，返回空的数据
                    if (animes == null) return ss.ToArray().ToList();
                    //将参数转换为数组的形式
                    Span<Anime> span = new Span<Anime>(animes.ToArray());
                    //计算长度
                    int count = span.Length;
                    //循环开始啦♪(^∇^*)
                    for (int i = 0; i < count; i++)
                    {
                        //拿到指定星期的List<Anime>数据
                        ss[
                        //将星期几的数据转化为Int型
                        (int)
                        //得到一部动画的数据
                        span[i]
                        //得到播放时间是星期几
                        .AnimePlayTime.DayOfWeek
                        ]
                        //把数据装到指定的List<Anime>中
                        .Add(span[i]);
                    }
                    //返回处理后的数据
                    return ss
                        //转换为数组
                        .ToArray()
                        //转换为List
                        .ToList();
                //最终的数据结构：List<List<Anime>>
                case SwitchType.Year:
                    if (year == 0)
                    {
                        return new List<List<Anime>> { animes };
                    }
                    else
                    {
                        Span<List<Anime>> Lists = new Span<List<Anime>>(new List<Anime>[]
                        {
                            new List<Anime>()
                        });
                        //将参数转换为数组的形式
                        Span<Anime> anime = new Span<Anime>(animes.ToArray());
                        int animeCount = anime.Length;
                        for (int i = 0; i < animeCount; i++)
                        {
                            if (anime[i].AnimePlayTime.Year == year)
                            {
                                Lists[0].Add(anime[i]);
                            }
                        }
                        return Lists.ToArray().ToList();
                    }
                case SwitchType.Season:
                    Span<List<Anime>> Season = new Span<List<Anime>>(new List<Anime>[]
                    {
                        new List<Anime>(),//一季度 一月新番
                        new List<Anime>(),//二季度 四月新番
                        new List<Anime>(),//三季度 七月新番
                        new List<Anime>(),//四季度 十月新番
                    });
                    //将参数转换为数组的形式
                    Span<Anime> animeSeason = new Span<Anime>(animes.ToArray());
                    int animeSeasonL = animeSeason.Length;
                    for (int i = 0; i < animeSeasonL; i++)
                    {
                        switch (animeSeason[i].AnimePlayTime.Month)
                        {
                            case 1:
                            case 2:
                            case 3:
                                Season[0].Add(animeSeason[i]);
                                break;
                            case 4:
                            case 5:
                            case 6:
                                Season[1].Add(animeSeason[i]);
                                break;
                            case 7:
                            case 8:
                            case 9:
                                Season[2].Add(animeSeason[i]);
                                break;
                            case 10:
                            case 11:
                            case 12:
                                Season[3].Add(animeSeason[i]);
                                break;
                            default:
                                break;
                        }
                    }
                    return Season.ToArray().ToList();
                default://如果是SwitchType以外，那么返回null
                    return null;
            }
        }

        /// <summary>
        /// 得到动画的播出年份
        /// 
        /// 例如，有一串动画，播出年份分别是
        /// 2018年，2008年，2018年，2017年，
        /// 那么返回就是2018年2008年2017年
        /// 
        /// 相当于去除重复？
        /// 
        /// </summary>
        /// <param name="animes">动画链表</param>
        /// <returns></returns>
        public List<int> GetAnimeYears(List<Anime> animes)
        {
            HashSet<int> year = new HashSet<int>();
            if (animes == null) return new List<int>();

            Span<Anime> anime = new Span<Anime>(animes.ToArray());

            //长度
            int l = anime.Length;
            int Y;
            for (int i = 0; i < l; i++)
            {
                Y = anime[i].AnimePlayTime.Year;
                if (year.Contains(Y))
                {
                    continue;
                }
                else
                {
                    year.Add(Y);
                }
            }
            return year.OrderByDescending(r => r).ToList();

        }

        /// <summary>
        /// 处理返回根据动画播出年份分类的集合
        /// </summary>
        /// <param name="animes"></param>
        /// <returns></returns>
        public Dictionary<int, List<Anime>> SwitchAnimeByYear(List<Anime> animes)
        {
            if (animes == null)
            {
                //如果是空就返回空的集合
                return new Dictionary<int, List<Anime>>();
            }
            else
            {
                Dictionary<int, List<Anime>> animeByYear = new Dictionary<int, List<Anime>>();
                Span<Anime> a = new Span<Anime>(animes.ToArray());
                var l = a.Length;//长度
                for (int i = 0; i < l; i++)
                {
                    if (animeByYear[a[i].AnimePlayTime.Year] == null)
                    {
                        animeByYear[a[i].AnimePlayTime.Year] = new List<Anime>
                        {
                            a[i]
                        };
                    }
                    else
                    {
                        animeByYear[a[i].AnimePlayTime.Year].Add(a[i]);
                    }
                }
                // 因为年份有可能不同，两个，三个，不固定，所以渲染时要计算长度
                return animeByYear;
            }
        }

        /// <summary>
        /// 
        /// 将动画的标签整合到一起
        /// 
        /// </summary>
        /// <param name="animeTags"></param>
        /// <returns></returns>
        public Dictionary<string, List<AnimeTag>> SwitchTagByName(ICollection<AnimeTag> animeTags)
        {
            Dictionary<string, List<AnimeTag>> valuePairs = new Dictionary<string, List<AnimeTag>>();
            HashSet<string> Ki = new HashSet<string>();
            Span<AnimeTag> span = animeTags.ToArray();
            int spanLen = span.Length;
            for (int i = 0; i < spanLen; i++)
            {
                if (Ki.Contains(span[i].TagName))
                {
                    valuePairs[span[i].TagName].Add(span[i]);
                }
                else
                {
                    Ki.Add(span[i].TagName);
                    valuePairs.Add(span[i].TagName, new List<AnimeTag>());
                }
            }
            return valuePairs;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animeTags"></param>
        /// <param name="TagName"></param>
        /// <returns></returns>
        public List<AnimeTag> SwitchTagByName(ICollection<AnimeTag> animeTags, string TagName)
        {
            if (TagName == string.Empty) return animeTags.ToList();

            List<AnimeTag> tag = new List<AnimeTag>();
            Span<AnimeTag> animeTag = animeTags.ToArray();
            int Len = animeTag.Length;
            for (int i = 0; i < Len; i++)
            {
                if (TagName.Equals(animeTag[i].TagName))
                {
                    tag.Add(animeTag[i]);
                }
            }
            return tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tags"></param>
        /// <param name="TagName"></param>
        /// <returns></returns>
        public HashSet<int> GetTagAnimeIDs(ICollection<AnimeTag> tags, string TagName)
        {
            HashSet<int> vs = new HashSet<int>();
            Span<AnimeTag> animetags = SwitchTagByName(tags, TagName).ToArray();
            int Len = animetags.Length;
            for (int i = 0; i < Len; i++)
            {
                vs.Add(animetags[i].Anime.AnimeID);
            }
            return vs;
        }

        public HashSet<int> GetAnimeIds(ICollection<AnimeTag> tags)
        {
            HashSet<int> ID = new HashSet<int>();
            foreach (var item in tags)
            {
                ID.Add(item.Anime.AnimeID);
            }
            return ID;
        }

        /// <summary>
        /// 返回一个空的List
        /// </summary>
        /// <returns></returns>
        private List<Anime> GetEmpty()
        {
            return new List<Anime>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<List<AnimeUserInfo>> SwitchAnimeByStats(List<AnimeUserInfo> animes)
        {
            List<AnimeUserInfo> AnimeEnd = new List<AnimeUserInfo>();//看完的
            List<AnimeUserInfo> AnimeStart = new List<AnimeUserInfo>();//已经开始看的
            List<AnimeUserInfo> AnimeSubed = new List<AnimeUserInfo>();//订阅完还没开始看的

            foreach (var item in animes)
            {
                var anime = item.SubAnime;
                var IsEnd = anime.IsEnd;
                if (anime.AnimeNum == item.NowAnimeNum)//看完的
                {
                    if (IsEnd)//是否已经完结
                    {
                        AnimeEnd.Add(item);//已完结的，添加到看完了目录
                    }
                    else
                    {
                        AnimeStart.Add(item);//没完结的，添加到已经开始看，但还没看完的目录
                    }

                }
                if (item.NowAnimeNum != 0 && item.NowAnimeNum < anime.AnimeNum)//还没看完
                {
                    AnimeStart.Add(item);
                }
                if (item.NowAnimeNum == 0)//还没开始看
                {
                    AnimeSubed.Add(item);
                }
            }
            return new List<List<AnimeUserInfo>> { AnimeSubed, AnimeStart, AnimeEnd };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animes"></param>
        /// <param name="animeStats"></param>
        /// <returns></returns>
        public List<Anime> SwitchAnimeByStats(ICollection<Anime> animes, AnimeStats animeStats)
        {
            switch (animeStats)
            {
                case AnimeStats.End:
                    return SwitchAnimeByStats(animes.ToList(), true);
                case AnimeStats.Play:
                    return SwitchAnimeByStats(animes.ToList(), false);
                case AnimeStats.All:
                    return animes.ToList();
                default:
                    return null;
            }
        }

        public enum AnimeStats
        {
            End,Play,All
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animes"></param>
        /// <param name="IsEnd"></param>
        /// <returns></returns>
        private List<Anime> SwitchAnimeByStats(List<Anime> animes, bool IsEnd)
        {
            List<Anime> ani = new List<Anime>();
            Span<Anime> anime = animes.ToArray();
            int Len = anime.Length;
            for (int i = 0; i < Len; i++)
            {
                if (anime[i].IsEnd == IsEnd)
                {
                    ani.Add(anime[i]);
                }
            }
            return ani;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animes"></param>
        /// <param name="animeType"></param>
        /// <returns></returns>
        public List<Anime> SwitchAnimeByType(ICollection<Anime> animes, AnimeType animeType, bool All)
        {
            List<Anime> returnData = new List<Anime>();
            Span<Anime> anime = animes?.ToArray() ?? new Span<Anime>();
            int Len = anime.Length;
            //因为没有设置了，就先这样设置一下
            if (All)
            {
                return animes?.ToList() ?? returnData;
            }
            else
            {
                for (int i = 0; i < Len; i++)
                {
                    if (anime[i].AnimeType == animeType)
                    {
                        returnData.Add(anime[i]);
                    }
                }
            }
            return returnData;
        }
    }
}
