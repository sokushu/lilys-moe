using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Views.Bangumi
{
    public class BangumiEditModel
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
