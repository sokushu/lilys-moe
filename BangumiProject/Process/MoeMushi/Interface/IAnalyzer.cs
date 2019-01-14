using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Interface
{
    public interface IAnalyzer
    {
        /// <summary>
        /// 需要解析的Json数据
        /// </summary>
        JObject OutPutJson { get; }

        /// <summary>
        /// 开始解析
        /// </summary>
        JObject Analyzer();
    }
}
