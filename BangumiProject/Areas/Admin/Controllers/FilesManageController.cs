using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BangumiProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = Final.Yuri_Admin)]
    public class FilesManageController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        /// <summary>
        /// 存储密码
        /// </summary>
        private static List<string> Passwords = new List<string>();

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

        [HttpGet]
        [Route("/Admin/MakePassword")]
        public IActionResult MakePasswords()
        {
            if (Passwords.Count == 0)
            {
                string ID = string.Empty;
                for (int i = 0; i < 5; i++)
                {
                    ID += Guid.NewGuid().ToString();
                    ID += DateTime.Now.ToString();
                    Passwords.Add(ID);
                }
                return Json(Passwords);
            }
            else
            {
                return Json("还有未用完的密码，请用完后在重新生成密码");
            }
            
        }

        /// <summary>
        /// 用于返回上传的链接，验证方式，验证密码等
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("/Admin/GetApi")]
        public IActionResult UpLoadAPI(string Password)
        {
            string PW = Passwords.Where(pw => pw == Password).FirstOrDefault();
            if (PW != null)
            {
                //密码只有一一个
                bool flag = Passwords.Remove(PW);
                if (flag)
                {
                    //开始读取文件
                    //验证一下，是否是我们的软件啊
                }
                return Json("");
            }
            else
            {
                return Json("密码不正确");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        [NonAction]
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