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
            int? iMode = httpContext.Session.GetInt32(nameof(UIMode));
            if (iMode != null)
            {
                UIMode ReturnMode = (UIMode)iMode;
                switch (ReturnMode)
                {
                    case UIMode.Normal_:
                    case UIMode.Normal_G:
                    case UIMode.YuriMode_:
                    case UIMode.YuriMode_Shojo:
                    case UIMode.YuriMode_G:
                        return ReturnMode;
                    default:
                        return UIMode.Normal_;
                }
            }
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
            httpContext.Session.SetInt32(nameof(UIMode), UIMode_Int);
        }
    }
}
