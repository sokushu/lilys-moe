using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Setting.Controllers
{
    /// <summary>
    /// 侧边栏的设置项目
    /// </summary>
    [Area("Setting")]
    [Authorize]
    public class SettingController : Controller
    {
        /// <summary>
        /// 网站开关设置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Switch", Name = "WEBSITECLOSE")]
        [Authorize(Policy = Final.Yuri_Admin)]
        public IActionResult Close()
        {
            if (WebSiteSetting.IsWebSiteOpen)
            {
                WebSiteSetting.IsWebSiteOpen = false;
            }
            else
            {
                WebSiteSetting.IsWebSiteOpen = true;
            }
            return Redirect("/");
        }
    }
}