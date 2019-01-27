using System;
using System.Collections.Generic;
using System.Text;
using MoeMushi.Interface;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Bson.Converters;
using Newtonsoft.Json;

namespace MoeMushi.Analyzer
{
    public class JsonAnalyzer : IAnalyzer
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="InputJson"></param>
        public JsonAnalyzer(string InputJson)
        {
            Json = InputJson;
        }

        private string Json { get; set; }
        /// <summary>
        /// 用于输出的Json
        /// </summary>
        public JObject OutPutJson => Analyzer(Json);

        /// <summary>
        /// 解析,
        /// 用于用户来实现
        /// </summary>
        /// <returns></returns>
        public JObject Analyzer(string Json)
        {
            return JsonConvert.DeserializeObject<JObject>(Json);
        }
    }
}
