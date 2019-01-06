//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using BangumiProject.Models;
//using BangumiProject.Views.Files;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using MoeUtilsBox.String;
//using SixLabors.ImageSharp.Formats;
//using System.Net;
//using System.Net.Mime;
//using SixLabors.ImageSharp;
//using SixLabors.ImageSharp.PixelFormats;
//using BangumiProject.Process.Files;
//using BangumiProject.Process;
//using BangumiProject.Process.Files.aliyunOSS;
//using Microsoft.Extensions.Caching.Memory;
//using MoeUtilsBox;
//using BangumiProject.Areas.Users.Models;
//using Images = BangumiProject.Areas.Files.Models.FileImages;

//namespace BangumiProject.Controllers
//{
    
//    public class FilesController : Controller
//    {
//        /// <summary>
//        /// 用户数据库
//        /// </summary>
//        private readonly UserManager<Users> _userManager;
//        private readonly BangumiProjectContext _DB;
//        private readonly MoeTools MoeTools = new MoeTools();
//        //加入内存缓存
//        private readonly IMemoryCache _cache;
//        /// <summary>
//        /// 加载数据库
//        /// </summary>
//        /// <param name="_userManager">用户权限数据库</param>
//        /// <param name="DB"></param>
//        public FilesController(UserManager<Users> _userManager, BangumiProjectContext _DB, IMemoryCache memoryCache)
//        {
//            this._userManager = _userManager;
//            this._DB = _DB;
//            _cache = memoryCache;
//            //提前加载缓存
//            if (!_cache.TryGetValue("Images", out List<Images> Images))
//            {
//                Images = _DB.Images.Include(img => img.Photos).Include(img => img.UpLoadUsers).ToList();
//                _cache.Set("Images", Images);
//            }
//        }
//        //============================================================================================================================

//        /// <summary>
//        /// 相册服务
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Photo", Name = "Photo")]
//        [Authorize(Policy = Final.Yuri_Admin)]//管理员可登陆，暂时不能访问
//        public async Task<IActionResult> Photo()
//        {
//            // 先说说这个服务吧
//            // 这是一个相册服务，用户可以创建相册
//            // 可以分享相册，也可以选择相册私密，密码访问。
//            // 就是这样子吧
//            // 用户不登陆访问看到的是其他用户分享的样子，登陆之后就是自己的相册列表

//            var User = await _userManager.GetUserAsync(HttpContext.User);
//            // 获取用户上传的所有图片
//            var Images = await _DB.Images.Where(img => img.UpLoadUsers.Equals(User)).ToListAsync();
//            _cache.Set(HttpContext.User, User);
//            return View();
//        }

//        /// <summary>
//        /// 返回一个图片列表
//        /// </summary>
//        /// <returns></returns>
//        [Authorize(Policy = Final.Yuri_Yuri1)] //需要登陆进行访问
//        [HttpGet]
//        //[ResponseCache]//视图缓存
//        [Route("/files", Name = "FileList")]
//        public async Task<IActionResult> Index()
//        {
//            //获取用户上传的所有图片
//            var User = await _userManager.GetUserAsync(HttpContext.User);
//            //加载所有的图片数据
//            var Images = _cache.Get<List<Images>>("Images");

//            Images = Images.Where(img => img.UpLoadUsers.Id.Equals(User.Id)).OrderByDescending(img => img.Time).ToList();
//            //获取文件名，方便获取静态文件
//            Images.ForEach(img =>
//                img.ImagePath.GetFileName()
//            );

//            return View("Index", new IndexModel
//            {
//                Message ="Hi",
//                Tilte ="文件上传（本站支持.jpg .png格式的图片）",
//                Pic = Images
//            });
//        }

//        /// <summary>
//        /// 动态的得到图片文件
//        /// 基于资源的授权方式，不是自己的图片是无法打开的
//        /// </summary>
//        /// <param name="imageid"></param>
//        /// <returns></returns>
//        [Authorize]  // 需要登陆访问
//        [HttpGet]
//        [ResponseCache(Duration = 60 * 15)]//缓存十五分钟
//        [Route("/files/{imageid?}", Name = "GetImages")]
//        public async Task<IActionResult> GetImages(string imageid)
//        {
//            var Image = await _DB.Images.SingleOrDefaultAsync(img => img.ImageID.Contains(imageid));
//            if (Image == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                var ReadUser = Image.ReadUsers;
//                if (string.IsNullOrEmpty(ReadUser) || string.IsNullOrWhiteSpace(ReadUser))
//                {
//                    // 公开权限的图片
//                    byte[] Data = await System.IO.File.ReadAllBytesAsync(Image.ImagePath);
//                    return File(Data, MediaTypeNames.Image.Jpeg);
//                }
//                else
//                {
//                    var User = await _userManager.GetUserAsync(HttpContext.User);
//                    HashSet<string> Read = new HashSet<string>(ReadUser.Split(','));
//                    if (Read.Contains(User.Id))
//                    {
//                        byte[] Data = await System.IO.File.ReadAllBytesAsync(Image.ImagePath);
//                        return File(Data, MediaTypeNames.Image.Jpeg);
//                    }
//                    else
//                    {
//                        return StatusCode((int)HttpStatusCode.Forbidden);
//                    }
//                }
//            }
//        }

//        /// <summary>
//        /// 设置图片的信息
//        /// 
//        /// 全局查询筛选器进行软删除，即标记为删除，实际并不删除数据库中的信息
//        /// </summary>
//        /// <param name="imageid"></param>
//        /// <see cref="https://docs.microsoft.com/zh-cn/ef/core/querying/filters"/>
//        /// <returns></returns>
//        [Authorize]
//        [HttpPost]
//        [Route("/files/{imageid?}/setting", Name = "SetImage")]
//        public async Task<IActionResult> SetImage(string imageid, ImageSetting imageSetting)
//        {
//            Images image = await _DB.Images.Where(Img => Img.ImageID.Equals(imageid)).Include(img => img.UpLoadUsers).FirstOrDefaultAsync();
//            if (image == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                Users users = await _userManager.GetUserAsync(HttpContext.User);
//                if (image.UpLoadUsers.Id.Equals(users.Id))  //是一个人
//                {
//                    if (imageSetting.IsDel == true)//标记为删除了
//                    {

//                    }
//                    else
//                    {
//                        image.ImageName = imageSetting.Name;//设置图片名称
//                        image.ReadUsers = imageSetting.ReadUser;//设置图片读取权限
//                    }
                    
//                    _DB.Entry(image).State = EntityState.Modified;  //标记为已更改
//                    await _DB.SaveChangesAsync();
//                    return RedirectToRoute("FileList");
//                }
//                else
//                {
//                    return StatusCode((int)HttpStatusCode.Forbidden);
//                }
//            }
//        }

//        /// <summary>
//        /// 返回页面用
//        /// </summary>
//        /// <param name="imageid"></param>
//        /// <returns></returns>
//        [Authorize(Policy = Final.Yuri_Yuri1)]
//        [HttpGet]
//        [Route("/files/{imageid?}/setting", Name = "GetSetImage")]
//        public async Task<IActionResult> GetSetImage(string imageid)
//        {
//            //读取图片
//            Images image = await _DB.Images.Where(Img => Img.ImageID.Equals(imageid))
//                .Include(img => img.UpLoadUsers).FirstOrDefaultAsync();
//            if (image == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                Users users = await _userManager.GetUserAsync(HttpContext.User);
//                if (image.UpLoadUsers.Equals(users))  //是一个人
//                {
//                    ImageSetting imagesetting = new ImageSetting
//                    {
//                        Name = image.ImageName,
//                        ReadUser = image.ReadUsers,
//                        ImageID = image.ImageID
//                    };
//                    //返回设置页面
//                    return View(
//                        viewName: "SetImage",
//                        model: imagesetting
//                        );
//                }
//                else
//                {
//                    return StatusCode((int)HttpStatusCode.Forbidden);
//                }
//            }
//        }

//        /// <summary>
//        /// 返回一个公开的图片
//        /// ReadUsers字段是空的场合
//        /// </summary>
//        /// <param name="imageID"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [ResponseCache(Duration = 60 * 60)]//缓存一个小时
//        [Route("/files/public/{imageID?}", Name = "GetImagePublic")]
//        public async Task<IActionResult> GetImagePublic(string imageID)
//        {
//            var Image = await _DB.Images.SingleOrDefaultAsync(img => img.ImageID.Contains(imageID));
//            if (Image == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                var ReadUser = Image.ReadUsers;
//                if (string.IsNullOrEmpty(ReadUser) || string.IsNullOrWhiteSpace(ReadUser))
//                {
//                    byte[] Data = await System.IO.File.ReadAllBytesAsync(Image.ImagePath);
//                    return File(Data, MediaTypeNames.Image.Jpeg);
//                }
//                else
//                {
//                    // 如果是需要验证用户的，则跳转到用户验证方式
//                    return RedirectToRoutePermanent("GetImages", imageID);
//                }
//            }
//        }

//        /// <summary>
//        /// 文件上传，准备使用Ajax进行操作
//        /// 
//        /// 文件上传的安全防护：
//        /// 防止用户上传图片以外的文件，
//        /// 对文件的ContentType进行检查，为了防止恶意用户的伪造，上传危险文件
//        /// 对文件是否真的是图片也做了验证
//        /// 
//        /// 这里使用了ImageSharp进行图片处理，具体请看SEE
//        /// </summary>
//        /// <see cref="https://github.com/SixLabors/ImageSharp"/>
//        /// <returns></returns>
//        [Authorize(Policy = Final.Yuri_Yuri1)] //需要登陆进行访问
//        [HttpPost]
//        [ValidateAntiForgeryToken]//禁止跨站访问的令牌验证
//        [RequestSizeLimit((1024L * 1024L * 1024L * 5L))] // 文件上传大小限制在5MB
//        [Route("/files/upload", Name = "UploadFiles")]
//        public async Task<IActionResult> PostImages([FromForm]UpLoadFiles uploadFiles)
//        {
//            var files = uploadFiles.Files;

//            FileUpLoadProcess fileUpLoadProcess = new FileUpLoadProcess();

//            var User = await _userManager.GetUserAsync(HttpContext.User);
//            var IsADmin = await _userManager.IsInRoleAsync(User, Final.Yuri_Admin);

//            try
//            {
//                ReturnType returnType = await fileUpLoadProcess.FileUpLoadToAliyunOSSAsync(files, User);
//                switch (returnType)
//                {
//                    case ReturnType.AdminLog:
//                        var returnAdmingLog = await fileUpLoadProcess.AdmingLogAsync(IsADmin, files);
//                        goto OK;//如果不出错，就证明OK
//                    case ReturnType.FileUpLoadOK:
//                        var Image = fileUpLoadProcess.Images;
//                        await _DB.Images.AddAsync(Image);
//                        await _DB.SaveChangesAsync();
//                        return File(System.IO.File.ReadAllBytes(Image.ImagePath), MediaTypeNames.Image.Jpeg);
//                    OK:
//                        return RedirectToRoute("FileList");
//                    default:
//                        goto OK;
//                }
//            }
//            catch (ErrorException e) when (e.StatesCode == 1)
//            {
//                //返回到上传页面
//                return RedirectToRoute("FileList");
//            }
//            catch (ErrorException e) when (e.StatesCode == 2)
//            {
//                //上传文件格式错误
//                //发生其他诡异的错误
//                throw;
//            }
//            catch (Exception e)
//            {
//                //其他更加诡异的错误
//                throw e;
//            }
//        }
//    }
//}