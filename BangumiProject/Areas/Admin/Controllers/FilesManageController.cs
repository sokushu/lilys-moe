using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Services.DBServices.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;
using Blog = BangumiProject.Areas.Blogs.Models.Blogs;
using System.IO;
using BangumiProject.Areas.Admin.Process.Files;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class FilesManageController : Controller
    {
        private readonly IDBCore _DBCORE;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_DBCORE"></param>
        /// <param name="_userManager"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_authorizationService"></param>
        public FilesManageController(
            IDBCore _DBCORE,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._DBCORE = _DBCORE;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            Directory.GetFiles("/");
            return View();
        }

        /// <summary>
        /// 返回文件管理的那一部分视图
        /// 使用AJAX获取
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult GetFileList(string Path)
        {
            
            return PartialView();
        }
        
        private void Process(string Path, IFileProcess process)
        {

        }

    }
}