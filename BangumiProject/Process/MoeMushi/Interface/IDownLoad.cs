using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Interface
{
    public interface IDownLoad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        string HttpHtml(string url);
        Stream HttpStream(string url);
    }
}
