using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectDBServices.PageModels
{
    public class Common
    {
        public bool Yuri_Admin { get; set; }

        public bool Yuri_Girl { get; set; }

        public bool Yuri_Yuri5 { get; set; }

        public bool Yuri_Yuri4 { get; set; }

        public bool Yuri_Yuri3 { get; set; }

        public bool Yuri_Yuri2 { get; set; }

        public bool Yuri_Yuri1 { get; set; }

        public bool Yuri_Boy { get; set; }

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
