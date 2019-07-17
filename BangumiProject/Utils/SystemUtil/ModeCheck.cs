using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BaseProject;
using BaseProject.Process;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
            int? mode = httpContext.Session.GetInt32(nameof(Common.YuriMode));
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
        /// <param name="common"></param>
        public static void SetComm(this HttpContext httpContext, Common common)
        {
            httpContext.Session.SetInt32(nameof(Common.IsSignIn), common.IsSignIn.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.YuriMode), common.YuriMode.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.YURI_TYPE), (int)common.YURI_TYPE);
            httpContext.Session.SetInt32(nameof(Common.UIMode), (int)common.UIMode);
            httpContext.Session.SetString(nameof(Common.BackPicPath), common.BackPicPath);

            httpContext.Session.SetInt32(nameof(Common.UI.New_4Anime), common.UI.New_4Anime.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.UI.YuriNews), common.UI.YuriNews.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.UI.YuriInfo), common.UI.YuriInfo.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.UI.YuriGoods), common.UI.YuriGoods.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.UI.NewBangumiTime), common.UI.NewBangumiTime.BoolToInt());
            httpContext.Session.SetInt32(nameof(Common.UI.BackPic), common.UI.BackPic.BoolToInt());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserManager"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static Common CommonMake(UserManager<User> UserManager, HttpContext httpContext, bool isSignIn)
        {
            var user = UserManager.GetUserAsync(httpContext.User).Result;
            string policyName = UserManager.GetRolesAsync(user).Result.FirstOrDefault();

            Final.YURI_TYPE _type = policyName.GetYuri_Type();//获取权限类型

            bool YuriMode = httpContext.YuriModeCheck();
            UIMode iMode = httpContext.UIModeCheck(YuriMode);

            //将通用数据写入到Session里面
            return new Common
            {
                IsSignIn = isSignIn,
                UI = UI.CreateUI(YuriMode, iMode),
                YuriMode = YuriMode,
                BackPicPath = user.UserBackPic ?? string.Empty,
                UIMode = iMode,
                Username = user.UserName,
                YURI_TYPE = _type
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public static Final.YURI_TYPE GetYuri_Type(this string policyName)
        {
            switch (policyName)
            {
                case Final.Yuri_Admin:
                    return Final.YURI_TYPE.Yuri_Admin;
                case Final.Yuri_Girl:
                    return Final.YURI_TYPE.Yuri_Girl;
                case Final.Yuri_Yuri1:
                    return Final.YURI_TYPE.Yuri_Yuri1;
                case Final.Yuri_Yuri2:
                    return Final.YURI_TYPE.Yuri_Yuri2;
                case Final.Yuri_Yuri3:
                    return Final.YURI_TYPE.Yuri_Yuri3;
                case Final.Yuri_Yuri4:
                    return Final.YURI_TYPE.Yuri_Yuri4;
                case Final.Yuri_Yuri5:
                    return Final.YURI_TYPE.Yuri_Yuri5;
                case Final.Yuri_Boy:
                    return Final.YURI_TYPE.Yuri_Boy;
                default:
                    return Final.YURI_TYPE.Yuri_Yuri1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static Common GetComm(this HttpContext httpContext, bool UI0 = false)
        {
            UI uI;
            if (UI0)
            {
                uI = UI.CreateUI(false, UIMode.Normal_);
            }
            else
            {
                uI = new UI
                {
                    New_4Anime = httpContext.Session.GetInt32(nameof(Common.UI.New_4Anime)).IntToBool(),
                    YuriNews = httpContext.Session.GetInt32(nameof(Common.UI.YuriNews)).IntToBool(),
                    YuriInfo = httpContext.Session.GetInt32(nameof(Common.UI.YuriInfo)).IntToBool(),
                    YuriGoods = httpContext.Session.GetInt32(nameof(Common.UI.YuriGoods)).IntToBool(),
                    NewBangumiTime = httpContext.Session.GetInt32(nameof(Common.UI.NewBangumiTime)).IntToBool(),
                    BackPic = httpContext.Session.GetInt32(nameof(Common.UI.BackPic)).IntToBool(),
                };
            }
            return new Common
            {
                IsSignIn = httpContext.Session.GetInt32(nameof(Common.IsSignIn)).IntToBool(),
                YuriMode = httpContext.Session.GetInt32(nameof(Common.YuriMode)).IntToBool(),
                YURI_TYPE = (Final.YURI_TYPE)(httpContext.Session.GetInt32(nameof(Common.YURI_TYPE)) ?? 1),
                UIMode = (UIMode)(httpContext.Session.GetInt32(nameof(Common.UIMode)) ?? 0),
                BackPicPath = (httpContext.Session.GetString(nameof(Common.BackPicPath)) ?? string.Empty),
                UI = uI,
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="YuriMode"></param>
        public static void SetYuriMode(this HttpContext httpContext, bool YuriMode)
        {
            int Value = YuriMode.BoolToInt();
            httpContext.Session.SetInt32(nameof(Common.YuriMode), Value);
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
