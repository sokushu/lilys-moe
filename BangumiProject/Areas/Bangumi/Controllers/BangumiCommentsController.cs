using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Services.DBServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;
using Blog = BangumiProject.Areas.Blogs.Models.Blogs;
using BangumiProject.Areas.Bangumi.Models;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Bangumi")]
    public class BangumiCommentsController : Controller
    {
        private readonly IDBCore _DBCORE;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiCommentsController(
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
        /// 
        /// </summary>
        /// <param name="animeid"></param>
        /// <param name="Page"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Bangumi/{animeid:int}/Comments", Name = "ff")]
        public IActionResult BangumiComments(int animeid, int Page = 1)
        {
            if (_DBCORE.HasAnimeID(animeid))
            {
                var Comments = _DBCORE.Save_ToList<AnimeComm>(CacheKey.Anime_Comments(animeid), db =>
                    db.Where(ac => ac.Anime.AnimeID == animeid));
                return PartialView();
            }
            return NotFound();
        }
    }
}