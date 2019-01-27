using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpCode.Core;
using MoeMushi.Analyzer;
using MoeMushi.Interface;
using MoeMushi.Process;

namespace MoeMushi
{
    public class Mushi
    {
        /// <summary>
        /// 
        /// </summary>
        public string URL { get; set; }
        public JObjProcessMap JObjProcessMap { get; set; }
        public JObjProcessModel<string> JObjProcessModel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Task Start()
        {
            return Task.Run(()=> 
            {
                var html = HttpHtml(URL);

                IAnalyzer analyzer = new JsonAnalyzer(html);

                var jObj = analyzer.OutPutJson;
                if (JObjProcessMap != null)
                {
                    JObjProcessMap.Process(analyzer);
                    JObjProcessMap.Save();
                }
                else if(JObjProcessModel != null)
                {
                    JObjProcessModel.Process(analyzer);
                    JObjProcessModel.Save();
                }
                else
                {
                    throw new NullReferenceException("没有设定处理单元");
                }
                


            });
        }

        private string HttpHtml(string url)
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
    }
}
