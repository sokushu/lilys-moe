using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 这个是给动画打分的
    /// </summary>
    [Area("Bangumi")]
    public class BangumiMarkingController : Controller
    {
        // GET: BangumiMarking
        public ActionResult Index()
        {
            return View();
        }

        // GET: BangumiMarking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BangumiMarking/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BangumiMarking/Create
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

        // GET: BangumiMarking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BangumiMarking/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        // GET: BangumiMarking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BangumiMarking/Delete/5
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