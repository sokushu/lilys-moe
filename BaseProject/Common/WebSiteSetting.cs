using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class WebSiteSetting
    {
        /// <summary>
        /// 是否显示头图
        /// </summary>
        public static bool IsShowTopPic { get; set; }

        /// <summary>
        /// 头图的显示路径
        /// </summary>
        public static string PicPath { get; set; }

        /// <summary>
        /// 是否开放注册
        /// </summary>
        public static bool IsOpenSignUp { get; set; }

        /// <summary>
        /// 网站是否开启，是否能够访问
        /// </summary>
        public static bool IsWebSiteOpen { get; set; }
    }
}
