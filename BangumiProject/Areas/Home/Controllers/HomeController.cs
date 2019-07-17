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
        public async Task<IActionResult> GetIndex()
        {
            // 模式初始化
            Init(LoadMode.YuriMode, LoadMode.UIMode, LoadMode.SignIn);
            Index index = new Index();
            Tuple<Index, Common> Model = null;
            switch (IMode)
            {
                case UIMode.YuriMode_:
                case UIMode.YuriMode_Shojo:
                case UIMode.YuriMode_G:
                    List<Anime> YuriAnimeNew4 = new List<Anime>();
                    if (common.UI.New_4Anime)
                    {
                        //得到最新的4部动画
                        //得到含有百合标签的最新4部动画
                        HashSet<string> YuriTags = YuriName.ToHashSet();
                        YuriAnimeNew4 = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4_Yuri(),
                            db => db.Include(anime => anime.Tags).Where(anime => anime.Tags.FirstOrDefault(tag => YuriTags.Contains(tag.TagName)) != null));
                        //因为动画在完结之前我们不会知道这到底是不是百合动画，所以暂时没有未完结动画的数据
                    }
                    if (common.UI.YuriGoods)
                    {

                    }
                    if (common.UI.YuriInfo)
                    {

                    }
                    if (common.UI.YuriNews)
                    {

                    }
                    index.Animes = YuriAnimeNew4;
                    Model = Tuple.Create(index, common);
                    break;
                case UIMode.Normal_:
                    //得到最新的4部动画
                    List<Anime> animes = DBServices.Save_ToList<Anime>(CacheKey.Anime_New4(), db => db.OrderByDescending(anime => anime.Time).Take(4));
                    //得到未完结的动画
                    List<Anime> SAnime = DBServices.Save_ToList<Anime>(CacheKey.Anime_NotEnd(), db => db.Where(anime => anime.IsEnd == false));
                    //对未完结动画分成星期一，星期二的形式
                    WeekSwitch WeekSwitch = new WeekSwitch();
                    var weeks = WeekSwitch.SwitchAnime(SAnime, WeekSwitch.SwitchType.Week);

                    //测试用功能
                    var AllUsers = await UserManager.Users.ToListAsync();
                    var User = await UserManager.GetUserAsync(HttpContext.User);
                    if (User != null)
                    {
                        var UserYuri = await UserManager.GetRolesAsync(User);
                        var Role = UserYuri.FirstOrDefault();
                        switch (Role)
                        {
                            case Final.Yuri_Boy:    // 猪狗不如的权限，封禁禁言等
                                return StatusCode(Final.StatusCode403);
                            default:
                                break;
                        }
                    }
                    index.AllUsers = AllUsers;
                    index.Animes = animes;
                    index.WeekAnimes = weeks;
                    Model = Tuple.Create(index, common);
                    break;
                case UIMode.Normal_G:
                    break;
                default:
                    break;
            }
            // 最后返回页面
            return View("Index", Model);
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