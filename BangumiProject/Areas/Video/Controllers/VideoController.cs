using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using BangumiProject.Areas.Video.Process;
using BangumiProject.Services;
using System.Timers;
using Microsoft.AspNetCore.StaticFiles;

namespace BangumiProject.Areas.Video.Controllers
{
    /// <summary>
    /// 我的机子每个月
    /// 2T流量
    /// 30M带宽（有点小，不过也支持同时3，4个人看视频了，视频不太大的话）
    /// 
    /// 测试功能，测试一下视频播放
    /// 关键是荒野的科特布奇飞行队（第三飞行少女队精神续作）B站没有啊，我又不想下载，
    /// 可以试试这样子
    /// 
    /// 嘿嘿嘿(●ˇ∀ˇ●)
    /// 
    /// 我想看飞行少女队！！！
    /// 或者脱离B站比较好？反正我有2T流量每个月，不怕！！
    ///
    /// 
    /// =============================================
    /// 
    /// 2019年1月6日，我有一个不得了的想法。
    /// 
    /// 很久很久以前，生物是没有性别之分的吧。也就是，所有的生物的性别
    /// 都是一样的，我们可以认为全部是女性或全部是男性吧。
    /// 
    /// 我们的祖先是流行同性恋的。
    /// 我们好恩爱呀，生个孩子吧。我们的祖先这样说。
    /// 但是我们好像不太方便呢……（毕竟同性啊）
    /// 
    /// 后来我们的祖先恩恩爱爱，为了生孩子，做了不同的分工。
    /// 后来产生了性别……
    /// 
    /// 当然了，是我的脑洞……没有查什么资料，纯想象的产物。如果属实，纯属碰巧……
    /// 
    /// 我想说的是，综上所述，性别的概念也许不会消失掉的，
    /// 即便是男性，或是女性突然消失，或是灭绝之类的。
    /// 
    /// 在同性中还是会产生性别的概念。
    /// 例如攻受之类的，还是会产生一个性别的概念。
    /// 这就是我想说的。
    /// 
    /// 2019年1月6日脑洞结束
    /// 
    /// =============================================
    /// </summary>
    [Area("Video")]
    public class VideoController : Controller
    {
        private readonly ICommDB _DBService;
        /// <summary>
        /// 用于视频的一个集合
        /// </summary>
        private static List<VideoDB> Video = new List<VideoDB>();
        private static HashSet<string> VideoSet = new HashSet<string>();
        
        /// <summary>
        /// 依赖注入
        /// </summary>
        /// <param name="_DBService"></param>
        public VideoController(ICommDB _DBService)
        {
            this._DBService = _DBService;
        }
        /// <summary>
        /// 视频索引页面
        /// </summary>
        /// <returns></returns>
        // GET: Play
        [HttpGet]
        [Route("/Play", Name = Final.Route_Video_Index)]
        public ActionResult Index()
        {
            //读取全部视频
            return View(
                viewName:"Play"
                );
        }

        /// <summary>
        /// 视频播放页面
        /// ^_^还是把视频的各类信息保存到数据库中吧
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Play/v5
        [HttpGet]
        [Route("/Play/Video{id:int}", Name = Final.Route_Video_Details)]
        public ActionResult Details(int id = -1)
        {
            try
            {
                var VideoInfo = Video[id];
                //对视频路径进行一定的处理
                VideoInfo.VideoPath = "";
                return View(
                viewName: "VideoPlay",
                model:VideoInfo
                );
            }
            catch
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// 拿到我们的页面
        /// ！！！！！！！！！！！！！！！！！！！！！！！！
        /// 这个下载的执行依赖Aria2进行下载，使用之前要提前安装
        /// ！！！！！！！！！！！！！！！！！！！！！！！！
        /// </summary>
        /// <returns></returns>
        // GET: Play/Create
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Girl)]
        [Route("/Play/Create", Name = Final.Route_Video_Create)]
        public ActionResult Create()
        {
            return View(
                viewName:"Create"
                );
        }

        /// <summary>
        /// 上传页面，
        /// 说是上传，其实并不是，
        /// 上传多费劲哪！！！
        /// 所以这里是放上一个BT链接，下载链接就会自动下载的下载器
        /// 当下载完成之后，就会放到指定的文件夹中，然后Html5视频播放喽
        /// </summary>
        /// <returns></returns>
        // POST: Play/Create
        [HttpPost]
        [Route("/Play/Create", Name = Final.Route_Video_Create_POST)]
        [Authorize(Policy = Final.Yuri_Girl)]//需要一个比较高的权限。
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormFile torrentFile)
        {
            try
            {
                string name = torrentFile.FileName;
                using (var stream = new FileStream($"{Final.FilePath_DownLoad}{name}", FileMode.Create))
                {
                    torrentFile.CopyTo(stream);
                    stream.Close();
                }
                Task.Run(() =>
                {
                    FileDownLoad downLoad = new FileDownLoad
                    {
                        SavePath = Final.FilePath_DownLoad,
                        ToolPath = @"C:\Users\myweb\Downloads\aria2-1.34.0-win-64bit-build1\aria2-1.34.0-win-64bit-build1\aria2c.exe",
                        TorrentFile = $"{Final.FilePath_DownLoad}{name}"
                    };
                    downLoad.Start();
                });
                //返回首页
                return RedirectToRoute("Index");
            }
            catch
            {
                return Json("下载错误，请重新上传文件");
            }
        }

        // GET: Play/Edit/5
        [HttpGet]
        [Route("/Play/Edit/Video{id:int}", Name = Final.Route_Video_Edit)]
        public ActionResult Edit(int id)
        {
            return NotFound();
        }

        // POST: Play/Edit/5
        [HttpPost]
        [Route("/Play/Edit/Video{id:int}", Name = Final.Route_Video_Edit_POST)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }

        // GET: Play/Delete/5
        [HttpGet]
        [Route("/Play/Delete/Video{id:int}", Name = Final.Route_Video_Delete)]
        [Authorize(Policy = Final.Yuri_Girl)]
        public ActionResult Delete(int id)
        {
            return NotFound();
        }

        // POST: Play/Delete/5
        [HttpPost]
        [Route("/Play/Delete/Video{id:int}", Name = Final.Route_Video_Delete_POST)]
        [Authorize(Policy = Final.Yuri_Girl)]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return NotFound();
            }
            catch
            {
                return NotFound();
            }
        }
    }

    /// <summary>
    /// 视频的一些基本信息
    /// </summary>
    public struct VideoDB
    {
        public string Name { get; set; }
        public string Info { get; set; }
        public DateTime Time { get; set; }
        public string VideoPath { get; set; }
    }
}