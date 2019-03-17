
using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.AnimeProcess.AnimeFilterA
{
    public class AnimeFilterByWeek : IAnimeFilter
    {
        private int WeekDay { get; set; }
        public AnimeFilterByWeek(int weekDay)
        {
            if (WeekDay < 0 || weekDay > 6)
            {
                WeekDay = -1;
                return;
            }
            WeekDay = weekDay;
        }

        public List<Anime> Process(List<Anime> animes)
        {
            if (WeekDay == -1)
                return animes;
            List<Anime> ReturnAnime = new List<Anime>();
            //将参数转换为数组的形式
            Span<Anime> span = new Span<Anime>(animes.ToArray());
            //计算长度
            int count = span.Length;
            int day;
            //循环开始啦♪(^∇^*)
            for (int i = 0; i < count; i++)
            {
                day = (int)span[i].AnimePlayTime.DayOfWeek;
                if (day == WeekDay)
                {
                    ReturnAnime.Add(span[i]);
                }
            }
            return ReturnAnime;
        }
    }
}
