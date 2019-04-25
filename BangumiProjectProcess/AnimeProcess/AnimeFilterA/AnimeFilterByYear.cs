﻿
using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.AnimeProcess.AnimeFilterA
{
    public class AnimeFilterByYear : IAnimeFilter
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
        public List<Anime> Process(List<Anime> animes)
        {
            if (Year == -1)//年份-1代表不过滤
                return animes;
            List<Anime> ReturnAnime = new List<Anime>();
            Span<Anime> Animes = animes.ToArray();
            int ArrLen = Animes.Length;
            for (int i = 0; i < ArrLen; i++)
            {
                if (Animes[i].AnimePlayTime.Year == Year)
                {
                    ReturnAnime.Add(Animes[i]);
                }
            }
            return ReturnAnime;
        }
    }
}