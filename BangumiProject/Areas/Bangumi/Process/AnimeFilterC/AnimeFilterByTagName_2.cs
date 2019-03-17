
using BangumiProject.DBModels;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeFilterC
{
    /// <summary>
    /// 使用的第二种的过滤方式，
    /// 前提是动画的标签信息要提前加载
    /// </summary>
    public class AnimeFilterByTagName_2 : IAnimeFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private string TagName { get; set; }
        public AnimeFilterByTagName_2(string TagName)
        {
            this.TagName = TagName;
        }

        public List<Anime> AnimeFilter(List<Anime> animes)
        {
            return animes.Where(anime =>
            {
                //判断该动画中是否存在指定标签
                return anime.Tags.Where(tag => tag.TagName == TagName).Count() > 0;
            }).ToList();
        }
    }
}
