using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Areas.Users.Models;
using BangumiProject.Models;
using BangumiProject.Process.Bangumi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BangumiProject.Controllers
{
    public class SearchController : Controller
    {

        private readonly UserManager<Users> _userManager;
        private readonly BangumiProjectContext _DB;
        private readonly SignInManager<Users> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMemoryCache _memoryCache;

        private readonly DBCache _dbCache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_userManager"></param>
        /// <param name="_DB"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_authorizationService"></param>
        /// <param name="_memoryCache"></param>
        public SearchController(
                UserManager<Users> _userManager, BangumiProjectContext _DB,
                SignInManager<Users> _signInManager, IAuthorizationService _authorizationService,
                IMemoryCache _memoryCache
            )
        {
            this._userManager = _userManager;
            this._DB = _DB;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
            this._memoryCache = _memoryCache;
            this._dbCache = new DBCache(_memoryCache, _DB);
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="w">搜索的关键字</param>
        /// <param name="tag">搜索的动画标签</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Search", Name = "Search")]
        public async Task<IActionResult> IndexAsync(string w = "", string tag = "")
        {
            List<Anime> ReturnAnime;//返回的数据
            if (tag != "")
            {
                //标签不为空，开始搜索标签
                BangumiFilter filter = new BangumiFilter
                {
                    //设置查询参数
                    AnimeTypeAll = true,
                    TypeName = tag
                };

                //查询缓存
                var AllAnime = await _dbCache.GetValueAsync<List<Anime>>(Final.Cache_AllAnime);
                var AllTags = await _dbCache.GetValueAsync<List<AnimeTag>>(Final.Cache_AllAnimeTags);

                ReturnAnime = filter.Filter(AllAnime, AllTags);
            }
            return View();
        }
    }
}