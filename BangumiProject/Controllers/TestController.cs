using System;
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
using Users = BangumiProject.Areas.Users.Models.Users;
using BangumiProject.Services;
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Services.DBServices.Interface;

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
        private readonly IDBCore _DBCORE;
        private readonly MemoryCacheHelper memoryCacheHelper = new MemoryCacheHelper();
        private readonly ICommDB _commdb;
        /// <summary>
        /// 进行初始化
        /// </summary>
        /// <param name="_userManager"></param>
        /// <param name="DB"></param>
        /// <param name="_roleManager"></param>
        public TestController(
            ICommDB _commdb, 
            UserManager<Users> _userManager, 
            BangumiProjectContext _DB, 
            RoleManager<IdentityRole> _roleManager, 
            IMemoryCache memoryCache,
            IDBCore _DBCORE
            )
        {
            this._DBCORE = _DBCORE;
            this._userManager = _userManager;
            this._DB = _DB;
            this._roleManager = _roleManager;
            _cache = memoryCache;
            this._commdb = _commdb;
        }

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
            await Task.Run(() => { });
            return View("AAA");
        }

        [HttpGet]
        [Route("/TestGet")]
        public IActionResult Get()
        {
            HttpContext.Session.SetString("Test","Hello");
            return Json("OK");
        }

        public T GetDate<T>(Func<string, T> func)
        {
            T t = func.Invoke("TEST");
            return t;
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
        [Route("/session")]
        public IActionResult Cache()
        {
            //这段代码的作用：
            //读取数据库中第一个数据
            //保存到缓存“Test”
            var Anime = _DBCORE.Save_ToFirst<Anime>("Test", anime => anime);

            _DBCORE.Save_Remove("Test");

            Anime = _DBCORE.Save_ToFirst<Anime>("Test", anime => anime);
            Anime.AnimeName = "Happy Sugar Life";
            _DBCORE.Save_Updata("Test", Anime).Commit();
            object obj = _DBCORE.ToFirst<Anime>(db => db);
            object obj00 = _DBCORE.GetCache<object>("Test");
            return Json(Anime);
        }
    }


}