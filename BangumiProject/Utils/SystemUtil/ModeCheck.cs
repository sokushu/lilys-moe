using BangumiProjectDBServices.PageModels;
using BaseProject;
using BaseProject.Process;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// 
    /// </summary>
    public static class ModeCheck
    {
        /// <summary>
        /// 检查是否是百合模式
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
        public static UIMode UIModeCheck(this HttpContext httpContext, bool YuriMode = false)
        {
            int? iMode = httpContext.Session.GetInt32(nameof(UIMode));
            if (iMode != null)
            {
                UIMode ReturnMode = (UIMode)iMode;
                switch (ReturnMode)
                {
                    case UIMode.Normal_:
                    case UIMode.Normal_G:
                        return ReturnMode;
                    case UIMode.YuriMode_:
                    case UIMode.YuriMode_Shojo:
                        return YuriMode ? ReturnMode : UIMode.Normal_;
                    case UIMode.YuriMode_G:
                        return YuriMode ? ReturnMode : UIMode.Normal_G;
                    default:
                        return UIMode.Normal_;
                }
            }
            return UIMode.Normal_;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static Common_UIEnable CreateCommon_UI(this HttpContext httpContext)
        {
            Common_UIEnable common_UI = new Common_UIEnable()
            {
                New_4Anime = httpContext.Session.GetInt32(nameof(Common_UIEnable.New_4Anime)).IntToBool(),
                YuriInfo = httpContext.Session.GetInt32(nameof(Common_UIEnable.YuriInfo)).IntToBool(),
                YuriGoods = httpContext.Session.GetInt32(nameof(Common_UIEnable.YuriGoods)).IntToBool(),
                YuriNews = httpContext.Session.GetInt32(nameof(Common_UIEnable.YuriNews)).IntToBool(),
            };
            return common_UI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="common_UI"></param>
        public static void SetCommon_UI(this HttpContext httpContext, Common_UIEnable common_UI)
        {
            httpContext.Session.SetInt32(nameof(Common_UIEnable.New_4Anime), common_UI.New_4Anime.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common_UIEnable.YuriInfo), common_UI.YuriInfo.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common_UIEnable.YuriGoods), common_UI.YuriGoods.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common_UIEnable.YuriNews), common_UI.YuriNews.BoolToInt());
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
