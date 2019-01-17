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
        /// <summary>
        /// 不指定动画类型，直接返回全部动画
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
