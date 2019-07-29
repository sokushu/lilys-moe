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
using BangumiProjectDBServices.PageModels;
using Microsoft.AspNetCore.Authorization;
using BangumiProject.Controllers;

namespace BangumiProject.Areas.HomeBar.Controllers
{
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
            IAuthorizationService AuthorizationService
            )
            :base(
                 DBServices: _DBCORE, 
                 RoleManager: _roleManager, 
                 UserManager: _UserManager,
                 SignInManager: _SignInManager,
                 AuthorizationService: AuthorizationService
                 ) {}

        /// <summary>
        /// 显示首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/", Name = Final.Route_Index)]
        public IActionResult GetIndex()
        {
            // 模式初始化
            Init(LoadMode.YuriMode, LoadMode.UIMode, LoadMode.SignIn);
            // 初始化显示页面
            InitView("Index");
            Index index = new Index();
            switch (IMode)
            {
                case UIMode.YuriMode_:
                case UIMode.YuriMode_Shojo:
                case UIMode.YuriMode_G:
                    List<Anime> YuriAnimeNew4 = new List<Anime>();
                    if (Ccommon.UI.New_4Anime)
                    {
                        //得到含有百合标签的最新4部动画
                        HashSet<string> YuriTags = YuriName.ToHashSet();
                        YuriAnimeNew4 = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4_Yuri(),
                            db => db.Include(anime => anime.Tags).Where(anime => anime.Tags.FirstOrDefault(tag => YuriTags.Contains(tag.TagName)) != null));
                    }
                    if (Ccommon.UI.YuriGoods)
                    {

                    }
                    if (Ccommon.UI.YuriInfo)
                    {

                    }
                    if (Ccommon.UI.YuriNews)
                    {

                    }
                    if (Ccommon.UI.BackPic)
                    {

                    }
                    if (Ccommon.UI.NewBangumiTime)
                    {
                        //不加载新番时间表
                        //因为动画在完结之前我们不会知道这到底是不是百合动画，所以暂时没有未完结动画的数据
                    }
                    index.Animes = YuriAnimeNew4;
                    break;
                case UIMode.Normal_:
                    List<Anime> animes = new List<Anime>();
                    List<Anime> SAnime = new List<Anime>();
                    List<List<Anime>> weeks = new List<List<Anime>>();
                    if (Ccommon.UI.NewBangumiTime)
                    {
                        //得到未完结的动画
                        SAnime = DBServices.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                        //对未完结动画分成星期一，星期二的形式
                        WeekSwitch WeekSwitch = new WeekSwitch();
                        weeks = WeekSwitch.SwitchAnime(SAnime, WeekSwitch.SwitchType.Week);
                    }
                    if (Ccommon.UI.New_4Anime)
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
                    index.Animes = animes;
                    index.WeekAnimes = weeks;
                    break;
                case UIMode.Normal_G:
                    List<Anime> animesNormal_G = new List<Anime>();
                    List<Anime> SAnimeNormal_G = new List<Anime>();
                    List<List<Anime>> weeksNormal_G = new List<List<Anime>>();
                    if (Ccommon.UI.New_4Anime)
                    {
                        //得到最新的4部动画
                        animesNormal_G = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4(), db => db.OrderByDescending(anime => anime.Time).Take(4));
                    }
                    if (Ccommon.UI.YuriGoods)
                    {

                    }
                    if (Ccommon.UI.YuriInfo)
                    {

                    }
                    if (Ccommon.UI.YuriNews)
                    {

                    }
                    if (Ccommon.UI.BackPic)
                    {

                    }
                    if (Ccommon.UI.NewBangumiTime)
                    {
                        //得到未完结的动画
                        SAnimeNormal_G = DBServices.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                        //对未完结动画分成星期一，星期二的形式
                        WeekSwitch WeekSwitchNormal_G = new WeekSwitch();
                        weeksNormal_G = WeekSwitchNormal_G.SwitchAnime(SAnimeNormal_G, WeekSwitch.SwitchType.Week);
                    }
                    index.Animes = animesNormal_G;
                    index.WeekAnimes = weeksNormal_G;
                    break;
                default:
                    break;
            }
            Model = Tuple.Create(index, Ccommon);
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
        [Route("/About.jsp", Name = "About")]
        public IActionResult GetAbout()
        {
            return View("About");
        }

        /// <summary>
        /// 提交问题的方法
        /// </summary>
        /// <param name="wenti"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/About.jsp")]
        public IActionResult PostAbout(string wenti)
        {
            
            return Json("谢谢你的问题，我已经记下了，现在你可以关闭页面了");
        }
    }
}