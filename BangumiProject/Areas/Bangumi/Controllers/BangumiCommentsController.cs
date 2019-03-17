using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Area("Bangumi")]
    public class BangumiCommentsController : Controller
    {
        private readonly IServices _DBCORE;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiCommentsController(
            IServices _DBCORE,
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