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
using BangumiProjectDBServices.PageModels;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

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
        protected string[] YuriName { get; set; } = new string[] { string.Empty };

        /// <summary>
        /// 是否显示非百合动画的警告页面
        /// </summary>
        protected bool ShowNotYuriPage { get; set; } = false;

        /// <summary>
        /// UI模式的类型
        /// </summary>
        protected UIMode IMode { get; set; } = UIMode.Normal_;

        /// <summary>
        /// 能够通用的数据
        /// </summary>
        protected Common Ccommon { get; set; } = new Common();

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
        /// 用户权限
        /// </summary>
        protected Final.YURI_TYPE YURI_TYPE { get; set; } = Final.YURI_TYPE.Yuri_Yuri1;

        /// <summary>
        /// 
        /// </summary>
        protected object Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        protected string ViewName { get; set; } = string.Empty;
        /// <summary>
        /// 数据库
        /// </summary>
        protected IServices DBServices { get; }

        /// <summary>
        /// 权限表
        /// </summary>
        protected RoleManager<IdentityRole> RoleManager { get; }

        /// <summary>
        /// 用户数据表
        /// </summary>
        protected UserManager<User> UserManager { get; }

        /// <summary>
        /// 用户登录相关
        /// </summary>
        protected SignInManager<User> SignInManager { get; }

        /// <summary>
        /// 用户权限服务
        /// </summary>
        protected IAuthorizationService AuthorizationService { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        protected BaseController(
            IServices DBServices,
            SignInManager<User> SignInManager,
            UserManager<User> UserManager,
            RoleManager<IdentityRole> RoleManager = null,
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
        protected virtual void Init(
            params LoadMode[] loadModes
            )
        {
            //对传入的数据进行排序
            var Params = loadModes.ToList().OrderBy(key => key).ToList();
            
            IsSignIn = HttpContext.Session.GetInt32(nameof(Common.IsSignIn)).IntToBool();
            if (!IsSignIn)//如果是没登陆的状态
            {
                if (IsSignIn = SignInManager.IsSignedIn(User))//检查是否真的没登陆
                {
                    HttpContext.SetComm(Ccommon = HttpContext.CommonMake(UserManager, IsSignIn));
                }
                else
                {
                    Ccommon = HttpContext.GetComm(true);//得到未登陆的
                }
            }
            else
            {
                //已经登陆的状态
                Ccommon = HttpContext.GetComm();
            }
            if (IsSignIn)
            {
                UID = UserManager.GetUserId(User);
                UserName = UserManager.GetUserName(User);
                YURI_TYPE = Ccommon.YURI_TYPE;
            }
            foreach (var Mode in Params)
            {
                switch (Mode)
                {
                    case LoadMode.SignIn:
                        if (!IsSignIn)
                        {
                            return;
                        }
                        break;
                    case LoadMode.YuriMode:
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
                    case LoadMode.UIMode:
                        IMode = HttpContext.UIModeCheck(YuriMode);
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ViewName"></param>
        [NonAction]
        protected virtual void InitView(string ViewName)
        {
            this.ViewName = ViewName;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [NonAction]
        public override ViewResult View(string viewName, object model)
        {
            ViewData[nameof(Common)] = Ccommon;
            return base.View(viewName, model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns>
        [NonAction]
        public override ViewResult View(string viewName)
        {
            ViewData[nameof(Common)] = Ccommon;
            return base.View(viewName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public override ViewResult View()
        {
            return base.View(ViewName);
        }

        /// <summary>
        /// 
        /// </summary>
        protected enum LoadMode
        {
            SignIn,
            YuriMode,
            UIMode,
        }
    }
}