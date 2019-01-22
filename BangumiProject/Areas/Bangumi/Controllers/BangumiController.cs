using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using BangumiProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using User = BangumiProject.Areas.Users.Models.Users;
using Blog = BangumiProject.Areas.Blogs.Models.Blogs;
using MoeUtilsBox;
using BangumiProject.Areas.Bangumi.Process;
using BangumiProject.Areas.Bangumi.Views.Bangumi.Model;
using BangumiProject.Areas.Bangumi.Models;
using Microsoft.EntityFrameworkCore;
using MoeUtilsBox.Utils;
using System.Net;
using BangumiProject.Areas.Error.Models;
using BangumiProject.Areas.Bangumi.Process.AnimeFilterC;
using BangumiProject.Areas.Bangumi.Process.AnimeProcessC;

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
    public class BangumiController : Controller
    {
        private readonly ICommDB _DBServices;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAuthorizationService _authorizationService;
        public BangumiController(
            ICommDB _DBServices,
            UserManager<User> _userManager,
            SignInManager<User> _signInManager,
            IAuthorizationService _authorizationService
            )
        {
            this._DBServices = _DBServices;
            this._userManager = _userManager;
            this._signInManager = _signInManager;
            this._authorizationService = _authorizationService;
        }

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
        public async Task<ActionResult> IndexAsync(
            int Page = 1, string tagname = "", int year = -1, int season = -1, int animestats = -1,
            int animetype = -1, int dayofweek = -1
            )
        {
            KEY key = new KEY { Key = CacheKey.Anime_All().ToCharArray() };
            if (!_DBServices.GetDate(key, out List<Anime> ListAnime))
            {
                ListAnime = await _DBServices.GetDateToListAsync<Anime>(db => db.Select(a => a));
                _DBServices.SetCache(key, ListAnime);
            }
            key = new KEY { Key = CacheKey.Anime_AllTags().ToCharArray() };
            if (!_DBServices.GetDate(key, out List<AnimeTag> ListTag))
            {
                ListTag = await _DBServices.GetDateToListAsync<AnimeTag>(db => db.Include(a => a.Anime));
                _DBServices.SetCache(key, ListTag);
            }
            /*
             * 这里是一个变化高发区
             * 未来可能会加入不同的过滤条件
             */
            AnimeFilter animeFilter = new AnimeFilter();

            //动画是否完结
            animeFilter.SetAnimeFilter(new AnimeFilterByEnd(animestats));
            //动画的年份
            animeFilter.SetAnimeFilter(new AnimeFilterByYear(year));
            //动画的类型
            animeFilter.SetAnimeFilter(new AnimeFilterByAnimeType(animetype));
            //动画播出日是星期几
            animeFilter.SetAnimeFilter(new AnimeFilterByWeek(dayofweek));
            //这是哪一季度的动画
            animeFilter.SetAnimeFilter(new AnimeFilterBySeason(season));
            //添加标签过滤
            animeFilter.SetAnimeFilter(new AnimeFilterByTagName(tagname, ListTag));

            //返回最终的过滤结果集
            var Animes = animeFilter.GetAnimeFilter(ListAnime);
            PageHelper pageHelper = new PageHelper(20);
            return View(
                viewName:"Bangumi",
                model: new Views.Bangumi.Model.Bangumi
                {
                    AllPage = pageHelper.GetAllPage(),      //处理后动画的全部页数
                    NowPage = pageHelper.GetNowPage(),      //现在看到的页数
                    Animes = pageHelper.GetListPage(Page, Animes),        //处理后的动画集合
                    AnimeSeason = new List<int> { 1, 2, 3, 4 },//动画的季度（总共就4个季度，而且是常量的）
                    //AnimeTags = filter.AnimeTagName,  //动画的标签合集
                    //AnimeYear = filter.AnimeYear   //动画的年份合集
                }
                );
        }

        /// <summary>
        /// 查询一部动画的数据
        /// 如果用户登陆，还可以返回登陆过后用户对这部动画做的信息
        /// </summary>
        /// <param name="id">需要查询的动画ID</param>
        /// <returns></returns>
        // GET: Bangumi/5
        [HttpGet]
        [Route("/Bangumi/{id:int}", Name = Final.Route_Bangumi_Details)]
        public async Task<ActionResult> DetailsAsync(int id = -1)
        {
            //从数据库中读取数据
            if (!_DBServices.HasAnimeID(id))
                return NotFound();
            var key = new KEY { Key = CacheKey.Anime_One(id).ToCharArray() };
            if (!_DBServices.GetDate(key, out Anime Anime))
            {
                /**
                 * 我有话要说：
                 * 下面这个就是要对
                 */
                Anime = await _DBServices.GetDateOneAsync<Anime>(db =>
                        db.Where(a => a.AnimeID == id)
                        .Include(a => a.Souce)
                        .Include(a => a.Tags)
                        .Include(a => a.AnimeComms));
                _DBServices.SetCache(key, Anime);
            }
            key = new KEY { Key = CacheKey.Blog_One_ByAnimeID(id).ToCharArray() };
            if (!_DBServices.GetDate(key, out ICollection<Blog> blogs))
            {
                blogs = await _DBServices.GetDateToListAsync<Blog>(db => db.Where(b => b.AnimeID == id).OrderByDescending(a => a.Time).Take(10));
                _DBServices.SetCache(key, blogs);
            }
            /**
             * 这里是变化的数据
             * 未来可能会加入不同的页面展示模块
             * 想办法把这里拆分出来
             */
            //初始化数据
            var userAnimeNumber = 0;                                    //动画观看集数
            var IsSignIn = false;                                       //用户是否登录
            var IsSub = false;                                          //用户是否订阅某动画
            var IsShowEdit = false;                                     //用户是否可以编辑
            ICollection<AnimeMemo> memo = new List<AnimeMemo>();        //用户写下的MEMO
            //未登录也可以显示：
            ICollection<Blog> blog = blogs.GetListDate();               //用户对该动画的长评短评
            ICollection<AnimeTag> animeTags = new List<AnimeTag>();     //动画的标签
            ICollection<AnimeSouce> animeSouces = new List<AnimeSouce>();//动画的播放源
            ICollection<AnimeComm> animeComms = new List<AnimeComm>();  //动画的评论
            //var SubNum = 0;                                           //用户订阅量（暂时用不到呢）

            animeTags = Anime.Tags;
            animeSouces = Anime.Souce;
            animeComms = Anime.AnimeComms;
            if (Anime.AnimeNumUpdata())//计算动画集数
            {
                //需要更新动画信息
                _DBServices.SetCache(key, Anime);//全部的数据读取好之后，缓存一下
                await _DBServices.UpdateAsync(Anime);
            }
            //动画集数列表的相关计算
            AnimeNumberInfo animeNumberInfo = Anime.AnimeNumPage();

            AnimeProcess animeProcess = new AnimeProcess();

            if (!(IsSignIn = _signInManager.IsSignedIn(HttpContext.User))) //如果没登陆，后面的就不需要处理了
            {
                return View(
                    viewName: "Bangumi_OneAnime",
                    model: animeProcess.BuildModel<Bangumi_OneAnime>(model => 
                    {
                        model.Anime = Anime;
                        model.UserAnimeNumber = userAnimeNumber;
                        model.Memos = memo;
                        model.IsSub = IsSub;
                        model.IsSignIn = IsSignIn;
                        model.IsShowEdit = IsShowEdit;
                        model.Page = animeNumberInfo;
                    })
                    );
            }
            //如果没有登陆，返回Null
            var userID = _userManager.GetUserId(HttpContext.User);
            //尝试读取缓存
            key = new KEY { Key = CacheKey.Anime_User_Info(userID, id).ToCharArray() };
            if (!_DBServices.GetDate(key, out AnimeUserInfo Infos))
            {
                Infos = await _DBServices.GetDateOneAsync<AnimeUserInfo>(db => db.Where(info => info.Users.Id == userID && info.SubAnime.AnimeID == id)
                .Include(info => info.Memos));
                _DBServices.SetCache(key, Infos);
            }
            if (Infos != null)//没有订阅
            {
                IsSub = true;
                memo = Infos.Memos;
                userAnimeNumber = Infos.NowAnimeNum;
            }
            //检查权限
            var EditAnime = await _authorizationService.AuthorizeAsync(HttpContext.User, Final.Yuri_Yuri4);
            IsShowEdit = EditAnime.Succeeded;

            return View(
                viewName: "Bangumi_OneAnime",
                model:new Bangumi_OneAnime
                {
                    Anime = Anime,
                    UserAnimeNumber = userAnimeNumber,
                    Memos = memo,
                    IsSub = IsSub,
                    IsSignIn = IsSignIn,
                    IsShowEdit = IsShowEdit,
                    Page = animeNumberInfo
                }
                );
        }

        // GET: Bangumi/Create
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create)]
        public ActionResult Create()
        {
            //返回视图
            return View(
                viewName:"AddBangumi"
                );
        }

        // POST: Bangumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create_POST)]
        public async Task<IActionResult> CreateAsync(IFormCollection collection)
        {
            try
            {
                //上传的文件
                var files = collection.Files;
                if (files != null)
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
                var End = collection["IsEnd"];
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
                await _DBServices.AddAsync(anime);
                //最新发现，到这一步ID会有值o(*￣▽￣*)ブ

                _DBServices.AddAnimeID(anime.AnimeID);//这里不要忘记添加动画ID
                return RedirectToRoute(Final.Route_Bangumi_Index, anime.AnimeID);
            }
            catch
            {
                throw;//显示错误页面
            }
        }

        // GET: Bangumi/Edit/5
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Edit/{id:int}", Name = Final.Route_Bangumi_Edit)]
        public async Task<ActionResult> EditAsync(int id)
        {
            if (_DBServices.HasAnimeID(id))
            {
                var key = new KEY { Key = CacheKey.Anime_One(id).ToCharArray() };
                if (!_DBServices.GetDate(key, out Anime Anime))
                {
                    //如果缓存中没有就临时读取一下，因为提交后会删除缓存，所以不保存了
                    Anime = await _DBServices.GetDateOneAsync<Anime>(db =>
                            db.Where(a => a.AnimeID == id)
                            .Include(a => a.Tags));
                }
                return View(
                viewName: "BangumiEdit",
                model: new BangumiEdit
                {
                    Anime = Anime
                }
                );
            }
            return NotFound();
        }

        // POST: Bangumi/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Admin)]
        [Route("/Bangumi/Edit/{id:int}", Name = Final.Route_Bangumi_Edit_POST)]
        public async Task<ActionResult> EditAsync(int id, BangumiEdit bangumiEdit)
        {
            try
            {
                if (_DBServices.HasAnimeID(id))
                {
                    Anime Anime = bangumiEdit.Anime;
                    var NewTag = bangumiEdit.AddTag;

                    Anime anime = await _DBServices.GetDateOneAsync<Anime>(db => db.Where(a => a.AnimeID == id).Include(a => a.Tags));

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

                    await _DBServices.UpdateAsync(anime);
                    //清除旧缓存
                    _DBServices.Remove(new KEY { Key = CacheKey.Anime_One(id).ToCharArray() });
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
        [Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete)]
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
        [Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete_POST)]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                //_DBServices.RemoveAnimeID(id);
                return NotFound();
            }
            catch
            {
                new Tuple<string, User>("", new User());
                throw;
            }
        }

        ///===========================================================================
        ///===========================================================================
        ///===========================================================================

        /// <summary>
        /// 
        /// </summary>
        private void GetAnimeByID(int ID)
        {

        }
    }
}