using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Controllers;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;
using Microsoft.EntityFrameworkCore;
namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 这个是动画的Memo
    /// </summary>
    [Area("Bangumi")]
    public class BangumiMemoController : Controller
    {
        private readonly ICommDB _DBServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiMemoController(
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
        // GET: BangumiMemo
        public ActionResult Index()
        {
            return Json("Error");
        }

        /// <summary>
        /// 返回一个超详细的Memo信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: BangumiMemo/Details/5
        public ActionResult Details(int id)
        {
            return Json("Error");
        }

        [HttpGet]
        [Authorize]
        [Route("/Bangumi/Memo/Create{animeid:int}")]
        public ActionResult Create()
        {
            //返回一个专门的添加页面
            return View(
                viewName:"Memo"
                );
        }

        /// <summary>
        /// 添加一个Memo
        /// </summary>
        /// <param name="animeid"></param>
        /// <param name="animeMemo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("/Bangumi/Memo/Create{animeid:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(int animeid, AnimeMemo animeMemo)
        {
            try
            {
                if (_DBServices.HasAnimeID(animeid))
                {
                    var uid = _userManager.GetUserId(HttpContext.User);
                    var Info = await _DBServices.GetFirstAsync<AnimeUserInfo>(info => info.SubAnime.AnimeID == animeid && info.Users.Id == uid);
                    if (Info != null)
                    {
                        Info.Memos.Add(animeMemo);
                        await _DBServices.UpdateAsync(Info);
                    }
                    //没有订阅，不能添加
                    //最终返回到动画详细页面
                    return RedirectToRoute(Final.Route_Bangumi_Details, animeid);
                }
                return Json("Error");
            }
            catch
            {
                return Json("Error");
            }
        }

        // GET: BangumiMemo/Edit/5
        [Authorize]
        [Route("/Bangumi/Memo/Edit{memoid:int}")]
        public async Task<ActionResult> EditAsync(int memoid)
        {
            var Memo = await _DBServices.GetFirstAsync<AnimeMemo>(memo => memo.ID == memoid);
            if (Memo != null)
            {
                return View(
                    viewName:"Memo",
                    model:new AnimeMemo
                    {
                        MemoStr = Memo.MemoStr,
                        NowAnimeNum = Memo.NowAnimeNum
                    }
                    );
            }
            return RedirectToRoute("Index");
        }

        // POST: BangumiMemo/Edit/5
        [HttpPost]
        [Authorize]
        [Route("/Bangumi/Memo/Edit{memoid:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int memoid, AnimeMemo animeMemo)
        {
            try
            {
                var Memo = await _DBServices.GetDateOneAsync<AnimeMemo>(db => db.Include(me => me.UserAnimeInfo).ThenInclude(info => info.SubAnime).Where(memo => memo.ID == memoid));
                if (Memo != null)
                {
                    Memo.NowAnimeNum = animeMemo.NowAnimeNum;
                    Memo.MemoStr = animeMemo.MemoStr;
                    await _DBServices.UpdateAsync(Memo);
                    //返回动画页面
                    return RedirectToRoute(Final.Route_Bangumi_Details, Memo.UserAnimeInfo.SubAnime.AnimeID);
                }
                return RedirectToRoute("Index");
            }
            catch
            {
                return Json("Error");
            }
        }

        // GET: BangumiMemo/Delete/5
        public ActionResult Delete(int id)
        {
            return Json("Error");
        }

        // POST: BangumiMemo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return Json("Error");
            }
            catch
            {
                return Json("Error");
            }
        }
    }
}