using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.PageMe
{
    public class AnimeOne : IPage
    {
        private bool YuriMode { get; set; }
        private bool ShowYuriPage { get; set; }
        public AnimeOne(bool YuriMode, bool ShowYuriPage, string[] Pages) : base(Pages)
        {
            this.YuriMode = YuriMode;
            this.ShowYuriPage = ShowYuriPage;
        }
        public override void PageSwitch()
        {
            if (YuriMode)
            {
                if (ShowYuriPage)
                {
                    Page = Pages[0];
                    return;
                }
            }
            Page = Pages[1];
        }
    }
}
