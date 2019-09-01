using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BangumiProject.Areas.Error.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BangumiProject.Areas.Error.Controllers
{
    [Area("Error")]
    public class ErrorController : Controller
    {
        /// <summary>
        /// 错误页面的显示
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Error", Name = "SystemError")]
        public IActionResult Error()
        {
            var Code = HttpContext.Response.StatusCode;
            IExceptionHandlerFeature exf = HttpContext.Features.Get<IExceptionHandlerFeature>();
            Exception exception = null;
          
            var err = exf?.Error;
            switch (err.GetType().Name)
            {
                case nameof(ErrorException):
                    ErrorException error = null;
                    switch ((error = ((ErrorException)err)).StatesCode)
                    {
                        case Final.StatusCode403:
                            break;
                        case Final.StatusCode404:
                            break;
                        case Final.StatusCodeBangumiNotFound:
                            error.SetMessage("没有找到你想要的动画");
                            break;
                        default:
                            break;
                    }
                    exception = error;
                    break;
                case nameof(AnimeNotFoundException):
                    //AnimeNotFoundException animeNotFound = null;
                    exception = err;
                    break;
                default:
                    string meg = string.Empty;
                    switch (Code)
                    {
                        case (int)HttpStatusCode.NotFound:          // 404
                        case (int)HttpStatusCode.MethodNotAllowed:
                            meg = "网页走丢了 >.<";
                            break;
                        case (int)HttpStatusCode.Forbidden:         // 403
                            meg = "非常抱歉，您没有权限访问";
                            break;
                        case (int)HttpStatusCode.OK:                // 基本不可能走到这里的
                            return RedirectToRoute("Home_Index");
                        default:                                    // 如果出现了意料之外的Code，那就返回首页吧
                            return RedirectToRoute("Home_Index");
                    }
                    exception = new Exception(meg);
                    break;
            }
            return View(
                    viewName: "Error",
                    model: exception
                    );
        }
    }
}