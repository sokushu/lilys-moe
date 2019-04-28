using System;
using Microsoft.AspNetCore.Mvc;
using BaseProject.Core;
using BangumiProjectDBServices.Services;
using BaseProject.Exceptionss;
using BangumiProjectDBServices.PageModels;
using BangumiProjectProcessComponents.PageSwitch;
using BangumiProjectProcessComponents.ModelStream;
using BangumiProjectProcessComponents.ModelLoader;
using BangumiProjectProcessComponents.Process;
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
            IAuthorizationService _AuthorizationService
            ) :base(
                DBServices: _Services,
                UserManager: _UserManager,
                AuthorizationService: _AuthorizationService
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
            /*
             * 从缓存中读取数据，如果缓存中没有就从数据库中读取数据
             */
            var ListAnime = DBServices.Save_ToList<Anime>(CacheKey.Anime_All(),
                    db => db.Select(a => a));
            var ListTag = DBServices.Save_ToList<AnimeTag>(CacheKey.Anime_AllTags(),
                    db => db.Include(a => a.Anime));
            /*
             * 这里是一个变化高发区
             * 未来可能会加入不同的过滤条件
             */
            //AnimeFilters animeFilter = new AnimeFilters();


            ////动画是否完结
            //animeFilter.SetAnimeFilter(new AnimeFilterByEnd(animestats));
            ////动画的年份
            //animeFilter.SetAnimeFilter(new AnimeFilterByYear(year));
            ////动画的类型
            //animeFilter.SetAnimeFilter(new AnimeFilterByAnimeType(animetype));
            ////动画播出日是星期几
            //animeFilter.SetAnimeFilter(new AnimeFilterByWeek(dayofweek));
            ////这是哪一季度的动画
            //animeFilter.SetAnimeFilter(new AnimeFilterBySeason(season));
            ////添加标签过滤
            //animeFilter.SetAnimeFilter(new AnimeFilterByTagNameYuriMode(tagname, ListTag, YuriMode, string.Empty));


            ////返回最终的过滤结果集
            //var Animes = animeFilter.GetAnimeFilter(ListAnime);
            //PageHelper pageHelper = new PageHelper(20);
            //return View(
            //    viewName: "Bangumi",
            //    model: new Views.Bangumi.Model.Bangumi
            //    {
            //        AllPage = pageHelper.GetAllPage(),      //处理后动画的全部页数
            //        NowPage = pageHelper.GetNowPage(),      //现在看到的页数
            //        Animes = pageHelper.GetListPage(Page, Animes),        //处理后的动画集合
            //        AnimeSeason = new List<int> { 1, 2, 3, 4 },//动画的季度（总共就4个季度，而且是常量的）
            //        //AnimeTags = filter.AnimeTagName,  //动画的标签合集
            //        //AnimeYear = filter.AnimeYear   //动画的年份合集
            //    }
            //    );
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
        public async Task<IActionResult> DetailsAsync(int id = -1)
        {
            var Result = await AuthorizationService.AuthorizeAsync(HttpContext.User, Final.Yuri_Yuri4);
            var UID = UserManager.GetUserId(HttpContext.User);
            bool isOK = Result.Succeeded;
            // 百合模式检查
            YuriMode = HttpContext.YuriModeCheck();
            try
            {
                // 初始化
                CorePageLoader corePageLoader = new CorePageLoader();

                Bangumi_OneAnimeModelStream bangumi_OneAnimeModelStream = new Bangumi_OneAnimeModelStream();
                // 加载数据
                ShowNotYuriPage = bangumi_OneAnimeModelStream.SetModelLoader
                    (new AnimeModelLoader(DBServices).SetParams(id), new IsShowYuriPage(YuriName));
                bangumi_OneAnimeModelStream.SetModelLoader
                    (new AnimeUserInfoLoader(DBServices).SetParams(UID, id));
                bangumi_OneAnimeModelStream.SetModelLoader
                    (new BoolLoader(nameof(Bangumi_OneAnime.IsShowEdit)).SetParams(isOK));

                corePageLoader.SetModelStream(bangumi_OneAnimeModelStream);
                
                //构建数据
                IPage PageW = new Bangumi_OneAnimePageSwitch(YuriMode, ShowNotYuriPage);
                return View(
                    viewName: corePageLoader.GetPage(PageW),
                    model: corePageLoader.Build<Bangumi_OneAnime>()
                    );
            }
            catch (AnimeNotFoundException NotFoundAnime)
            {
                return NotFound(NotFoundAnime);
            }
            catch (Exception info)
            {
                throw info;
            }
        }

        // GET: Bangumi/Create
        [HttpGet]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create)]
        public ActionResult Create()
        {
            //返回视图
            return View(
                viewName: "AddBangumi"
                );
        }

        // POST: Bangumi/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Final.Yuri_Yuri4)]
        [Route("/Bangumi/Create", Name = Final.Route_Bangumi_Create_POST)]
        public IActionResult CreateAsync(IFormCollection collection)
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
                /*######################   BUG    #########################
                 * 这里会出现BUG
                 * 造成一开始不能选中完结动画
                 * 解决办法就是产生新的Anime对象了
                 * 把Anime当作参数
                 * 过一段时间再解决这个问题吧
                 *######################   BUG    #########################
                 */
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
                DBServices.Add(anime).Commit();
                //最新发现，到这一步ID会有值o(*￣▽￣*)ブ
                UpDataNew4(anime);//更新首页的最新4个动画的缓存
                UpDataNotEnd(anime);
                DBServices.ADDAnimeID(anime.AnimeID);//这里不要忘记添加动画ID
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
        public ActionResult EditAsync(int id)
        {
            if (DBServices.HasAnimeID(id))
            {
                

                return View(
                viewName: "BangumiEdit",
                model: new AnimeEdit
                {
                    Anime = null
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

        //// GET: Bangumi/Delete/5
        //[HttpGet]
        //[Authorize(Policy = Final.Yuri_Admin)]
        //[Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete)]
        //public ActionResult Delete(int id)
        //{
        //    return NotFound();
        //}

        ///// <summary>
        ///// 删除一部动画
        ///// 我们这里要是用软删除
        ///// 不是真的删除，而是标记为删除
        ///// </summary>
        ///// <param name="id">要删除的ID</param>
        ///// <param name="collection">删除的原因，以留做备案用</param>
        ///// <returns></returns>
        //// POST: Bangumi/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[Authorize(Policy = Final.Yuri_Admin)]
        //[Route("/Bangumi/Delete/{id?}", Name = Final.Route_Bangumi_Delete_POST)]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
        //        //_DBServices.RemoveAnimeID(id);
        //        return NotFound();
        //    }
        //    catch
        //    {
        //        new Tuple<string, User>("", new User());
        //        throw;
        //    }
        //}

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

        /// <summary>
        /// 
        /// </summary>
        private void YURIModeCheck()
        {
            //获取百合模式
            int? mode = HttpContext.Session.GetInt32(Final.YuriMode);
            if (mode != null)
            {
                YuriMode = mode == 1 ? true : false;
            }
        }
    }
}