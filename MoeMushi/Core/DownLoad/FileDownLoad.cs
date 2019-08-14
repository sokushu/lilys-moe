using HttpCode.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoeMushi.Core.DownLoad
{
    public class FileDownLoad
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
        public FileDownLoad(string URL)
        {
            this.URL = URL;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="POSTDATA"></param>
        /// <returns></returns>
        public byte[] POST(string POSTDATA)
        {
            PostData = POSTDATA;
            Method = "POST";
            return Process();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public byte[] GET()
        {
            Method = "GET";
            return Process();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private byte[] Process()
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
                        ResultType = ResultType.Byte
                    };
                    break;
                case "POST":
                    items = new HttpItems
                    {
                        Method = Method,
                        Url = URL,
                        Postdata = PostData,
                        ResultType = ResultType.Byte
                    };
                    break;
            }
            HttpResults httpResults = httpHelpers.GetHtml(items);
            return httpResults.ResultByte;
        }
    }
}
