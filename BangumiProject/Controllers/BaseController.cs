using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using BangumiProject.Areas.Bangumi.Process;
using BangumiProject.Areas.HomeBar.Views.Home.Model;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;
using BangumiProjectProcess.Common;
using BangumiProjectDBServices.PageModels;
using Microsoft.AspNetCore.Authorization;

namespace BangumiProject.Controllers
{
    public abstract class BaseController : Controller
    {
        /// <summary>
        /// 百合模式
        /// </summary>
        protected bool YuriMode { get; set; } = false;

        /// <summary>
        /// 百合标签的名称
        /// </summary>
        protected string[] YuriName { get; set; } = null;

        /// <summary>
        /// 是否显示非百合动画的警告页面
        /// </summary>
        protected bool ShowNotYuriPage { get; set; } = false;

        /// <summary>
        /// UI模式的类型
        /// </summary>
        protected UIMode IMode { get; set; } = UIMode.Normal_;

        /// <summary>
        /// 哪些元素显示，哪些不显示
        /// </summary>
        protected Common_UIEnable Common_UI { get; set; } = Common_UIEnable.CreateUI(false, UIMode.Normal_);

        /// <summary>
        /// 能够通用的数据
        /// </summary>
        protected Common Common { get; set; } = new Common();

        /// <summary>
        /// 是否已经登录
        /// </summary>
        protected bool IsSignIn { get; set; } = false;

        /// <summary>
        /// 用户名
        /// </summary>
        protected string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户的ID
        /// </summary>
        protected string UID { get; set; } = string.Empty;

        /// <summary>
        /// 数据库
        /// </summary>
        protected IServices DBServices { get; }

        /// <summary>
        /// 权限表
        /// </summary>
        protected RoleManager<IdentityRole> RoleManager { get; }

        /// <summary>
        /// 
        /// </summary>
        protected UserManager<User> UserManager { get; }

        /// <summary>
        /// 
        /// </summary>
        protected SignInManager<User> SignInManager { get; }

        /// <summary>
        /// 
        /// </summary>
        protected IAuthorizationService AuthorizationService { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        protected BaseController(
            IServices DBServices,
            RoleManager<IdentityRole> RoleManager = null,
            UserManager<User> UserManager = null,
            SignInManager<User> SignInManager = null,
            IAuthorizationService AuthorizationService = null
            )
        {
            this.DBServices = DBServices;
            this.RoleManager = RoleManager;
            this.UserManager = UserManager;
            this.SignInManager = SignInManager;
            this.AuthorizationService = AuthorizationService;
        }
        
        /// <summary>
        /// 初始化，加载相应的数据
        /// </summary>
        /// <param name="loadMode"></param>
        [NonAction]
        protected virtual void Init(params LoadMode[] loadModes)
        {
            foreach (var Mode in loadModes)
            {
                switch (Mode)
                {
                    case LoadMode.SignIn:
                        //验证是否登录
                        IsSignIn = SignInManager.IsSignedIn(HttpContext.User);
                        break;
                    case LoadMode.UIMode:
                        //加载UI模式
                        IMode = HttpContext.UIModeCheck(YuriMode);
                        break;
                    case LoadMode.YuriMode:
                        //加载百合模式
                        YuriMode = HttpContext.YuriModeCheck();
                        if (YuriMode)
                        {
                            switch (IMode)
                            {
                                case UIMode.Normal_:
                                case UIMode.Normal_G:
                                    //普通模式下，是显示所有数据，所以显示警告
                                    //可以关掉。
                                    //后期再写相关功能模块
                                    ShowNotYuriPage = true;
                                    break;
                                default:
                                    //其他的模式，不管了
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
            if (IsSignIn)
            {
                switch (IMode)
                {
                    case UIMode.Normal_:
                    case UIMode.YuriMode_:
                    case UIMode.YuriMode_Shojo:
                        Common_UI = Common_UIEnable.CreateUI(YuriMode, IMode);
                        break;
                    case UIMode.YuriMode_G:
                    case UIMode.Normal_G:
                        // 加载UI显示数据
                        Common_UI = HttpContext.CreateCommon_UI();
                        break;
                    default:
                        break;
                }
                //获取用户名
                UserName = UserManager.GetUserName(HttpContext.User);
                //获取用户的ID
                UID = UserManager.GetUserId(HttpContext.User);
            }
            // Comm类的设置
            Common.IsSignIn = IsSignIn;
            Common.UIMode = IMode;
            Common.Username = UserName;
        }

        /// <summary>
        /// 
        /// </summary>
        protected enum LoadMode
        {
            SignIn,
            UIMode,
            YuriMode,
        }
    }
}