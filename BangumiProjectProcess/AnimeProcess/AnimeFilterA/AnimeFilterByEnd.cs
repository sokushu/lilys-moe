
using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.AnimeProcess.AnimeFilterA
{
    public class AnimeFilterByEnd : IAnimeFilter
    { 
        private AnimeStats Stats { get; set; }
        public AnimeFilterByEnd(AnimeStats stats)
        {
            this.Stats = stats;
        }
        public AnimeFilterByEnd(int Input)
        {
            switch (Input)
            {
                case -1:
                    Stats = AnimeStats.All;
                    break;
                case 0:
                    Stats = AnimeStats.End;
                    break;
                case 1:
                    Stats = AnimeStats.Play;
                    break;
                default:
                    Stats = AnimeStats.All;
                    break;
            }
        }
        public List<Anime> Process(List<Anime> animes)
        {
            List<Anime> returnanime = new List<Anime>();
            switch (Stats)
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
