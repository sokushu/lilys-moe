using BangumiProject.Areas.Bangumi.Interface;
using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    public class AnimeFilterByTagName : IBangumiCase
    {
        private string TagName { get; set; }
        private ICollection<AnimeTag> animeTags { get; set; }
        public AnimeFilterByTagName(string TagName, ICollection<AnimeTag> animeTags)
        {
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
            HashSet<int> Animeids = new HashSet<int> { 0 };
            foreach (var item in animeTags)
            {
                if (item.TagName == TagName)
                {
                    Animeids.Add(item.Anime.AnimeID);
                }
            }
            return Animeids;
        }
    }
}
