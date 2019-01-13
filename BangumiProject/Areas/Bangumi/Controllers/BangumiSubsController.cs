using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Services;
using User = BangumiProject.Areas.Users.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BangumiProject.Areas.Bangumi.Models;
using Microsoft.AspNetCore.Cors;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    public class BangumiSubsController : Controller
    {
        private readonly ICommDB _DBServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiSubsController(
            ICommDB _DBServices,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._userManager = _userManager;
            this._DBServices = _DBServices;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }
        // GET: BangumiSubs
        public ActionResult Index()
        {
            return View();
        }

        // GET: BangumiSubs/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BangumiSubs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BangumiSubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: BangumiSubs/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BangumiSubs/Edit/5
        [HttpPost]
        [EnableCors("Test")]
        [Route("/BangumiSub{animeid:int}")]
        public async Task<ActionResult> EditAsync(int animeid, int AnimeNum = -1)
        {
            try
            {
                if (_DBServices.HasAnimeID(animeid))
                {
                    //订阅动画
                    if (animeid != -1 && AnimeNum == -1)
                    {
                        var UID = _userManager.GetUserId(HttpContext.User);
                        var info = await _DBServices.GetFirstAsync<AnimeUserInfo>(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == animeid);
                        if (info == null)
                        {
                            var Anime = await _DBServices.GetFirstAsync<Anime>(anime => anime.AnimeID == animeid);
                            var User = await _userManager.GetUserAsync(HttpContext.User);
                            await _DBServices.AddAsync(new AnimeUserInfo
                            {
                                NowAnimeNum = 0,
                                SubAnime = Anime,
                                Users = User
                            });
                            List<string> json = new List<string> { "true", "订阅成功" };
                            return Json(json);
                        }
                        else
                        {
                            List<string> json = new List<string> { "false", "您已经定阅过" };
                            return Json(json);
                        }
                    }
                    //更新集数
                    if (AnimeNum != -1 && animeid != -1)
                    {
                        // 对传送过来的动画集数做检查
                        if (1 > AnimeNum)
                            return new JsonResult("Error");
                        var UID = _userManager.GetUserId(HttpContext.User);
                        var info = await _DBServices.GetFirstAsync<AnimeUserInfo>(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == animeid);
                        if (info == null)
                        {
                            return Json(new List<string> { "false", "没有订阅动画，请先订阅动画" });
                        }
                        info.NowAnimeNum = AnimeNum;
                        await _DBServices.UpdateAsync(info);
                        return Json(new List<string> { "true", "更改成功" });
                    }
                }
                return Json("Error");
            }
            catch
            {
                return Json("Error");
            }
        }

        // GET: BangumiSubs/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BangumiSubs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}