﻿using BangumiProject.Areas.Bangumi.Interface;
using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    public class AnimeFilterByEnd : IBangumiCase
    {
        private AnimeStats stats { get; set; }
        public AnimeFilterByEnd(AnimeStats stats)
        {
            this.stats = stats;
        }

        public List<Anime> AnimeFilter(List<Anime> animes)
        {
            List<Anime> returnanime = new List<Anime>();
            switch (stats)
            {
                case AnimeStats.End:
                    Span<Anime> AnimeEnd = animes.ToArray();
                    int AEL = AnimeEnd.Length;
                    for (int i = 0; i < AEL; i++)
                    {
                        if (AnimeEnd[i].IsEnd == true)
                        {
                            returnanime.Add(AnimeEnd[i]);
                        }
                    }
                    break;
                case AnimeStats.Play:
                    Span<Anime> AnimePlay = animes.ToArray();
                    int APL = AnimePlay.Length;
                    for (int i = 0; i < APL; i++)
                    {
                        if (AnimePlay[i].IsEnd == false)
                        {
                            returnanime.Add(AnimePlay[i]);
                        }
                    }
                    break;
                case AnimeStats.All:
                    //不做处理
                    return animes;
                default:
                    throw new NullReferenceException("动画处理出现错误");
            }
            return returnanime;
        }
    }
}
