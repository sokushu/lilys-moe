using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Models;
using BangumiProject.Process;
using BangumiProject.Process.Bangumi;
using BangumiProject.Views.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BangumiProject.Controllers
{
    public class ProfileController : Controller
    {
        /// <summary>
        /// 用户数据库，以及工具的初始化
        /// </summary>
        private readonly UserManager<Users> _userManager;
        private readonly BangumiProjectContext _DB;
        private readonly RoleManager<IdentityRole> _roleManager;
        /// <summary>
        /// 进行初始化
        /// </summary>
        /// <param name="_userManager"></param>
        /// <param name="DB"></param>
        /// <param name="_roleManager"></param>
        public ProfileController(UserManager<Users> _userManager, BangumiProjectContext _DB, RoleManager<IdentityRole> _roleManager)
        {
            this._userManager = _userManager;
            this._DB = _DB;
            this._roleManager = _roleManager;
        }

        /// <summary>
        /// 得到一个页面
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Profile/{uid?}", Name = Final.Route_Profile_UID)]
        public async Task<IActionResult> GetIndex(string uid)
        {
            var user = await _userManager.FindByIdAsync(uid);

            var animes = await _DB.UserAnimeInfos.Where(infos => infos.Users.Equals(user))
                .Include(infos => infos.SubAnime)
                .ToListAsync();

            WeekSwitch Switch = new WeekSwitch();
            var AnimeList = Switch.SwitchAnimeByStats(animes);

            return View("Profile", new ProfileModel
            {
                Users = user,
                IsMe = false,
                AnimeInfos = AnimeList
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("/Profile", Name = Final.Route_Profile)]
        public async Task<IActionResult> GetMyProfile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var animes = await _DB.UserAnimeInfos.Where(infos => infos.Users.Equals(user))
                .Include(infos => infos.SubAnime)
                .ToListAsync();

            WeekSwitch Switch = new WeekSwitch();
            var AnimeList = Switch.SwitchAnimeByStats(animes);

            return View("Profile", new ProfileModel
            {
                Users = user,
                IsMe = true,
                AnimeInfos = AnimeList
            });
        }
    }

}