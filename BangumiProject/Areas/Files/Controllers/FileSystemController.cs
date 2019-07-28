using BangumiProject.Controllers;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BangumiProject.Areas.Files.Controllers
{
    [Area("Files")]
    [Authorize(Policy = Final.Yuri_Yuri1)]
    public class FileSystemController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="DBServices"></param>
        /// <param name="SignInManager"></param>
        /// <param name="UserManager"></param>
        public FileSystemController(
            IServices DBServices,
            SignInManager<User> SignInManager,
            UserManager<User> UserManager
            ) : base(
                DBServices,
                SignInManager,
                UserManager
                )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("/File", Name = "ShowFile")]
        public IActionResult ShowFile()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/UpLoad", Name = "FileUpLoad")]
        public IActionResult UpLoad(IFormCollection collection)
        {
            Init();
            InitView("VIEWNAME");
            Model = "HelloWorld";
            // 事先判断上传的位置（OSS还是本机）
            //TODO: 判断代码
            var files = collection.Files;
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    SaveFile("", file);
                }
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("/UpLoad", Name = "FileUpLoad")]
        public IActionResult UpLoadGet(IFormCollection collection)
        {
            Init();
            InitView("VIEWNAME");
            Model = "HelloWorld";
            // 事先判断上传的位置（OSS还是本机）
            //TODO: 判断代码
            var files = collection.Files;
            if (files.Count > 0)
            {
                foreach (var file in files)
                {
                    SaveFile("", file);
                }
            }
            return View();
        }
    }
}