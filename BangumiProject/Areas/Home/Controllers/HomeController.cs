using BangumiProject.Areas.Bangumi.Process;
using BangumiProject.Areas.Home.Views.Home;
using BangumiProject.Controllers;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels.Core;
using BangumiProjectDBServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BangumiProject.Areas.HomeBar.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Home")]
    public class HomeController : BaseController
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="_DBCORE"></param>
        /// <param name="_roleManager"></param>
        public HomeController(
            IServices _DBCORE,
            RoleManager<IdentityRole> _roleManager,
            UserManager<User> _UserManager,
            SignInManager<User> _SignInManager,
            IAuthorizationService AuthorizationService,
            IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider
            )
            : base(
                 DBServices: _DBCORE,
                 RoleManager: _roleManager,
                 UserManager: _UserManager,
                 SignInManager: _SignInManager,
                 AuthorizationService: AuthorizationService
                 )
        { }

        /// <summary>
        /// 显示首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/", Name = Final.Route_Home_Index)]
        public IActionResult GetIndex()
        {
            //模式初始化
            Init<IndexModel>("Index");
            switch (IMode)
            {
                case UIMode.YuriMode_:
                case UIMode.YuriMode_Shojo:
                case UIMode.YuriMode_G:
                    List<Anime> YuriAnimeNew4 = new List<Anime>();
                    if (Model.UI.New_4Anime)
                    {
                        //得到含有百合标签的最新4部动画
                        HashSet<string> YuriTags = YuriName.ToHashSet();
                        YuriAnimeNew4 = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4_Yuri(),
                            db => db.Include(anime => anime.Tags).Where(anime => anime.Tags.FirstOrDefault(tag => YuriTags.Contains(tag.TagName)) != null));
                    }
                    if (Model.UI.YuriGoods)
                    {

                    }
                    if (Model.UI.YuriInfo)
                    {

                    }
                    if (Model.UI.YuriNews)
                    {

                    }
                    if (Model.UI.BackPic)
                    {

                    }
                    if (Model.UI.NewBangumiTime)
                    {
                        //不加载新番时间表
                        //因为动画在完结之前我们不会知道这到底是不是百合动画，所以暂时没有未完结动画的数据
                    }
                    ((IndexModel)Model).Animes = YuriAnimeNew4;
                    break;
                case UIMode.Normal_:
                    List<Anime> animes = new List<Anime>();
                    List<Anime> SAnime = new List<Anime>();
                    List<List<Anime>> weeks = new List<List<Anime>>();
                    if (Model.UI.NewBangumiTime)
                    {
                        //得到未完结的动画
                        SAnime = DBServices.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                        //对未完结动画分成星期一，星期二的形式
                        WeekSwitch WeekSwitch = new WeekSwitch();
                        weeks = WeekSwitch.SwitchAnime(SAnime, WeekSwitch.SwitchType.Week);
                    }
                    if (Model.UI.New_4Anime)
                    {
                        //得到最新的4部动画
                        animes = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4(), db => db.OrderByDescending(anime => anime.Time).Take(4));
                    }
                    if (IsSignIn)
                    {
                        switch (YURI_TYPE)
                        {
                            case Final.YURI_TYPE.Yuri_Boy:// 猪狗不如的权限，封禁禁言等
                                return Forbid();
                            default:
                                break;
                        }
                    }
                    ((IndexModel)Model).Animes = animes;
                    ((IndexModel)Model).WeekAnimes = weeks;
                    break;
                case UIMode.Normal_G:
                    List<Anime> animesNormal_G = new List<Anime>();
                    List<Anime> SAnimeNormal_G = new List<Anime>();
                    List<List<Anime>> weeksNormal_G = new List<List<Anime>>();
                    if (Model.UI.New_4Anime)
                    {
                        //得到最新的4部动画
                        animesNormal_G = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4(), db => db.OrderByDescending(anime => anime.Time).Take(4));
                    }
                    if (Model.UI.YuriGoods)
                    {

                    }
                    if (Model.UI.YuriInfo)
                    {

                    }
                    if (Model.UI.YuriNews)
                    {

                    }
                    if (Model.UI.BackPic)
                    {

                    }
                    if (Model.UI.NewBangumiTime)
                    {
                        //得到未完结的动画
                        SAnimeNormal_G = DBServices.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                        //对未完结动画分成星期一，星期二的形式
                        WeekSwitch WeekSwitchNormal_G = new WeekSwitch();
                        weeksNormal_G = WeekSwitchNormal_G.SwitchAnime(SAnimeNormal_G, WeekSwitch.SwitchType.Week);
                    }
                    ((IndexModel)Model).Animes = animesNormal_G;
                    ((IndexModel)Model).WeekAnimes = weeksNormal_G;
                    break;
                default:
                    break;
            }
            // 最后返回页面
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
        [Route("/About.jsp", Name = Final.Route_Home_About)]
        public IActionResult GetAbout()
        {
            Init<AboutModel>("About");
            return View();
        }

        /// <summary>
        /// 提交问题的方法
        /// </summary>
        /// <param name="wenti"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/About.jsp", Name = Final.Route_Home_About)]
        public IActionResult PostAbout(AboutModel.InputModel input)
        {
            Init<AboutModel>("About");
            return View();
        }
    }
}