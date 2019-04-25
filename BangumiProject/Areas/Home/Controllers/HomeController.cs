using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BangumiProject.Areas.Bangumi.Process;
using BangumiProject.Areas.HomeBar.Views.Home.Model;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;

namespace BangumiProject.Areas.HomeBar.Controllers
{
    [Area("HomeBar")]
    public class HomeController : Controller
    {
        /// <summary>
        /// 用户数据库，以及工具的初始化
        /// </summary>
        private readonly IServices _DBCORE;
        private readonly RoleManager<IdentityRole> _roleManager;
        public HomeController(
            IServices _DBCORE, 
            RoleManager<IdentityRole> _roleManager)
        {
            this._roleManager = _roleManager;
            this._DBCORE = _DBCORE;
        }

        /// <summary>
        /// 
        /// </summary>
        private bool YuriMode { get; set; } = false;

        /// <summary>
        /// 返回首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/", Name = Final.Route_Index)]
        public async Task<IActionResult> GetIndex()
        {
            //
            YuriMode = HttpContext.YuriModeCheck();
            UIMode iMode = UIMode.Normal_;
            switch (iMode)
            {
                case UIMode.YuriMode_:
                    break;
                case UIMode.YuriMode_Shojo:
                    break;
                case UIMode.YuriMode_G:
                    break;
                case UIMode.Normal_:
                    //得到最新的4部动画
                    List<Anime> animes = _DBCORE.Save_ToList<Anime>(CacheKey.Anime_New4(), db => db.OrderByDescending(anime => anime.Time).Take(4));
                    //得到未完结的动画
                    List<Anime> SAnime = _DBCORE.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                    //对未完结动画分成星期一，星期二的形式
                    WeekSwitch WeekSwitch = new WeekSwitch();
                    var weeks = WeekSwitch.SwitchAnime(SAnime, WeekSwitch.SwitchType.Week);
                    //读取博客
                    //读取新闻

                    //测试用功能
                    var AllUsers = await _DBCORE.UserManager.Users.ToListAsync();
                    var User = await _DBCORE.UserManager.GetUserAsync(HttpContext.User);
                    if (User != null)
                    {
                        var UserYuri = await _DBCORE.UserManager.GetRolesAsync(User);
                        var Role = UserYuri.FirstOrDefault();
                        switch (Role)
                        {
                            case Final.Yuri_Admin:
                            // 显示管理员的界面
                            case Final.Yuri_Girl:
                            case Final.Yuri_Yuri5:  // 较高的权限
                                                    //显示添加动画的连接

                                break;
                            case Final.Yuri_Yuri4:
                            case Final.Yuri_Yuri3:
                            case Final.Yuri_Yuri2:
                            case Final.Yuri_Yuri1:  // 普通的权限

                                break;
                            case Final.Yuri_Boy:    // 猪狗不如的权限

                                break;
                            default:                // 用户没有权限？滚开！！拒绝访问！！！
                                                    // 当然，正常情况是走不到这里的(*^_^*)
                                return StatusCode(Final.StatusCode403);
                        }
                    }
                    //进行渲染
                    return PartialView("Index", new Index
                    {
                        Animes = animes,
                        WeekAnimes = weeks,
                        AllUsers = AllUsers
                    });
                case UIMode.Normal_G:
                    break;
                default:
                    break;
            }
            return View();
        }

        /// <summary>
        /// 返回关于页面
        /// 
        /// 返回一个Jsp格式的关于页面
        /// 
        /// 这个页面可以做一个纪念
        /// 作为第一个版本的纪念
        /// 
        /// 第一个版本是用Java写的，
        /// 第二个版本使用Springboot写的
        /// 第三个版本就是现在看到的C# NetCore写的版本
        /// 
        /// </summary>
        /// <returns>返回渲染的视图</returns>
        [HttpGet]
        [Route("/About.jsp", Name = "About")]
        public IActionResult GetAbout()
        {
            //ReadConfig readConfig = new ReadConfig();
            //ViewData["About"] = readConfig;
            return View("About");
        }

        //[HttpPost]
        //[Route("/About.jsp")]
        //public IActionResult PostAbout(string wenti)
        //{
        //    ReadConfig readConfig = new ReadConfig();
        //    readConfig.Add($"问题：{readConfig.Count}号", $"{wenti} || Time:{DateTime.Now}");
        //    return Json("谢谢你的问题，我已经记下了，现在你可以关闭页面了");
        //}
    }
}