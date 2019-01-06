using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;
using BangumiProject.Areas.Error.Models;
using BangumiProject.Areas.Files.Models;
using BangumiProject.Areas.Files.Process;
using BangumiProject.Areas.Files.Views.Files.Model;
using BangumiProject.Controllers;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoeUtilsBox;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Areas.Files.Controllers
{
    [Area("Files")]
    public class FilesController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly ICommDB _DBService;
        private readonly IAuthorizationService _authorizationService;
        public FilesController(
            UserManager<User> _userManager,
            ICommDB _DBService, 
            IAuthorizationService _authorizationService
            )
        {
            this._userManager = _userManager;
            this._DBService = _DBService;
            this._authorizationService = _authorizationService;
        }

        /// <summary>
        /// 返回一个图片列表
        /// </summary>
        /// <returns></returns>
        // GET: Files
        [HttpGet]
        [Route("/Files", Name = Final.Route_Files_Index)]
        public async Task<ActionResult> IndexAsync()
        {
            //获取用户上传的所有图片
            var UserID = _userManager.GetUserId(HttpContext.User);
            //加载所有的图片数据
            List<FileImages> Images = await _DBService.GetDateToListAsync<FileImages>(db => db.Where(img => img.UpLoadUsers.Id == UserID));

            //获取文件名，方便获取静态文件
            Images.ForEach(img =>
                img.ImagePath.GetFileName()
            );

            return View("Index", new IndexModel
            {
                Message = "Hi",
                Tilte = "文件上传（本站支持.jpg .png格式的图片）",
                Pic = Images
            });
        }

        // GET: Files/Details/5
        [HttpGet]
        [Route("/Files/{id?}", Name = Final.Route_Files_Details)]
        public async Task<ActionResult> DetailsAsync(string id)
        {
            var Image = await _DBService.GetFirstAsync<FileImages>(img => img.ImageID.Contains(id));
            if (Image == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }
            else
            {
                var ReadUser = Image.ReadUsers;
                if (string.IsNullOrEmpty(ReadUser) || string.IsNullOrWhiteSpace(ReadUser))
                {
                    // 公开权限的图片
                    byte[] Data = await System.IO.File.ReadAllBytesAsync(Image.ImagePath);
                    return File(Data, MediaTypeNames.Image.Jpeg);
                }
                else
                {
                    var UserID = _userManager.GetUserId(HttpContext.User);
                    HashSet<string> Read = new HashSet<string>(ReadUser.Split(','));
                    if (Read.Contains(UserID))
                    {
                        byte[] Data = await System.IO.File.ReadAllBytesAsync(Image.ImagePath);
                        return File(Data, MediaTypeNames.Image.Jpeg);
                    }
                    else
                    {
                        return StatusCode((int)HttpStatusCode.Forbidden);
                    }
                }
            }
        }

        // POST: Files/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(IFormCollection collection)
        {
            var files = collection.Files[0];

            FileUpLoadProcess fileUpLoadProcess = new FileUpLoadProcess();

            var User = await _userManager.GetUserAsync(HttpContext.User);
            var IsADmin = await _userManager.IsInRoleAsync(User, Final.Yuri_Admin);

            try
            {
                ReturnType returnType = await fileUpLoadProcess.FileUpLoadToAliyunOSSAsync(files, User);
                switch (returnType)
                {
                    case ReturnType.AdminLog:
                        var returnAdmingLog = await fileUpLoadProcess.AdmingLogAsync(IsADmin, files);
                        goto OK;//如果不出错，就证明OK
                    case ReturnType.FileUpLoadOK:
                        var Image = fileUpLoadProcess.Images;
                        await _DBService.AddAsync(Image);
                        return File(System.IO.File.ReadAllBytes(Image.ImagePath), MediaTypeNames.Image.Jpeg);
                    OK:
                        return RedirectToRoute(Final.Route_Files_Index);
                    default:
                        goto OK;
                }
            }
            catch (ErrorException e) when (e.StatesCode == 1)
            {
                //返回到上传页面
                return RedirectToRoute(Final.Route_Files_Index);
            }
            catch (ErrorException e) when (e.StatesCode == 2)
            {
                //上传文件格式错误
                //发生其他诡异的错误
                throw;
            }
            catch (Exception e)
            {
                //其他更加诡异的错误
                throw e;
            }
        }

        // GET: Files/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Files/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }

        // GET: Files/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Files/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(IndexAsync));
            }
            catch
            {
                return View();
            }
        }
    }
}