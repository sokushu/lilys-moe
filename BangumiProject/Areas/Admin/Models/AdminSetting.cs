using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Admin.Models
{
    public class AdminSetting
    {
        /// <summary>
        /// 是否显示头图
        /// </summary>
        public bool IsShowTopPic { get; set; }

        /// <summary>
        /// 头图的显示路径
        /// </summary>
        public string PicPath { get; set; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        public bool IsOpenSignUp { get; set; }

        /// <summary>
        /// 网站是否开启，是否能够访问
        /// </summary>
        public bool IsWebSiteOpen { get; set; }
    }
}
