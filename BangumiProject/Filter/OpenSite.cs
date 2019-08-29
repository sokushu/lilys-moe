using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace BangumiProject.Filter
{
    /// <summary>
    /// 
    /// </summary>
    public class OpenSite : IAuthorizationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        private IAuthorizationService AuthorizationService { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="AuthorizationService"></param>
        public OpenSite(IAuthorizationService AuthorizationService)
        {
            this.AuthorizationService = AuthorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!WebSiteSetting.IsWebSiteOpen)
            {
                bool isAdmin = AuthorizationService.AuthorizeAsync(context.HttpContext.User, Final.Yuri_Admin).Result.Succeeded;
                if (!isAdmin)
                {
                    context.Result = new ContentResult()
                    {
                        Content = "网站正在维护中，请稍后访问，给您带来的不变，我们深感抱歉",
                        ContentType = "text/html;charset=utf-8",
                        StatusCode = StatusCodes.Status200OK
                    };
                }
            }
        }
    }
}
