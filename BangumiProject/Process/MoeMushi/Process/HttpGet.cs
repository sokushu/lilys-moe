using BangumiProject.Process.MoeMushi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using HttpCode.Core;
using System.Net.Http;
using System.IO;

namespace BangumiProject.Process.MoeMushi.Process
{
    public class HttpGet : IDownLoad
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        public string HttpHtml(string url)
        {
            HttpHelpers httpHelpers = new HttpHelpers();
            HttpItems httpItems = new HttpItems
            {
                Url = url,
                Method = HttpMethod.Get.Method
            };

            HttpResults httpResults = httpHelpers.GetHtml(httpItems);

            return httpResults.Html;
        }

        public Stream HttpStream(string url)
        {
            throw new NotImplementedException();
        }
    }
}
