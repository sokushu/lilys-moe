using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BangumiProjectDBServices.PageModels.Core;
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
        /// 是否已经初始化了
        /// </summary>
        private bool IsInit { get; set; } = false;

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
        protected BaseModel Model { get; set; } = null;

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
        protected virtual void Init<T>(string ViewName) where T : BaseModel, new()
        {
            this.ViewName = ViewName;
            if (IsInit)
            {
                return;
            }
            if (Model == null)//检查Model是否为空，只初始化一次
            {
                IsInit = true;
                Model = new T();
                IsSignIn = SignInManager.IsSignedIn(User);//检查登录
                if (IsSignIn)
                {
                    //创建已登录用户的状态
                    Model = HttpContext.GetComm(Model, true);

                    UID = UserManager.GetUserId(User);
                    UserName = UserManager.GetUserName(User);
                    YURI_TYPE = Model.YURI_TYPE;

                    YuriMode = HttpContext.YuriModeCheck();
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
                else
                {
                    //创建未登录用户的状态
                    Model = HttpContext.GetComm(Model, false);
                }
            }
        }

        /// <summary>
        /// 初始化，加载相应的数据
        /// </summary>
        /// <param name="loadMode"></param>
        [NonAction]
        protected virtual void Init(string Viewname = "", bool NotInit = false)
        {
            IsInit = NotInit;
            this.ViewName = ViewName;
            if (IsInit)
            {
                return;
            }
            IsInit = true;
            IsSignIn = SignInManager.IsSignedIn(User);//检查登录
            if (IsSignIn)
            {
                UID = UserManager.GetUserId(User);
                UserName = UserManager.GetUserName(User);
                YURI_TYPE = (Final.YURI_TYPE)(HttpContext.Session.GetInt32(nameof(BaseModel.YURI_TYPE)) ?? 1);

                YuriMode = HttpContext.YuriModeCheck();
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
        /// 保存上传的文件
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
        /// 返回页面的一个简单方法
        /// </summary>
        /// <returns></returns>
        [NonAction]
        public override ViewResult View()
        {
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