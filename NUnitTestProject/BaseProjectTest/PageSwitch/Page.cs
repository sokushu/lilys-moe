using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.PageSwitch
{
    public class Page : IPage
    {
        private bool YuriMode { get; set; }
        public Page(bool YuriMode) : base("page1", "page2")
        {
            this.YuriMode = YuriMode;
        }
        public override void PageSwitch()
        {
            if (YuriMode)
            {
                Page = Pages[0];
            }
            Page = Pages[1];
        }
    }
}
