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
            if (exf?.Error is ErrorException exception)  // 如果出现错误就显示错误信息
            {
                switch (exception.StatesCode)
                {
                    case Final.StatusCode403:
                        break;
                    case Final.StatusCode404:
                        break;
                    case Final.StatusCodeBangumiNotFound:
                        exception.SetMessage("没有找到你想要的动画");
                        break;
                    default:
                        break;
                }
            }
            else
            {
                exception = new ErrorException(Code);
                switch (Code)
                {
                    case (int)HttpStatusCode.NotFound:          // 404
                    case (int)HttpStatusCode.MethodNotAllowed:
                        exception.SetMessage("网页走丢了 >.<");
                        break;
                    case (int)HttpStatusCode.Forbidden:         // 403
                        exception.SetMessage("非常抱歉，您没有权限访问");
                        break;
                    case (int)HttpStatusCode.OK:                // 基本不可能走到这里的
                        return RedirectToRoute("Index");
                    default:                                    // 如果出现了意料之外的Code，那就返回首页吧
                        return RedirectToRoute("Index");
                }
            }
            return View(
                    viewName: "Error",
                    model: exception
                    );
        }
    }
}