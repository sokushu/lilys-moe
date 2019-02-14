using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Admin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class AdminSettingController : Controller
    {
        [HttpPost]
        [Route("/WebSetting")]
        public IActionResult Setting(AdminSetting setting)
        {
            return View();
        }

        [HttpGet]
        [Route("/WebSetting")]
        public IActionResult ShowSettingPage()
        {
            AdminSetting setting = new AdminSetting();

            return View(
                viewName:"",
                model:setting
                );
        }

        

    }
}