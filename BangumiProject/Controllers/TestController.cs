﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BangumiProject.Models;
using BangumiProject.Process;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using BangumiProject.Views.Home;
using MoeUtilsBox.List;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats.Png;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Timers;
using System.Threading;
using MoeUtilsBox.String;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace BangumiProject.Controllers
{
    public class TestController : Controller
    {

        /// <summary>
        /// 用户数据库，以及工具的初始化
        /// </summary>
        private readonly UserManager<Users> _userManager;
        private readonly BangumiProjectContext _DB;
        private readonly RoleManager<IdentityRole> _roleManager;
        //加入内存缓存
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheHelper memoryCacheHelper = new MemoryCacheHelper();
        /// <summary>
        /// 进行初始化
        /// </summary>
        /// <param name="_userManager"></param>
        /// <param name="DB"></param>
        /// <param name="_roleManager"></param>
        public TestController(UserManager<Users> _userManager, BangumiProjectContext _DB, RoleManager<IdentityRole> _roleManager, IMemoryCache memoryCache)
        {
            this._userManager = _userManager;
            this._DB = _DB;
            this._roleManager = _roleManager;
            _cache = memoryCache;
        }

        private string a = "TEST";

        [HttpPost]
        [Route("/Test", Name = Final.Route_Test)]
        public IActionResult Index()
        {
            var u = _cache.Get(HttpContext.User);
            return Json(HttpContext.User);
        }

        [HttpGet]
        [Route("/Test")]
        public async Task<IActionResult> GetIndexAsync(string b)
        {
            return Json(HttpContext.Session.Id);
        }

        public void T(object source, ElapsedEventArgs e)
        {
            Test();
        }

        public Task Test()
        {
            return Task.Run(() => 
            {
                Console.WriteLine("TEST HELLO WORLD");
                Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
                Thread.Sleep(5000);
            });
        }

        [HttpGet]
        [Route("/cache")]
        public IActionResult Cache()
        {
            var animes = _DB.Anime.First(anime => anime.AnimeID == 5);

            _cache.Set("Test", animes);

            return Json("OK");
        }
    }
}