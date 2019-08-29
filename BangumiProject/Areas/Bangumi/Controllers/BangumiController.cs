using System;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Core;
using BangumiProjectDBServices.Services;
using BaseProject.Exceptionss;
using BangumiProjectDBServices.PageModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BangumiProjectDBServices.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BangumiProjectDBServices.ParamsModels;
using BangumiProject.Controllers;
using Microsoft.AspNetCore.Identity;
using BangumiProject.CoreProcess.Bangumi;

namespace BangumiProject.Areas.Bangumi.Controllers
{
    /// <summary>
    /// 
    /// 有一个问题需要注意
    /// 内存缓存与数据库更新的问题
    /// 
    /// 经过我的一些实验探索，发现从缓存中读取出的数据不能更新进数据库中（还是待确认中的……）
    /// （例如试一下 _DB.Updata() 方法）
    /// 所以，为了安全起见。
    /// 遇到数据库更新的场景。
    /// 先读取数据后，再进行更新
    /// 
    /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    /// 后续更新！！更新！！更新！！！
    /// 重要的事情要重复几遍才行！！！
    /// 
    /// 经过实验证实
    /// _DB.Updata() 方法
    /// 是有效的！！！
    /// 
    /// </summary>
    [Area("Bangumi")]
    public class BangumiController : BaseController
    {
        public BangumiController(
            IServices _Services,
            UserManager<User> _UserManager,
            IAuthorizationService _AuthorizationService,
            SignInManager<User> SignInManager
            ) :base(
                DBServices: _Services,
                UserManager: _UserManager,
                AuthorizationService: _AuthorizationService,
                SignInManager: SignInManager
                )
        {}

        /// <summary>
        /// 
        /// 查询所有的动画数据，
        /// 动画索引页面
        /// 
        /// 可以分类查询动画，
        /// 按年份查询
        /// 按动画的类型标签查询
        /// 
        /// 搜索动画
        /// 
        /// 具体可以参考Bilibili的动画分类页面
        /// 
        /// 参数是过滤选项。
        /// 
        /// </summary>
        /// <param name="Page">页数，第几页</param>
        /// <param name="tagname">动画标签名</param>
        /// <param name="year">动画播出年份</param>
        /// <param name="season">动画播出季度</param>
        /// <param name="animestats">动画状态，完结了吗</param>
        /// <param name="animetype">动画类型，TV动画吗</param>
        /// <param name="dayofweek">动画是星期几播放的</param>
        /// <returns></returns>
        // GET: Bangumi
        [HttpGet]
        [Route("/Bangumi", Name = Final.Route_Bangumi_Index)]
        public ActionResult IndexAsync
            (
            int Page = 1, string tagname = "",
            int year = -1, int season = -1,
            int animestats = -1, int animetype = -1,
            int dayofweek = -1
            )
        {
            
            /*######################   BUG    #########################
            * 这里要用搜索来实现了
            *######################   BUG    #########################
            */
            return View();
        }

        /// <summary>
        /// 查询一部动画的数据
        /// 如果用户登陆，还可以返回登陆过后用户对这部动画做的信息
        /// 
        /// 待添加功能：
        /// 
        /// 
        /// </summary>
        /// <param name="id">需要查询的动画ID</param>
        /// <returns></returns>
        // GET: Bangumi/5
        [HttpGet]
        [Route("/Bangumi/{id:int}", Name = Final.Route_Bangumi_Details)]
        public IActionResult DetailsAsync(int ID = -1)
        {
            Init<Bangumi_OneAnime>("Bangumi_OneAnime");
            if (DBServices.HasAnimeID(ID))
            {
                LoadStreamBangumi_One bangumi_One = new LoadStreamBangumi_One(DBServices);
                bangumi_One.SetParams(ID, UID);

                Model = bangumi_One.Load();

                return View();
            }
            throw new AnimeNotFoundException("没有找到相应的动画");
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">动画ID</param>
        /// <param name="sub">动画集数</param>
        /// <param name="json">是否返回Json</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Policy = Final.Yuri_Yuri1)]
        [Route("/Bangumi/{id:int}", Name = "BangumiSub")]
        public IActionResult BangumiSub(int id, int sub = -1, bool json = false, bool del = false)
        {
            Init();
            if (DBServices.HasAnimeID(id))
            {
                if (del)
                {
                    ///
                    ///取消订阅
                    ///
                    var info = DBServices.Save_ToFirst<AnimeUserInfo>(CacheKey.Anime_User_Info(UID, id),
                        db => db.Where(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == id));
                    if (info != null)
                    {
                        DBServices.Remove(info).Commit();
                        if (json)
                        {
                            return Json(new List<string> { "true", "已经取消订阅" });
                        }
                    }
                    else
                    {
                        if (json)
                        {
                            return Json(new List<string> { "false", "没有订阅动画" });
                        }
                    }
                }
                else
                {
                    if (sub == -1)
                    {
                        ///
                        ///订阅动画
                        ///
                        var info = DBServices.Save_ToFirst<AnimeUserInfo>(CacheKey.Anime_User_Info(UID, id),
                            db => db.Where(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == id));
                        if (info == null)
                        {
                            var user = UserManager.GetUserAsync(User).Result;
                            var anime = DBServices.Save_ToFirst<Anime>(CacheKey.Anime_One(id), db => db.Where(ani => ani.AnimeID == id));

                            DBServices.Add(new AnimeUserInfo
                            {
                                NowAnimeNum = 0,
                                SubAnime = anime,
                                Users = user
                            }).Commit();

                            if (json)
                            {
                                return Json(new List<string> { "true", "订阅成功" });
                            }
                        }
                        else
                        {
                            if (json)
                            {
                                return Json(new List<string> { "false", "您已经定阅过" });
                            }
                        }
                    }
                    else
                    {
                        ///
                        ///更新动画的集数
                        ///
                        if (sub > 0)
                        {
                            var info = DBServices.Save_ToFirst<AnimeUserInfo>(CacheKey.Anime_User_Info(UID, id),
                                db => db.Where(animeinfo => animeinfo.Users.Id == UID && animeinfo.SubAnime.AnimeID == id));
                            if (info != null)
                            {
                                info.NowAnimeNum = sub;
                                DBServices.Save_Updata<AnimeUserInfo>(CacheKey.Anime_User_Info(UID, id), info).Commit();
                                if (json)
                                {
                                    return Json(new List<string> { "true", "更改成功" });
                                }
                            }
                            else
                            {
                                if (json)
                                {
                                    return Json(new List<string> { "false", "没有订阅动画，请先订阅动画" });
                                }
                            }
                        }
                        else
                        {
                            if (json)
                            {
                                return Json("Error");
                            }
                        }
                    }
                }
            }
            else
            {
                return NotFound();
            }
            return RedirectToRoute(Final.Route_Bangumi_Details, id);
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Bangumi/Create
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create)]
        public ActionResult Create(string type = null)
        {
            if (type != null)
            {
                switch (type)
                {
                    case "Memo"://添加一个MEMO
                        Init("Memo", NotInit:true);
                        break;
                    default:
                        Init("AddBangumi", NotInit: true);
                        break;
                }
            }
            else
            {
                Init("AddBangumi", NotInit: true);
            }
            return View();
        }

        /// <summary>
        /// 动画数据的创建
        /// 创建好数据进行上传，如果有图片数据的话，就调到新的页面，对图片进行裁剪
        /// </summary>
        /// <param name="collection"></param>
        /// <returns></returns>
        // POST: Bangumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create_POST)]
        public IActionResult CreateAsync(IFormCollection collection, int id = -1)
        {
            if (id > 0)
            {
                if (DBServices.HasAnimeID(id))
                {
                    Init();
                    var infos = DBServices.Save_ToFirst<AnimeUserInfo>(CacheKey.Anime_User_Info(UID, id), db => db.Include(info => info.Memos).Where(info => info.SubAnime.AnimeID == id && info.Users.Id == UID));
                    if (infos != null)
                    {
                        infos.Memos.Add(null);
                        DBServices.Save_Updata(CacheKey.Anime_User_Info(UID, id), infos).Commit();
                        return RedirectToRoute(Final.Route_Bangumi_Details, id);
                    }
                    return RedirectToRoute(Final.Route_Bangumi_Details, id);
                }
                return RedirectToRoute(Final.Route_Bangumi_Index);
            }
            else
            {
                try
                {
                    //上传的文件
                    var files = collection.Files;
                    if (files.Count > 0)
                    {
                        //上传文件的操作

                    }
                    var AnimeNum = collection["AnimeNum"];
                    if (!int.TryParse(AnimeNum, out int Num))
                    {
                        Num = 1;
                    }
                    var AnimeTime = collection["AnimePlayTime"];
                    if (!DateTime.TryParse(AnimeTime, out DateTime dateTime))
                    {
                        dateTime = DateTime.Now;
                    }
                    var End = collection["IsEnd"].FirstOrDefault();
                    if (!bool.TryParse(End, out bool IsEnd))
                    {
                        IsEnd = false;
                    }
                    //的到最后的动画ID
                    Anime anime = new Anime
                    {
                        AnimeName = collection["AnimeName"],
                        AnimeNum = Num,
                        AnimePic = collection["AnimePic"],
                        AnimeInfo = collection["AnimeInfo"],
                        AnimePlayTime = dateTime,
                        IsEnd = IsEnd
                    };
                    //将动画数据写入数据库
                    DBServices.Add(anime).Commit();
                    //最新发现，到这一步ID会有值o(*￣▽￣*)ブ
                    UpDataNew4(anime);//更新首页的最新4个动画的缓存
                    UpDataNotEnd(anime);
                    DBServices.ADDAnimeID(anime.AnimeID);//这里不要忘记添加动画ID
                    return RedirectToRoutePermanent(Final.Route_Bangumi_Delete, anime.AnimeID);
                }
                catch
                {
                    throw;//显示错误页面
                }
            }
        }

        /// <summary>
        /// 返回编辑动画的页面
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Bangumi/Edit/5
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Edit/{id:int}", Name = Final.Route_Bangumi_Edit)]
        public ActionResult EditAsync(int id)
        {
            Init<AnimeEdit>("BangumiEdit");
            if (DBServices.HasAnimeID(id))
            {
                var anime_One = DBServices.Save_ToFirst<Anime>(CacheKey.Anime_One(id), db => db.Where(anime => anime.AnimeID == id));
                Model = new AnimeEdit { Anime = anime_One };
                return View();
            }
            return NotFound();
        }

        /// <summary>
        /// 提交编辑之后的数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bangumiEdit"></param>
        /// <returns></returns>
        // POST: Bangumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Edit/{id:int}", Name = Final.Route_Bangumi_Edit_POST)]
        public ActionResult EditAsync(int id, AnimeEdit bangumiEdit)
        {
            try
            {
                if (DBServices.HasAnimeID(id))
                {
                    Anime Anime = bangumiEdit.Anime;
                    var NewTag = bangumiEdit.AddTag;

                    Anime anime = DBServices.ToFirst<Anime>(db => db.Where(a => a.AnimeID == id).Include(a => a.Tags));

                    anime.AnimeInfo = Anime.AnimeInfo;
                    anime.AnimeName = Anime.AnimeName;
                    anime.AnimeNum = Anime.AnimeNum;
                    anime.AnimePic = Anime.AnimePic;
                    anime.AnimeType = Anime.AnimeType;
                    anime.IsEnd = Anime.IsEnd;
                    anime.AnimePlayTime = Anime.AnimePlayTime;
                    if (!string.IsNullOrEmpty(NewTag) && !string.IsNullOrWhiteSpace(NewTag))
                    {
                        anime.Tags.Add(new AnimeTag
                        {
                            TagName = bangumiEdit.AddTag
                        });
                    }
                    UpDataNotEnd(anime);
                    //把缓存数据，数据库数据更新
                    DBServices.Save_Updata(CacheKey.Anime_One(id), anime).Commit();
                    //回到动画详细页面
                    return RedirectToRoute(Final.Route_Bangumi_Details, id);
                }
                return NotFound();
            }
            catch
            {
                throw;
            }
        }

        // GET: Bangumi/Delete/5
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Delete/{id:int}", Name = Final.Route_Bangumi_Delete)]
        public ActionResult Delete(int id)
        {
            return NotFound();
        }

        /// <summary>
        /// 删除一部动画
        /// 我们这里要是用软删除
        /// 不是真的删除，而是标记为删除
        /// </summary>
        /// <param name="id">要删除的ID</param>
        /// <param name="collection">删除的原因，以留做备案用</param>
        /// <returns></returns>
        // POST: Bangumi/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Delete/{id:int}", Name = Final.Route_Bangumi_Delete_POST)]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: 更改成软删除，目前还是直接删除的逻辑
                var DelAnime = DBServices.Save_ToFirst<Anime>(CacheKey.Anime_One(id), db => db.Where(anime => anime.AnimeID == id));
                DBServices.Remove(DelAnime).Commit();
                return RedirectToRoute(Final.Route_Bangumi_Index);
            }
            catch
            {
                throw;
            }
        }

        ////=====================================================================================
        ////=====================================================================================
        ////=====================================================================================
        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        private void UpDataNew4(Anime anime)
        {
            var List = DBServices.GetCache<List<Anime>>(CacheKey.Anime_New4());
            List.RemoveAll(a => a.AnimeID == anime.AnimeID);
            List.Add(anime);
            DBServices.GetCacheEntry(CacheKey.Anime_New4()).Value = List.OrderByDescending(t => t.Time).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        private void UpDataNotEnd(Anime anime)
        {
            int ID = anime.AnimeID;
            var List = DBServices.GetCache<List<Anime>>(CacheKey.Anime_NotEnd());
            Anime SearchAnime = null;
            if ((SearchAnime = List.Where(a => a.AnimeID == ID).FirstOrDefault()) == null)
            {
                List.Add(anime);
            }
            else
            {
                List.Remove(SearchAnime);
                List.Add(anime);
            }
            DBServices.GetCacheEntry(CacheKey.Anime_NotEnd()).Value = List;
        }
    }
}