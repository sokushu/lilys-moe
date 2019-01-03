using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    public class BangumiController : Controller
    {
        // GET: Bangumi
        [HttpGet]
        [Route("/Bangumi", Name = Final.Route_Bangumi_Index)]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Bangumi/Details/5
        [HttpGet]
        [Route("/Bangumi/Details/{id}", Name = Final.Route_Bangumi_Details)]
        public ActionResult Details(int id = -1)
        {
            return View();
        }

        // GET: Bangumi/Create
        [HttpGet]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bangumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
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
        [Route("/Bangumi/Edit/{id}", Name = Final.Route_Bangumi_Edit)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bangumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Bangumi/Edit/{id}", Name = Final.Route_Bangumi_Edit_POST)]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Bangumi/Delete/5
        [HttpGet]
        [Route("/Bangumi/Delete/{id}", Name = Final.Route_Bangumi_Delete)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bangumi/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/Bangumi/Delete/{id}", Name = Final.Route_Bangumi_Delete_POST)]
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