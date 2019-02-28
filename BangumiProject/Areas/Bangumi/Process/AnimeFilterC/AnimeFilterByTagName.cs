
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    public class AnimeFilterByTagName : IAnimeFilter
    {
        private string TagName { get; set; }
        private ICollection<AnimeTag> animeTags { get; set; }
        private bool All { get; set; } = false;
        public AnimeFilterByTagName(string TagName, ICollection<AnimeTag> animeTags)
        {
            if (TagName == string.Empty)
            {
                All = true;
            }
            this.TagName = TagName;
            this.animeTags = animeTags;
        }

        /// <summary>
        /// 根据标签过滤动画
        /// </summary>
        /// <param name="animes"></param>
        /// <returns></returns>
        public List<Anime> AnimeFilter(List<Anime> animes)
        {
            HashSet<int> IDs = GetAnimeIDs(animeTags);
            return animes.Where(anime => IDs.Contains(anime.AnimeID)).ToList();
        }

        /// <summary>
        /// 获取动画ID集合
        /// </summary>
        /// <param name="animeTags"></param>
        /// <returns></returns>
        private HashSet<int> GetAnimeIDs(ICollection<AnimeTag> animeTags)
        {
            HashSet<int> Animeids = new HashSet<int>();
            foreach (var item in animeTags)
            {
                if (All || item.TagName == TagName)
                {
                    Animeids.Add(item.Anime.AnimeID);
                }
            }
            return Animeids;
        }
    }
}
