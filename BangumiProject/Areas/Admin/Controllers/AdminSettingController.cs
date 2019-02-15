using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Areas.Admin.Models;
using BangumiProject.Areas.Admin.Process;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class AdminSettingController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private AdminSettingWriteAndRead AdminSettingWriteAndRead { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public AdminSettingController()
        {
            AdminSettingWriteAndRead = new AdminSettingWriteAndRead();
        }

        [HttpPost]
        [Route("/WebSetting")]
        public IActionResult Setting(AdminSetting setting)
        {
            WebSiteSetting.IsShowTopPic = setting.IsShowTopPic;
            WebSiteSetting.IsOpenSignUp = setting.IsOpenSignUp;
            WebSiteSetting.IsWebSiteOpen = setting.IsWebSiteOpen;
            WebSiteSetting.PicPath = setting.PicPath;

            AdminSettingWriteAndRead.Write(setting);

            return Redirect("/WebSetting");
        }

        [HttpGet]
        [Route("/WebSetting")]
        public IActionResult ShowSettingPage()
        {
            AdminSetting setting = AdminSettingWriteAndRead.Read();
            return View(
                viewName:"Setting",
                model:setting
                );
        }
    }
}