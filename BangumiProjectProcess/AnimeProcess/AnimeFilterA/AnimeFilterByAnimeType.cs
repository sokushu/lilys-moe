
using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.AnimeProcess.AnimeFilterA
{

    public class AnimeFilterByAnimeType : IAnimeFilter
    {
        private AnimeType AnimeType { get; set; }
        private bool All { get; set; }
        /// <summary>
        /// 指定动画类型
        /// </summary>
        /// <param name="animeType"></param>
        public AnimeFilterByAnimeType(AnimeType animeType)
        {
            AnimeType = animeType;
        }

        public AnimeFilterByAnimeType(int AnimeType)
        {
            switch (AnimeType)
            {
                case -1:
                    All = true;
                    break;
                case 0:
                    this.AnimeType = BangumiProjectDBServices.Models.AnimeType.TVAnime;
                    break;
                case 1:
                    this.AnimeType = BangumiProjectDBServices.Models.AnimeType.OVA;
                    break;
                case 2:
                    this.AnimeType = BangumiProjectDBServices.Models.AnimeType.MovieAnime;
                    break;
                case 3:
                    this.AnimeType = BangumiProjectDBServices.Models.AnimeType.Other;
                    break;
                default:
                    All = true;
                    break;
            }
        }
        /// <summary>
        /// 不指定动画类型，直接返回全部动画t
        /// </summary>
        public AnimeFilterByAnimeType()
        {
            All = true;
        }

        public List<Anime> Process(List<Anime> animes)
        {
            if (All)
                return animes;
            List<Anime> ReturnAnime = new List<Anime>();
            Span<Anime> Anime = animes.ToArray();
            int AL = Anime.Length;
            for (int i = 0; i < AL; i++)
            {
                if (Anime[i].AnimeType == AnimeType)
                {
                    ReturnAnime.Add(Anime[i]);
                }
            }
            return ReturnAnime;
        }
    }
}
