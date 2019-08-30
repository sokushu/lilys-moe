using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Views.Bangumi.Model
{
    public class BangumiEdit : BaseModel
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
