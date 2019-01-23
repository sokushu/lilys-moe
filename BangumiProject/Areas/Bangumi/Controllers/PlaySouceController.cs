using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Areas.Bangumi.ParamsModels;
using BangumiProject.Areas.Bangumi.Views.PlaySouce.Model;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 添加播放元
    /// </summary>
    [Area("Bangumi")]
    public class PlaySouceController : Controller
    {
        private readonly ICommDB _DBServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public PlaySouceController(
            ICommDB _DBServices,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._DBServices = _DBServices;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }
        // GET: PlaySouce
        public ActionResult Index()
        {
            return View();
        }

        [Route("/Souce/{id:int}", Name = Final.Route_PlaySouce_Details)]
        // GET: PlaySouce/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PlaySouce/Create
        [Authorize(Policy = Final.Yuri_Yuri5)]
        [Route("/Souce/Add", Name = Final.Route_PlaySouce_Create)]
        public ActionResult Create()
        {
            return View(
                viewName:"Create"
                );
        }

        // POST: PlaySouce/Create
        [HttpPost]
        [Authorize(Policy = Final.Yuri_Yuri5)]
        [ValidateAntiForgeryToken]
        [Route("/Souce/Add", Name = Final.Route_PlaySouce_Create_POST)]
        public ActionResult Create(CreatePlaySouce playSouce)
        {
            try
            {
                string PicPath = playSouce.SitePic ?? string.Empty;
                if (playSouce.Pic != null)
                {
                    //上传图片，并获取图片路径
                    PicPath = "";
                }

                AnimeSouce souce = new AnimeSouce
                {
                    Name = playSouce.SiteName,
                    Info = playSouce.Info,
                    URL = playSouce.SiteURL,
                    Pic = PicPath
                };

                //将数据保存
                _DBServices.AddAsync(souce);

                return RedirectToRoute(Final.Route_PlaySouce_Details, souce.ID);
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaySouce/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PlaySouce/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                var a = collection.Files[0];
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlaySouce/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PlaySouce/Delete/5
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