using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BangumiProjectDBServices.PageModels.Core;
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
    public static class HttpContextTool
    {
        /// <summary>
        /// 检查是否是百合模式
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static bool YuriModeCheck(this HttpContext httpContext)
        {
            int? mode = httpContext.Session.GetInt32(nameof(BaseModel.YuriMode));
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
        public static void SetComm(this HttpContext httpContext, BaseModel common)
        {
            httpContext.Session.SetInt32(nameof(BaseModel.IsSignIn), common.IsSignIn.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.YuriMode), common.YuriMode.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.YURI_TYPE), (int)common.YURI_TYPE);
            httpContext.Session.SetInt32(nameof(BaseModel.UIMode), (int)common.UIMode);
            httpContext.Session.SetString(nameof(BaseModel.BackPicPath), common.BackPicPath);

            httpContext.Session.SetInt32(nameof(BaseModel.UI.New_4Anime), common.UI.New_4Anime.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.UI.YuriNews), common.UI.YuriNews.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.UI.YuriInfo), common.UI.YuriInfo.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.UI.YuriGoods), common.UI.YuriGoods.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.UI.NewBangumiTime), common.UI.NewBangumiTime.BoolToInt());
            httpContext.Session.SetInt32(nameof(BaseModel.UI.BackPic), common.UI.BackPic.BoolToInt());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public static BaseModel GetComm(this HttpContext httpContext, BaseModel baseModel, bool IsSignIn = false)
        {
            UI uI;
            if (IsSignIn)
            {
                uI = UI.CreateUI(false, UIMode.Normal_);
            }
            else
            {
                uI = new UI
                {
                    New_4Anime = httpContext.Session.GetInt32(nameof(BaseModel.UI.New_4Anime)).IntToBool(),
                    YuriNews = httpContext.Session.GetInt32(nameof(BaseModel.UI.YuriNews)).IntToBool(),
                    YuriInfo = httpContext.Session.GetInt32(nameof(BaseModel.UI.YuriInfo)).IntToBool(),
                    YuriGoods = httpContext.Session.GetInt32(nameof(BaseModel.UI.YuriGoods)).IntToBool(),
                    NewBangumiTime = httpContext.Session.GetInt32(nameof(BaseModel.UI.NewBangumiTime)).IntToBool(),
                    BackPic = httpContext.Session.GetInt32(nameof(BaseModel.UI.BackPic)).IntToBool(),
                };
            }
            baseModel.IsSignIn = httpContext.Session.GetInt32(nameof(BaseModel.IsSignIn)).IntToBool();
            baseModel.YuriMode = httpContext.Session.GetInt32(nameof(BaseModel.YuriMode)).IntToBool();
            baseModel.YURI_TYPE = (Final.YURI_TYPE)(httpContext.Session.GetInt32(nameof(BaseModel.YURI_TYPE)) ?? 1);
            baseModel.UIMode = (UIMode)(httpContext.Session.GetInt32(nameof(BaseModel.UIMode)) ?? 0);
            baseModel.BackPicPath = (httpContext.Session.GetString(nameof(BaseModel.BackPicPath)) ?? string.Empty);
            baseModel.UI = uI;
            return baseModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="YuriMode"></param>
        public static void SetYuriMode(this HttpContext httpContext, bool YuriMode)
        {
            int Value = YuriMode.BoolToInt();
            httpContext.Session.SetInt32(nameof(BaseModel.YuriMode), Value);
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
        public static UI CreateCommon_UI(this HttpContext httpContext)
        {
            UI common_UI = new UI()
            {
                New_4Anime = httpContext.Session.GetInt32(nameof(UI.New_4Anime)).IntToBool(),
                YuriInfo = httpContext.Session.GetInt32(nameof(UI.YuriInfo)).IntToBool(),
                YuriGoods = httpContext.Session.GetInt32(nameof(UI.YuriGoods)).IntToBool(),
                YuriNews = httpContext.Session.GetInt32(nameof(UI.YuriNews)).IntToBool(),
            };
            return common_UI;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="common_UI"></param>
        public static void SetCommon_UI(this HttpContext httpContext, UI common_UI)
        {
            httpContext.Session.SetInt32(nameof(UI.New_4Anime), common_UI.New_4Anime.BoolToInt());
            httpContext.Session.SetInt32(nameof(UI.YuriInfo), common_UI.YuriInfo.BoolToInt());
            httpContext.Session.SetInt32(nameof(UI.YuriGoods), common_UI.YuriGoods.BoolToInt());
            httpContext.Session.SetInt32(nameof(UI.YuriNews), common_UI.YuriNews.BoolToInt());
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
