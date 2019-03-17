using BangumiProject.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Views.Bangumi.Model
{
    public class BangumiEdit
    {
        /// <summary>
        /// 动画
        /// </summary>
        public Anime Anime { get; set; }
        /// <summary>
        /// 添加标签
        /// </summary>
        public string AddTag { get; set; }
    }
}
