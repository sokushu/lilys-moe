using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace System
{
    public static class ModeCheck
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool YuriModeCheck(this HttpContext httpContext)
        {
            int? mode = httpContext.Session.GetInt32(Final.YuriMode);
            if (mode != null)
            {
                return mode.IntToBool();
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="YuriMode"></param>
        public static void SetYuriMode(this HttpContext httpContext, bool YuriMode)
        {
            int Value = YuriMode.BoolToInt();
            httpContext.Session.SetInt32(Final.YuriMode, Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static UIMode UIModeCheck(this HttpContext httpContext)
        {
            return UIMode.Normal_;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="iMode"></param>
        public static void SetUIMode(this HttpContext httpContext, UIMode iMode)
        {
            int UIMode_Int = (int)iMode;
            httpContext.Session.SetInt32("", UIMode_Int);
        }
    }
}
