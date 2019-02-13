using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Search.Process;
using BangumiProject.Services.DBServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;
using Blog = BangumiProject.Areas.Blogs.Models.Blogs;
using BangumiProject.Areas.Bangumi.Models;
using static BangumiProject.Areas.Search.Process.BuildIndex;

namespace BangumiProject.Areas.Search.Controllers
{
    [Area("Search")]
    public class SearchController : Controller
    {
        private readonly IDBCore _DBCORE;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public SearchController(
            IDBCore _DBCORE,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._DBCORE = _DBCORE;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="w"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Search")]
        public IActionResult Index(string w)
        {
            var Animes = _DBCORE.Save_ToList<Anime>(CacheKey.Anime_All(), db => db);
            //BuildIndex buildIndex = new BuildIndex(Animes);
            //buildIndex.Init(ProcessType.Anime);

            Process.Search search = new Process.Search();
            search.SearchAnime("Happy");
            return View();
        }
    }
}