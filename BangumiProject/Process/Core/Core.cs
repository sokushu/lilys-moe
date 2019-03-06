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
    /// <summary>
    /// 让我们来设计一个自己的框架吧
    /// 
    /// 页面的显示，要可以复用。
    /// 一个页面上面有若干元素构成。
    /// 
    /// 例如这个页面用到了动画的数据，也用到了其他的数据。
    /// 我们可以来个加载什么的
    /// 
    /// Set（动画加载处理）
    /// Set（其他的加载处理）
    /// show（）
    /// 
    /// 就是这种感觉
    /// </summary>
    public class Core : Core2
    {
        public Core
            (
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService,
            HttpContext httpcontext
            )
            : base(
                  _userManager,
                  _signInManager,
                  _authorizationService,
                  httpcontext
                  )
        {

        }

        public void Run<Model>(ModelCore<Model> modelCore) where Model : class
        {

        }
    }
}
