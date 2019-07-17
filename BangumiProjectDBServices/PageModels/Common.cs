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

        /// <summary>
        /// 百合模式
        /// </summary>
        public bool YuriMode { get; set; } = false;

        /// <summary>
        /// 用户自定义的背景图片
        /// </summary>
        public string BackPicPath { get; set; } = string.Empty;

        /// <summary>
        /// 显示模式
        /// </summary>
        public UI UI { get; set; }
        
    }

    //======================================================
    //======================================================
    //界面显示
    public struct UI
    {
        /// <summary>
        /// 最近更新
        /// </summary>
        public bool New_4Anime { get; set; }

        /// <summary>
        /// 百合新闻
        /// </summary>
        public bool YuriNews { get; set; }

        /// <summary>
        /// 百合新情报
        /// </summary>
        public bool YuriInfo { get; set; }

        /// <summary>
        /// 百合商品新信息
        /// </summary>
        public bool YuriGoods { get; set; }

        /// <summary>
        /// 新番时间表
        /// </summary>
        public bool NewBangumiTime { get; set; }

        /// <summary>
        /// 背景图片
        /// </summary>
        public bool BackPic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="YuriMode"></param>
        /// <param name="iMode"></param>
        /// <returns></returns>
        public static UI CreateUI(bool YuriMode, UIMode iMode)
        {
            UI common_UI = new UI();
            switch (iMode)
            {
                case UIMode.Normal_:
                    common_UI.New_4Anime = true;
                    if (YuriMode)
                    {
                        common_UI.YuriNews = true;
                        common_UI.YuriInfo = true;
                        common_UI.YuriGoods = true;
                    }
                    else
                    {
                        common_UI.NewBangumiTime = true;
                    }
                    break;
                case UIMode.YuriMode_:
                    common_UI.New_4Anime = true;
                    common_UI.YuriNews = true;
                    common_UI.YuriInfo = true;
                    common_UI.YuriGoods = true;
                    break;
                case UIMode.YuriMode_Shojo:
                    common_UI.New_4Anime = true;
                    common_UI.YuriNews = true;
                    common_UI.YuriInfo = true;
                    common_UI.YuriGoods = true;
                    common_UI.BackPic = true;
                    break;
                case UIMode.YuriMode_G:
                case UIMode.Normal_G:
                    //需要从数据库中读取自定义配置
                    break;
                default:
                    throw new Exception();
            }
            return common_UI;
        }
    }
    //======================================================
    //======================================================
}
