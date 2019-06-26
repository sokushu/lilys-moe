using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class FilesManageController : Controller
    {
        private readonly IAuthorizationService _authorizationService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_DBCORE"></param>
        /// <param name="_userManager"></param>
        /// <param name="_signInManager"></param>
        /// <param name="_authorizationService"></param>
        public FilesManageController(
            IAuthorizationService _authorizationService
            )
        {
            this._authorizationService = _authorizationService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/Admin/Files")]
        public IActionResult Index()
        {
            Directory.GetFiles("/");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        private List<string> ListAllFile(string Path)
        {
            //Path目录是根目录下的文件夹或是文件
            //举个例子：
            // Path: /home/hom/
            //实际上我看到的是
            // /home/
            //这个目录也是实际上应该现实的目录
            string ProcessPath = Path;
            char DirectorySeparatorChar = System.IO.Path.DirectorySeparatorChar;
            int Len = ProcessPath.Length;
            if (ProcessPath.EndsWith(DirectorySeparatorChar))
            {
                if (Len > 1)
                {
                    ProcessPath = ProcessPath.Substring(0, Len - 1);
                }
                else
                {
                    goto ListFile;
                }
            }
            int SeparatorChar = ProcessPath.LastIndexOf(DirectorySeparatorChar);
            // D:\  (SeparatorChar + 1)会保留\符号，帮助得到正确结果
            ProcessPath = ProcessPath.Substring(0, (SeparatorChar + 1));

        //得到上一级目录
        //对文件进行显示
        ListFile:
            List<string> AllFiles = new List<string>();
            AllFiles.AddRange(Directory.GetDirectories(ProcessPath));
            AllFiles.AddRange(Directory.GetFiles(ProcessPath));
            return AllFiles;
        }
    }
}