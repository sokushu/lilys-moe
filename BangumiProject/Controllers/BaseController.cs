using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BangumiProjectDBServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

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
        /// 用于渲染的Model
        /// </summary>
        protected object Model { get; set; } = null;

        /// <summary>
        /// 页面的名称
        /// </summary>
        protected string ViewName { get; set; } = null;
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
            IsSignIn = SignInManager.IsSignedIn(User);//检查登录
            if (IsSignIn)
            {
                bool flag = HttpContext.Session.GetInt32(nameof(Common.IsSignIn)).IntToBool();
                if (!flag)
                {
                    HttpContext.SetComm(Ccommon = HttpContext.CommonMake(UserManager, IsSignIn));
                }
                else
                {
                    //已经登陆的状态
                    Ccommon = HttpContext.GetComm();
                }
            }
            else
            {
                Ccommon = HttpContext.GetComm(true);//得到未登陆的
                return;
            }

            UID = UserManager.GetUserId(User);
            UserName = UserManager.GetUserName(User);
            YURI_TYPE = Ccommon.YURI_TYPE;

            YuriMode = HttpContext.YuriModeCheck();
            if (YuriMode)
            {
                IMode = HttpContext.UIModeCheck(YuriMode);
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
        }

        /// <summary>
        /// 将数据保存到内存缓存中
        /// </summary>
        [NonAction]
        protected void SaveCache(string Key, object Value)
        {
            DBServices.MemoryCache.CreateEntry(Key).Value = Value;
        }

        /// <summary>
        /// 从内存缓存中读取数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Key"></param>
        /// <returns></returns>
        [NonAction]
        protected T GetCache<T>(string Key)
        {
            bool OK = DBServices.MemoryCache.TryGetValue(Key, out object obj);
            if (OK)
            {
                return (T)obj;
            }
            return default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="iformfile"></param>
        [NonAction]
        protected void SaveFile(string Path, params IFormFile[] iformfile)
        {
            foreach (IFormFile file in iformfile)
            {
                string fileName = file.FileName;
                string contentType = file.ContentType;
                long fileSize = file.Length;

                Stream stream = null;
                try
                {
                    using (stream = new FileStream(Path, FileMode.CreateNew))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }
                }
                catch (IOException)
                {
                    using (stream = new FileStream(Path, FileMode.Open))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    stream = null;
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
            ViewData[nameof(Common)] = Ccommon;
            if (Model == null)
            {
                if (ViewName == null)
                {
                    return View();
                }
                else
                {
                    return View(ViewName);
                }
            }
            else
            {
                if (ViewName == null)
                {
                    return View(Model);
                }
                else
                {
                    return View(ViewName, Model);
                }
            }
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