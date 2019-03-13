using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    public abstract class IPage
    {
        protected string Page { get; set; }

        protected string[] Pages { get; set; }

        public abstract void PageSwitch();

        public string Build()
        {
            PageSwitch();
            if (Page == null)
            {
                throw new NullReferenceException();
            }
            return Page;
        }
    }
}
