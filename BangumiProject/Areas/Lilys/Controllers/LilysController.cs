using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Lilys.Controllers
{
    /// <summary>
    /// 
    /// 这是里板块
    /// 
    /// 介绍动画，游戏，图片之类的
    /// http://lilys.moe/lilys
    /// 
    /// 网页不使用外部框架，全部是独立的框架。
    /// 
    /// 我想说的是除了数据共通之外，其他一切都将独立于
    /// 外部的lilys
    /// 
    /// 数据将共通，将打造成超级资源宝库
    /// 
    /// </summary>
    [Area("Lilys")]
    public class LilysController : Controller
    {
        // GET: Lilys
        [Route("/lilys", Name = "")]
        public ActionResult Index()
        {
            return View();
        }

        // GET: Lilys/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Lilys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lilys/Create
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

        // GET: Lilys/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Lilys/Edit/5
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

        // GET: Lilys/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Lilys/Delete/5
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