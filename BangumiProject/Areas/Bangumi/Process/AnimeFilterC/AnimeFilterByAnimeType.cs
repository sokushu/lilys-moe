using BangumiProject.Areas.Bangumi.Interface;
using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{

    public class AnimeFilterByAnimeType : IBangumiCase
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
                    this.AnimeType = Models.AnimeType.TVAnime;
                    break;
                case 1:
                    this.AnimeType = Models.AnimeType.OVA;
                    break;
                case 2:
                    this.AnimeType = Models.AnimeType.MovieAnime;
                    break;
                case 3:
                    this.AnimeType = Models.AnimeType.Other;
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

        public List<Anime> AnimeFilter(List<Anime> animes)
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
