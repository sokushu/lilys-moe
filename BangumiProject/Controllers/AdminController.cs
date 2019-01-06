//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using System.Timers;
//using BangumiProject.Models;
//using BangumiProject.Process;
//using BangumiProject.Process.BackRun;
//using BangumiProject.Process.MoeMushi;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Users = BangumiProject.Areas.Users.Models.Users;

//namespace BangumiProject.Controllers
//{
//    [Authorize(Policy = Final.Yuri_Admin)]
//    public class AdminController : Controller
//    {
//        // 加载配置文件。。test
//        private readonly ReadConfig _Config = new ReadConfig();
//        private readonly UserManager<Users> _userManager;
//        private readonly BangumiProjectContext _DB;
//        private readonly MoeMushiContext _MoeMushi;
//        /// <summary>
//        /// 定时任务，每24个小时跑一次
//        /// </summary>
//        private const long Time = 1000 * 60 * 60 * 24;
//        //初始化萌虫子
//        private readonly Mushi Mushi = new Mushi();
//        /// <summary>
//        /// 初始化
//        /// </summary>
//        /// <param name="_userManager"></param>
//        /// <param name="_DB">主数据库</param>
//        /// <param name="_MoeMushi">爬虫使用的数据库</param>
//        public AdminController(UserManager<Users> _userManager, BangumiProjectContext _DB, MoeMushiContext _MoeMushi)
//        {
//            this._DB = _DB;
//            this._userManager = _userManager;
//            this._MoeMushi = _MoeMushi;
//            //绑定计时器事件
//            //（还没写好，暂时不开启）
//            //Timer.Elapsed += TimeAsync;
//            //Timer24h.Elapsed += Time24h;
//            //开启计时器
//            //Timer24h.Start();
//        }
//        /// <summary>
//        /// 新建一个计时器
//        /// 用于爬虫任务的执行
//        /// </summary>
//        private Timer Timer = new Timer
//        {
//            Enabled = true,
//            Interval = Time,
//        };
//        /// <summary>
//        /// 这个计时器是用于启动爬虫任务的计时器，
//        /// 当晚上12点左右就会启动爬虫程序
//        /// </summary>
//        private Timer Timer24h = new Timer
//        {
//            Enabled = true,
//            //每15分钟检查一次
//            Interval = 15 * 60 * 1000
//        };
//        /// <summary>
//        /// 一个计时器的委托方法
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="e"></param>
//        public void TimeAsync(object source, ElapsedEventArgs e)
//        {
//            Mushi.Start(_MoeMushi);
//        }
//        /// <summary>
//        /// 检查时间，并触发每24小时执行的后台程序
//        /// </summary>
//        /// <param name="source"></param>
//        /// <param name="e"></param>
//        public void Time24h(object source, ElapsedEventArgs e)
//        {
//            //获取下次更新爬虫数据的时间（第二天的零晨01：00）
//            var Time = DateTime.Parse("01:00");
//            var time = DateTime.Parse(DateTime.Now.AddDays(1).ToShortDateString()).Add(Time.TimeOfDay);
//            var TimeNow = DateTime.Now;
//            if (DateTime.Compare(TimeNow, time) >= 0) //如果现在的日期大于获取下次更新的日期，那就开始吧
//            {
//                //因为是15分钟检查一次当前时间，所以当爬虫启动的时候，时间在00:00 ~ 00:30这个时间段
//                //爬虫启动后，会将爬虫数据库中的信息整理，更新进主数据库中
//                Timer.Start();//打开24小时一次的爬虫计时器（计时开始，只要不关闭服务器，就会每24小时执行一次）
//                Timer24h.Enabled = false;//把这个计时器关闭吧，没用了
//            }
//        }
//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/BackRun", Name = "GetBackRun")]
//        public async Task<IActionResult> GetBackRunAsync()
//        {
//            //得到后台任务设置画面
//            return Json("OK");
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Route("/BackRun", Name = "PostBackRun")]
//        public IActionResult PostBackRun()
//        {
//            //处理后台任务设置信息
//            //后台任务每24个小时执行一次
//            return View();
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Admin", Name = "GetAdminPage")]
//        public IActionResult GetIndex()
//        {
//            // 返回页面，将配置数据载入页面
//            return View(
//                viewName:"",
//                model:_Config
//                );
//        }

//        [HttpPost]
//        [Route("/Admin", Name = "PostAdminPage")]
//        public IActionResult PostIndex()
//        {
            
//            return View();
//        }
//    }
//}