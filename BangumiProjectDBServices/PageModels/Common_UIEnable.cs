using System;
using System.Collections.Generic;
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
        /// <summary>
        /// 
        /// </summary>
        public bool New_4Anime { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool YuriNews { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool YuriInfo { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool YuriGoods { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool NewBangumiTime { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool BackPic { get; set; } = false;
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
        public static Common_UIEnable CreateUI(bool YuriMode, UIMode iMode, Common_UIEnable common_UIs = null)
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
                    if (common_UIs != null)
                    {
                        common_UI = common_UIs;
                    }
                    break;
                default:
                    throw new Exception();
            }
            return common_UI;
        }
    }
}
