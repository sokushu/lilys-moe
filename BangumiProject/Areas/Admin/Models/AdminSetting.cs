using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Admin.Models
{
    public class AdminSetting
    {
        /// <summary>
        /// 是否显示头图
        /// </summary>
        [Display(Name = "是否显示头图")]
        public bool IsShowTopPic { get; set; }

        /// <summary>
        /// 头图的显示路径
        /// </summary>
        [Display(Name = "头图的显示路径")]
        public string PicPath { get; set; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        [Display(Name = "网站是否开放注册")]
        public bool IsOpenSignUp { get; set; }

        /// <summary>
        /// 网站是否开启，是否能够访问
        /// </summary>
        [Display(Name = "网站是否开启")]
        public bool IsWebSiteOpen { get; set; }
    }
}
