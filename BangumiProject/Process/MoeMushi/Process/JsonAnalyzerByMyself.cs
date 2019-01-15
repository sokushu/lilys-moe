using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Process
{
    public abstract class JsonAnalyzerByMyself<T> : JsonAnalyzer where T : class
    {
        public T Data { get; }
        public JsonAnalyzerByMyself(string JSon) : base(JSon)
        {
            Data = JsonAnalyzer(Analyzer());
        }
        /// <summary>
        /// 可以自定义返回的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public abstract T JsonAnalyzer(JObject JsonObj);
    }
}
