using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Interface
{
    public interface IUrl
    {
        void AddURL(string URL);
        string GetURL();
    }
}
