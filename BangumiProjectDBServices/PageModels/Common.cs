using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectDBServices.PageModels
{
    public class Common
    {
        /// <summary>
        /// 用户权限的类型
        /// </summary>
        public Final.YURI_TYPE YURI_TYPE { get; set; } = Final.YURI_TYPE.Yuri_Yuri1;

        /// <summary>
        /// 是否已经登陆
        /// </summary>
        public bool IsSignIn { get; set; } = false;

        /// <summary>
        /// UI的显示模式
        /// </summary>
        public UIMode UIMode { get; set; } = UIMode.Normal_;

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; } = string.Empty;
    }
}
