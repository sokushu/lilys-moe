using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Controllers;
using BangumiProject.Models;
using MoeUtilsBox.String;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Views.Bangumi
{
    public class BangumiModel
    {
        /// <summary>
        /// 过滤处理后的动画
        /// </summary>
        public List<Anime> Animes { get; set; }
        /// <summary>
        /// 动画的全部页数
        /// </summary>
        public int AllPage { get; set; }
        /// <summary>
        /// 现在动画的页数
        /// </summary>
        public int NowPage { get; set; }
        /// <summary>
        /// 全部的标签集合
        /// </summary>
        public List<string> AnimeTags { get; set; }
        /// <summary>
        /// 动画年份
        /// </summary>
        public List<int> AnimeYear { get; set; }
        /// <summary>
        /// 动画季度
        /// </summary>
        public List<int> AnimeSeason { get; set; }
        /// <summary>
        /// 得到图片
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public string AnimePic(Anime anime) => anime.AnimePic.IfEmptyReturnNull() ?? Final.DefaultAnimePic;
    }
}
