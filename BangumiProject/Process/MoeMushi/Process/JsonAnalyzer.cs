using BangumiProject.Process.MoeMushi.Interface;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Process
{
    public class JsonAnalyzer : IAnalyzer
    {
        /// <summary>
        /// 输入的JSON数据
        /// </summary>
        private string Json { get; }

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="Json"></param>
        public JsonAnalyzer(string Json)
        {
            this.Json = Json;
        }

        /// <summary>
        /// 用于输出数据
        /// </summary>
        public JObject OutPutJson { get; private set; }

        /// <summary>
        /// 进行解析作业
        /// </summary>
        /// <returns></returns>
        public JObject Analyzer()
        {
            var JsonData = JsonConvert.DeserializeObject<JObject>(Json);
            OutPutJson = JsonData;
            return JsonData;
        }
    }
}
