using System;
using System.Collections.Generic;
using System.Text;
using HttpCode.Core;

namespace MoeMushi.Core.DownLoad
{
    public class HtmlDownLoad
    {
        /// <summary>
        /// 
        /// </summary>
        private string URL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string Method { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private string PostData { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="URL"></param>
        public HtmlDownLoad(string URL)
        {
            this.URL = URL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="POSTDATA"></param>
        /// <returns></returns>
        public string POST(string POSTDATA)
        {
            PostData = POSTDATA;
            Method = "POST";
            return Process();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GET()
        {
            Method = "GET";
            return Process();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Process()
        {
            HttpHelpers httpHelpers = new HttpHelpers();
            HttpItems items = null;

            switch (Method)
            {
                case "GET":
                    items = new HttpItems
                    {
                        Method = Method,
                        Url = URL,
                    };
                    break;
                case "POST":
                    items = new HttpItems
                    {
                        Method = Method,
                        Url = URL,
                        Postdata = PostData
                    };
                    break;
            }
            HttpResults hr = httpHelpers.GetHtml(items);
            return hr.Html;
        }
    }
}
