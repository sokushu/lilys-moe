using HtmlAgilityPack;
using HttpCode.Core;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using BangumiProject.Process.MoeMushi.Interface;
using System.Net;
using BangumiProject.Process.MoeMushi.Model;
using BangumiProject.Models;
using Microsoft.EntityFrameworkCore;
using BangumiProject.Process.MoeMushi.DataSource;
using BangumiProject.Process.MoeMushi.Process;
using System.Reflection;

namespace BangumiProject.Process.MoeMushi
{
    /// <summary>
    /// 
    /// </summary>
    public class Mushi
    {
        
        /// <summary>
        /// 终止标志,
        /// 如果是true，则代表爬虫已经执行完毕停止运行了
        /// </summary>
        public static bool IsEnd { get; set; } = true;

        /// <summary>
        /// 关于MoeMushi的介绍
        /// 
        /// 这个爬虫共分四部分，
        /// 1，Html解析器
        /// 2，下载器
        /// 3，URL管理器
        /// 4，调度管理
        /// 
        /// 调度就是开始整个程序的，管理用。
        /// 
        /// 解析就是把解析的Html分出有用的信息，如果需要下载就交给下载器，例如
        /// 我需要的文本信息，交给下载器，下载器会保存到文件中，或是数据库
        /// 如果是URL就会交给URL管理器进行管理
        /// 
        /// 下载器，就是把数据保存到数据库或文件中，或是下载图片等
        /// 
        /// URL管理就是管理解析出来的URL，哪些需要下载，那些需要继续解析
        /// 要继续解析的就交给解析器
        /// 
        /// =================================================================
        /// 
        /// 我们这个爬虫所做的事情：
        /// 
        /// 爬虫应当是网站的核心部分。
        /// 我们的爬虫全自动的收集动画信息。并对网站内容进行全自动的更新。
        /// 
        /// 详细任务描述
        /// 我们的爬虫责任重大！！！
        /// 要负责的做的事情很多。
        /// 
        /// 1.在各大网站抓取动画数据
        ///     例如音乐制作，监督，作画，声优，原画，集数等
        ///     抓取数据并写入到数据库中。
        /// 2.抓取网站实时更新的数据
        ///     例如新番播放页面，动画目前播放的集数等
        ///     再例如本站连接到B站的链接，B站已经看不了了，就需要我们的爬虫登场了。检查链接可用性，更新链接等
        /// 3.网站整体数据的更新管理
        ///     
        /// 主要的工作就是以上的三点
        /// 
        /// =================================================================
        /// 
        /// </summary>
        /// <see cref="https://github.com/stulzq/HttpCode.Core"/>
        /// <param name="mushiParams"></param>
        public Task Start(MoeMushiContext _MoeMushiDB)
        {
            if (jsonAnalyzer == null)
                ThrowNullReferenceException();
            IDownLoad downLoad;
            if (Down != null)
            {
                //使用自定义的
                downLoad = Down;
            }
            else
            {
                //使用默认的
                downLoad = new HttpGet();
            }
            //返回的是Html或是Json串
            string HtmlorJson = downLoad.HttpHtml(Url.GetURL());
            var assem = Assembly.GetExecutingAssembly();
            //创建对象并解析
            dynamic obj = assem.CreateInstance(jsonAnalyzer.FullName, true, BindingFlags.Default, null, new object[] { HtmlorJson }, null, null);
            var data = obj.Data;//获取解析之后的数据集

            //对解析的数据进行进一步处理

            return Task.Run(() =>
            {
                var aw = BILIBILI_Timeline_Global(_MoeMushiDB);
                //全部执行完毕
                IsEnd = true;
            });
        }
        /// <summary>
        /// 添加下载器
        /// </summary>
        public IDownLoad Down { set; private get; } = null;
        public IUrl Url { private get; set; } = new UrlKuBaRu();
        private Type jsonAnalyzer { get; set; } = null;
        public Mushi AddUrl(string url)
        {
            Url.AddURL(url);
            return this;
        }
        private void ThrowNullReferenceException()
        {
            throw new NullReferenceException();
        }
        public Mushi SetJsonAnalyzer<T>() where T : class
        {
            jsonAnalyzer = typeof(T);
            return this;
        }

        /// <summary>
        /// 得到B站的新番时间表
        /// </summary>
        /// <param name="_MoeMushiDB">数据库连接</param>
        /// <returns></returns>
        private Task BILIBILI_Timeline_Global(MoeMushiContext _MoeMushiDB)
        {
            return Task.Run(async ()=> 
            {
                IsEnd = false;
                HttpHelpers httpHelpers = new HttpHelpers();

                HttpItems httpItems = new HttpItems
                {
                    Url = BILIBILI.BILIBILI_Timeline_Global(),
                    Method = HttpMethod.Get.Method
                };

                HttpResults results = httpHelpers.GetHtml(httpItems);

                HttpStatusCode code = results.StatusCode;

                if (code != HttpStatusCode.OK)
                {
                    //这里放个提醒，嘿，链接可能失效
                    //这里写入数据库中比较好
                }
                else
                {
                    //将得到的数据转换
                    JObject json = JsonConvert.DeserializeObject<JObject>(results.Html);
                    HashSet<int> hash = new HashSet<int>();
                    Dictionary<string, List<TimeLineInfo>> Data = new Dictionary<string, List<TimeLineInfo>>();

                    //是否成功？（success）
                    string IsSuccess = json["message"].ToString();
                    //提取数据
                    JToken jToken = json["result"];
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

                    //先清除旧数据
                    _MoeMushiDB.TimeLines.FromSql("DELETE FROM TimeLineAnimeInfos");
                    foreach (var item in Data.Keys)
                    {
                        _MoeMushiDB.TimeLines.UpdateRange(Data[item]);
                    }
                    _MoeMushiDB.SaveChanges();

                    //先清除旧数据
                    _MoeMushiDB.AnimeInfoSeas.FromSql("DELETE FROM AnimeInfos");
                    //获取season_id的数据
                    foreach (int season_id in hash)
                    {
                        List<AnimeInfoSea> animeInfos = await Bilibili_Animeinfo(season_id, _MoeMushiDB);
                        _MoeMushiDB.AnimeInfoSeas.UpdateRange(animeInfos);
                        await Task.Delay(5000);//停5秒，防止过快被反爬虫
                    }
                    _MoeMushiDB.SaveChanges();
                }
            });
        }

        /// <summary>
        /// 得到指定动画的剧集信息
        /// </summary>
        /// <param name="season_id"></param>
        /// <param name="_MoeMushiDB">数据库连接</param>
        /// <returns></returns>
        public Task<List<AnimeInfoSea>> Bilibili_Animeinfo(int season_id, MoeMushiContext _MoeMushiDB)
        {
            return Task.Run(()=> 
            {
                HttpHelpers httpHelpers = new HttpHelpers();
                HttpItems httpItems = new HttpItems
                {
                    Url = BILIBILI.BILIBILI_AnimeNumInfo(season_id: season_id),
                    Method = HttpMethod.Get.Method
                };
                HttpResults results = httpHelpers.GetHtml(httpItems);
                List<AnimeInfoSea> list = new List<AnimeInfoSea>();
                if (results.StatusCodeNum != 200)
                {
                    _MoeMushiDB.LogInfos.Add(new _LogInfo
                    {
                        Info = $"{season_id} results.StatusCodeNum 出现问题 返回代码：{results.StatusCodeNum}",
                        Time = DateTime.Now
                    });
                    _MoeMushiDB.SaveChanges();
                }
                else
                {
                    JObject Json = JsonConvert.DeserializeObject<JObject>(results.Html);

                    var j = Json["result"]?["main_section"]?["episodes"];
                    if (j != null)
                    {
                        int AnimeNumber = 1;
                        foreach (var Info in j)
                        {
                            AnimeInfoSea animeInfo = new AnimeInfoSea
                            {
                                AnimeNum = AnimeNumber,
                                Aid = (int)(Info["aid"] ?? -1),
                                Cid = (int)(Info["cid"] ?? -1),
                                Cover = (string)Info["cover"],
                                Id = (int)(Info["id"] ?? -1),
                                Long_title = (string)Info["long_title"],
                                Share_url = (string)Info["share_url"],
                                Season_id = season_id,
                                Stat_play = (int)(Info["stat"]?["play"] ?? -1),
                                Title = (string)Info["title"]
                            };
                            list.Add(animeInfo);
                            AnimeNumber++;
                        }
                    }
                    else
                    {
                        _MoeMushiDB.LogInfos.Add(new _LogInfo
                        {
                            Info = $"{season_id} Json['result']?['main_section']?['episodes'] 出现问题",
                            Time = DateTime.Now
                        });
                        _MoeMushiDB.SaveChanges();
                    }
                }
                return list;
            });
        }
    }
}
