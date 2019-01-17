using BangumiProject.Areas.Bangumi.Interface;
using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    public class AnimeFilterBySeason : IBangumiCase
    {
        private HashSet<int> Season = new HashSet<int>();
        public AnimeFilterBySeason(int Season)
        {
            if (Season > 4 || Season < 1)
            {
                Season = -1;
                return;
            }
            switch (Season)
            {
                case 1:
                    this.Season.Add(1);
                    this.Season.Add(2);
                    this.Season.Add(3);
                    break;
                case 2:
                    this.Season.Add(4);
                    this.Season.Add(5);
                    this.Season.Add(6);
                    break;
                case 3:
                    this.Season.Add(7);
                    this.Season.Add(8);
                    this.Season.Add(9);
                    break;
                case 4:
                    this.Season.Add(10);
                    this.Season.Add(11);
                    this.Season.Add(12);
                    break;
                default:
                    this.Season.Add(-1);
                    break;
            }
        }

        public List<Anime> AnimeFilter(List<Anime> animes)
        {
            List<Anime> ReturnAnime = new List<Anime>();
            //将参数转换为数组的形式
            Span<Anime> animeSeason = new Span<Anime>(animes.ToArray());
            int animeSeasonL = animeSeason.Length;
            for (int i = 0; i < animeSeasonL; i++)
            {
                if (Season.Contains(animeSeason[i].AnimePlayTime.Month))
                {
                    ReturnAnime.Add(animeSeason[i]);
                }
            }
            return ReturnAnime;
        }
    }
}
