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
        public ActionResult Edit(int id)
        {
            return Json("Error");
        }

        // POST: BangumiMemo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return Json("Error");
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