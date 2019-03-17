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
using BangumiProject.DBModels;
using Microsoft.AspNetCore.Cors;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 动画的订阅相关处理
    /// </summary>
    [Area("Bangumi")]
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
            return Json("Error");
        }

        // GET: BangumiSubs/Details/5
        public ActionResult Details(int id)
        {
            return Json("Error");
        }

        // GET: BangumiSubs/Create
        public ActionResult Create()
        {
            return Json("Error");
        }

        // POST: BangumiSubs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return Json("Error");
            }
            catch
            {
                return Json("Error");
            }
        }

        // GET: BangumiSubs/Edit/5
        public ActionResult Edit(int id)
        {
            return Json("Error");
        }

        /// <summary>
        /// 动画订阅与更新
        /// </summary>
        /// <param name="animeid"></param>
        /// <param name="AnimeNum"></param>
        /// <returns></returns>
        // POST: BangumiSub5
        [HttpPost]
        [EnableCors("Test")]
        [Route("/BangumiSub{animeid:int}")]
        public async Task<ActionResult> EditAsync(int animeid, int AnimeNum = -1)
        {
            try
            {
                if (_DBServices.HasAnimeID(animeid))
                {
                    var UID = _userManager.GetUserId(HttpContext.User);
                    if (UID == null)
                        return Json(new List<string> { "false", "请先登录" });
                    //订阅动画
                    if (animeid != -1 && AnimeNum == -1)
                    {
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
                        var info = await _DBServices.GetFirstAsync<AnimeUserInfo>(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == animeid);
                        if (info == null)
                            return Json(new List<string> { "false", "没有订阅动画，请先订阅动画" });
                        info.NowAnimeNum = AnimeNum;
                        await _DBServices.UpdateAsync(info);
                        return Json(new List<string> { "true", "更改成功" });
                    }
                }
                return Json("没有这个动画");
            }
            catch
            {
                return Json("出现未知错误");
            }
        }

        // GET: BangumiSubs/Delete/5
        public ActionResult Delete(int id, int b)
        {
            return Json("Error");
        }

        // POST: BangumiSubDel5
        [HttpPost]
        [EnableCors("Test")]
        [Authorize]//要登陆的
        [Route("/BangumiSubDel{animeid:int}")]
        public async Task<ActionResult> DeleteAsync(int animeid)
        {
            try
            {
                // 取消订阅动画
                if (_DBServices.HasAnimeID(animeid))
                {
                    //未来将改为使用软删除
                    var UID = _userManager.GetUserId(HttpContext.User);
                    var Info = await _DBServices.GetFirstAsync<AnimeUserInfo>(info => info.SubAnime.AnimeID == animeid && info.Users.Id == UID);
                    if (Info != null)
                    {
                        await _DBServices.RemoveFromDBAsync(Info);
                        return Json(new List<string> { "true", "成功取消" });
                    }
                    return Json(new List<string> { "true", "动画未订阅" });
                }
                return Json("Error");
            }
            catch
            {
                return Json("Error");
            }
        }
    }
}