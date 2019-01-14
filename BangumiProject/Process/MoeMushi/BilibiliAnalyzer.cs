using BangumiProject.Process.MoeMushi.Model;
using BangumiProject.Process.MoeMushi.Process;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi
{
    public class BilibiliAnalyzer : JsonAnalyzerByMyself<Dictionary<string, List<TimeLineInfo>>>
    {
        public BilibiliAnalyzer(string Json) : base(Json){}

        /// <summary>
        /// 进行解析
        /// </summary>
        /// <param name="JsonObj"></param>
        /// <returns></returns>
        public override Dictionary<string, List<TimeLineInfo>> JsonAnalyzer(JObject JsonObj)
        {
            HashSet<int> hash = new HashSet<int>();
            Dictionary<string, List<TimeLineInfo>> Data = new Dictionary<string, List<TimeLineInfo>>();

            //是否成功？（success）
            string IsSuccess = JsonObj["message"].ToString();
            //提取数据
            JToken jToken = JsonObj["result"];
            //开始遍历天
            foreach (var Day in jToken)
            {
                List<TimeLineInfo> list = new List<TimeLineInfo>();
                //开始遍历动画
                foreach (var animeInfo in Day["seasons"])
                {
                    int season_id = (int)animeInfo["season_id"];
                    hash.Add(season_id);
                    TimeLineInfo timeLineAnimeInfo = new TimeLineInfo
                    {
                        Date = DateTime.Parse((string)Day["date"]),
                        Cover = (string)animeInfo["cover"],
                        Ep_id = (int)animeInfo["ep_id"],
                        Favorites = (int)animeInfo["favorites"],
                        Is_published = (int)animeInfo["is_published"] > 0 ? true : false,
                        Pub_index = (string)animeInfo["pub_index"],
                        Pub_time = (string)animeInfo["pub_time"],
                        Season_id = season_id,
                        Square_cover = (string)animeInfo["square_cover"],
                        Title = (string)animeInfo["title"],
                        Delay = (int)animeInfo["delay"] > 0 ? true : false,
                        Delay_index = (string)animeInfo["delay_index"],
                        Delay_reason = (string)animeInfo["delay_reason"]
                    };
                    list.Add(timeLineAnimeInfo);
                }
                Data.Add((string)Day["date"], list);
            }
            return Data;
        }
    }
}
