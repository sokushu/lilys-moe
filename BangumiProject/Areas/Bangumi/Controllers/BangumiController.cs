using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    public class BangumiController : Controller
    {
        private readonly ICommDB _DBServices;
        public BangumiController(ICommDB _DBServices)
        {
            this._DBServices = _DBServices;
        }

        // GET: Bangumi
        [HttpGet]
        [Route("/Bangumi", Name = Final.Route_Bangumi_Index)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bangumi/5
        [HttpGet]
        [Route("/Bangumi/{id?}", Name = Final.Route_Bangumi_Details)]
        public ActionResult Details(int id = -1)
        {
            return View();
        }

        // GET: Bangumi/Create
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bangumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create_POST)]
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

        // GET: Bangumi/Edit/5
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Edit/{id?}", Name = Final.Route_Bangumi_Edit)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bangumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Edit/{id?}", Name = Final.Route_Bangumi_Edit_POST)]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Bangumi/Delete/5
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bangumi/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete_POST)]
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