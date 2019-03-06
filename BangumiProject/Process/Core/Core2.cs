using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Process.Core
{
    public abstract class Core2
    {
        public Core2(
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService,
            HttpContext httpcontext
            )
        {
            IsSignIn = _signInManager.IsSignedIn(httpcontext.User);
            UserName = _userManager.GetUserName(httpcontext.User);
            UID = _userManager.GetUserId(httpcontext.User);
        }

        /// <summary>
        /// 
        /// </summary>
        protected bool IsSignIn { get; set; }
        protected string UserName { get; set; }
        protected string UID { get; set; }
    }
}
