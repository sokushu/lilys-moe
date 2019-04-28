using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProjectDBServices.PageModels
{
    /// <summary>
    /// 
    /// </summary>
    public class Common_UIEnable
    {
        //==========================================================
        //Index
        //==========================================================
        [Key]
        public int ID { get; set; }

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
        //==========================================================
        //Index
        //==========================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="YuriMode"></param>
        /// <param name="iMode"></param>
        /// <param name="common_UIs"></param>
        /// <returns></returns>
        public static Common_UIEnable CreateUI(bool YuriMode, UIMode iMode)
        {
            Common_UIEnable common_UI = new Common_UIEnable();
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
}
