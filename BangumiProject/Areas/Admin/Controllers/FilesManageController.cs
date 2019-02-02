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
using BangumiProject.Areas.Admin.Process;

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
        [Route("/Admin/Files")]
        public IActionResult Index()
        {
            Directory.GetFiles("/");
            return View();
        }

        /// <summary>
        /// 返回文件管理的那一部分视图
        /// 使用AJAX获取
        /// 
        /// 注意：Path是要操作的目录，
        /// 最终遍历的是Path目录的上级目录。
        /// 
        /// </summary>
        /// <param name="Path">要操作的目录</param>
        /// <param name="ProcessType">要进行处理的编号</param>
        /// <param name="Rename">想要重命名的新名字</param>
        /// <returns></returns>
        [HttpGet]
        [Route("/Admin/Files/option")]
        public IActionResult FileProcess
            (
            string Path,
            string Rename,
            int ProcessType = -1
            )
        {
            if (string.IsNullOrEmpty(Path) || string.IsNullOrWhiteSpace(Path))
            {
                return Json("Error");
            }
            FileProcessType fileProcessType = (FileProcessType)ProcessType;
            IFileProcess fileProcess = null;
            switch (fileProcessType)
            {
                case FileProcessType.CreateFile:
                    fileProcess = new CrateFiles();
                    break;
                case FileProcessType.DeleteFile:
                    fileProcess = new DeleteFiles();
                    break;
                case FileProcessType.CreateDir:
                    fileProcess = new CreateDir();
                    break;
                case FileProcessType.DeleteDir:
                    fileProcess = new DeleteDir();
                    break;
                case FileProcessType.Rename:
                    fileProcess = new Rename(Rename);
                    break;
                default:
                    return Json("Error");
            }
            //进行相应的处理
            Process(Path, fileProcess);
            //重新对目录中的文件进行读取
            var FileList = ListAllFile(Path);
            return PartialView(
                viewName: "vvv",
                model: FileList
                );
        }
        
        /// <summary>
        /// 对文件的增删改查操作
        /// </summary>
        /// <param name="Path"></param>
        /// <param name="process"></param>
        private void Process(string Path, IFileProcess process)
        {
            try
            {
                process.Process(Path);
            }
            catch (FileNotFoundException)
            {
                
            }
            catch (Exception)
            {

            }
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