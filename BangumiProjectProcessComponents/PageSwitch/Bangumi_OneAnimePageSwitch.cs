using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectProcessComponents.PageSwitch
{
    public class Bangumi_OneAnimePageSwitch : IPage
    {
        private string Item { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="YuriMode"></param>
        /// <param name="ShowYuriPage"></param>
        public Bangumi_OneAnimePageSwitch(
            bool YuriMode, 
            bool ShowYuriPage
            ) : base("NotYuriWarning", "Bangumi_OneAnime")
        {
            if (YuriMode)
            {
                if (ShowYuriPage)
                {
                    Item = Pages[0];
                }
            }
            else
            {
                Item = Pages[1];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override void PageSwitch()
        {
            Page = Item;
        }
    }
}
