//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Text;
//using System.Threading.Tasks;
//using BangumiProject.Models;
//using BangumiProject.Views.Bangumi;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using MoeUtilsBox.List;
//using MoeUtilsBox.String;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Cors;
//using Microsoft.AspNetCore.Http;
//using BangumiProject.Process.Bangumi;
//using Microsoft.Extensions.Caching.Memory;
//using Users = BangumiProject.Areas.Users.Models.Users;
//using UserAnimeInfo = BangumiProject.Areas.Bangumi.Models.AnimeUserInfo;
//using Memo = BangumiProject.Areas.Bangumi.Models.AnimeMemo;
//using BangumiProject.Areas.Bangumi.Models;
//using BangumiProject.Areas.Blogs.Models;

//namespace BangumiProject.Controllers
//{
//    /// <summary>
//    /// 
//    /// 有一个问题需要注意
//    /// 内存缓存与数据库更新的问题
//    /// 
//    /// 经过我的一些实验探索，发现从缓存中读取出的数据不能更新进数据库中（还是待确认中的……）
//    /// （例如试一下 _DB.Updata() 方法）
//    /// 所以，为了安全起见。
//    /// 遇到数据库更新的场景。
//    /// 先读取数据后，再进行更新
//    /// 
//    /// +++++++++++++++++++++++++++++++++++++++++++++++++++++++++
//    /// 后续更新！！更新！！更新！！！
//    /// 重要的事情要重复几遍才行！！！
//    /// 
//    /// 经过实验证实
//    /// _DB.Updata() 方法
//    /// 是有效的！！！
//    /// 
//    /// </summary>
//    public class BangumiController : Controller
//    {
//        /// <summary>
//        /// 用户数据库，以及工具的初始化
//        /// </summary>
//        private readonly UserManager<Users> _userManager;
//        private readonly BangumiProjectContext _DB;
//        private readonly SignInManager<Users> _signInManager;
//        private readonly MoeNumberUtils MoeUtils = new MoeNumberUtils();
//        private readonly MoeListUtils MoeList = new MoeListUtils();
//        private readonly BangumiProcess bangumiProcess = new BangumiProcess();
//        private readonly IAuthorizationService _authorizationService;
//        private readonly IMemoryCache _memoryCache;
//        private readonly WeekSwitch weekSwitch = new WeekSwitch();
//        /// <summary>
//        /// 加载数据库,各种所需要的数据
//        /// </summary>
//        /// <param name="_userManager">用户权限数据库</param>
//        /// <param name="DB"></param>
//        /// <param name="_signInManager"></param>
//        public BangumiController
//            (
//                UserManager<Users> _userManager, BangumiProjectContext _DB, 
//                SignInManager<Users> _signInManager, IAuthorizationService _authorizationService,
//                IMemoryCache _memoryCache
//            )
//        {
//            this._userManager = _userManager;
//            this._DB = _DB;
//            this._signInManager = _signInManager;
//            this._authorizationService = _authorizationService;
//            this._memoryCache = _memoryCache;
//        }
//        //============================================================================================================================

//        /// <summary>
//        /// 查询所有的动画数据，
//        /// 动画索引页面
//        /// 
//        /// 可以分类查询动画，
//        /// 按年份查询
//        /// 按动画的类型标签查询
//        /// 
//        /// 搜索动画
//        /// 
//        /// 具体可以参考Bilibili的动画分类页面
//        /// 
//        /// 参数是过滤选项。
//        /// 
//        /// </summary>
//        /// <param name="TagName">标签</param>
//        /// <param name="Page">分页，页数</param>
//        /// <param name="year">哪一年的动画</param>
//        /// <param name="session">哪一季的动画（1月4月7月10月）</param>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Bangumi", Name = "Bangumi")]
//        public async Task<IActionResult> IndexAsync(string TagName = "", int Page = 1, int year = 1, int session = 0)
//        {
//            //加载全部动画数据
//            if (!_memoryCache.TryGetValue(Final.Cache_AllAnime, out List<Anime> AllAnime))
//            {
//                AllAnime = await _DB.Anime.ToListAsync();
//                _memoryCache.Set(Final.Cache_AllAnime, AllAnime);
//            }
//            //加载全部动画标签数据（这里的数据，主要是动画类型的数据）
//            if (!_memoryCache.TryGetValue(Final.Cache_AllAnimeTags, out Dictionary<string, List<AnimeTag>> AllTags))
//            {
//                var TagList = await _DB.AnimeTag.Include(tag => tag.Anime).ToListAsync();
//                AllTags = weekSwitch.SwitchTagByName(TagList);
//                _memoryCache.Set(Final.Cache_AllAnimeTags, AllTags);
//            }
//            //得到当前动画的所有年份，用于页面中
//            if (!_memoryCache.TryGetValue(Final.Cache_AllAnimeYear, out List<int> AnimeYear))
//            {
//                AnimeYear = weekSwitch.GetAnimeYears(AllAnime);
//                _memoryCache.Set(Final.Cache_AllAnimeYear, AnimeYear);
//            }

//            //初始化数据
//            List<Anime> ProcessAnime = new List<Anime>(AllAnime);//得到最终处理过的动画
//            if (TagName != "")
//            {
//                var List = AllTags[TagName];
//                if (List == null)//如果没有这个标签的动画
//                {
//                    ProcessAnime = new List<Anime>();
//                }
//                else
//                {
//                    int[] animeID = List.Select(tags => tags.Anime.AnimeID).ToArray();
//                    HashSet<int> vs = new HashSet<int>(animeID);
//                    //选取符合标签的动画
//                    ProcessAnime = ProcessAnime.Where(a => vs.Contains(a.AnimeID)).ToList();
//                }
                
//            }
//            //对年进行检查
//            if (year != 1)//不等于1代表有值输入
//            {
//                int yNow = DateTime.Now.Year;
//                MoeUtils.NumberCheck(LineNum: 1900, ProcessStr: "<=", InputNumber: ref year, IfFalseDefauleNumber: yNow);
//                MoeUtils.NumberCheck(LineNum: (yNow + 10), ProcessStr: ">=", InputNumber: ref year, IfFalseDefauleNumber: yNow);
//                //得到当前输入的年份的动画
//                ProcessAnime = weekSwitch.SwitchAnime(ProcessAnime, WeekSwitch.SwitchType.Year, year)[0];
//            }
//            //对季度进行检查
//            if (session != 0)
//            {
//                int NowSeason = weekSwitch.SeasonNow;
//                MoeUtils.NumberCheck(LineNum: 4, ProcessStr: "<", InputNumber: ref session, IfFalseDefauleNumber: NowSeason);
//                MoeUtils.NumberCheck(LineNum: 1, ProcessStr: ">", InputNumber: ref session, IfFalseDefauleNumber: NowSeason);
//                //得到当前输入的季度的动画
//                ProcessAnime = weekSwitch.SwitchAnime(ProcessAnime, WeekSwitch.SwitchType.Season)[session];
//            }
            
//            //对Page值进行检查
//            MoeUtils.NumberCheck(LineNum: 0, ProcessStr: "<=", InputNumber: ref Page, IfFalseDefauleNumber: 1);
//            //开始处理动画数据
//            PageHelper pageHelper = new PageHelper(20);
//            //对动画进行分页
//            List<Anime> animes = pageHelper.GetListPage(Page, ProcessAnime);

//            int AllPage = pageHelper.GetAllPage();
//            int NowPage = pageHelper.GetNowPage();
//            List<string> AnimeTags = AllTags.Keys.ToList();

//            // 页面渲染
//            return View(
//                viewName: "Bangumi",
//                model: new BangumiModel
//                {
//                    AllPage = AllPage,      //处理后动画的全部页数
//                    NowPage = NowPage,      //现在看到的页数
//                    Animes = animes,        //处理后的动画集合
//                    AnimeSeason = new List<int>{ 1, 2, 3, 4 },//动画的季度（总共就4个季度，而且是常量的）
//                    AnimeTags = AnimeTags,  //动画的标签合集
//                    AnimeYear = AnimeYear   //动画的年份合集
//                });
//        }

//        /// <summary>
//        /// 查询一部动画的数据
//        /// 如果用户登陆，还可以返回登陆过后用户对这部动画做的信息
//        /// </summary>
//        /// <param name="id">需要查询的动画ID</param>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Bangumi/{id?}", Name = "GetBangumi")]
//        public async Task<IActionResult> GetBangumi(int id = 0)
//        {
//            //从缓存中读取
//            /*
//             * 从缓存中尝试读取数据，如果没有数据，就从数据库中读取，并缓存到内存中。
//             * 注意：这一个缓存已经基本包含全部有关动画的数据了
//             */
//            Anime anime;
//            if ((anime = await bangumiProcess.Anime_CacheSetOrGetAsync(id, _memoryCache, _DB)) == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }

//            //初始化数据……
//            var userAnimeNumber = 0;                                    //动画观看集数
//            var IsSignIn = false;                                       //用户是否登录
//            var IsSub = false;                                          //用户是否订阅某动画
//            ICollection<Memo> memo = new List<Memo>();                  //用户写下的MEMO
//            ICollection<Blogs> blogs = new List<Blogs>();                 //用户对该动画的长评短评
//            ICollection<AnimeTag> animeTags = new List<AnimeTag>();     //动画的标签
//            ICollection<AnimeSouce> animeSouces = new List<AnimeSouce>();//动画的播放源
//            ICollection<AnimeComm> animeComms = new List<AnimeComm>();  //动画的评论
//            var SubNum = 0;                                             //用户订阅量
//            var IsShowEdit = false;                                     //用户是否可以编辑
                
//            //计算动画集数
//            if (bangumiProcess.AnimeNumUpdata(ref anime))
//            {
//                //需要更新动画信息
//                await _DB.SaveChangesAsync();
//                //需要重新加入缓存
//                _memoryCache.Set(id, anime);
//            }
//            //读取动画评论
//            animeComms = anime.AnimeComms;
//            //读取动画的短评，长评
//            blogs = _memoryCache.Get<List<Blogs>>($"Blogs{id}");
//            //读取动画的标签
//            if (anime.Tags != null)
//            {
//                animeTags = anime.Tags;
//            }
//            //读取动画的播放源
//            if (anime.Souce != null)
//            {
//                animeSouces = anime.Souce;
//            }
//            //获取用户订阅量
//            SubNum = await _DB.UserAnimeInfos.Where(uas => uas.SubAnime.Equals(anime)).CountAsync();
//            //判断用户是否已经登陆
//            if (IsSignIn = _signInManager.IsSignedIn(HttpContext.User))    
//            {
//                var User = await _userManager.GetUserAsync(HttpContext.User);
//                //找用户订阅的动画信息
//                var UserAnimeInfo = await _DB.UserAnimeInfos.Include(info => info.Memos).SingleOrDefaultAsync(a => a.SubAnime.Equals(anime) && a.Users.Equals(User));
//                if (UserAnimeInfo == null)// 用户没有订阅动画
//                {
//                    IsSub = false;
//                }
//                else
//                {
//                    userAnimeNumber = UserAnimeInfo.NowAnimeNum;
//                    memo = UserAnimeInfo.Memos;
//                    IsSub = true;
//                }

//                //验证用户是否有编辑动画的权限
//                var EditAnime = await _authorizationService.AuthorizeAsync(HttpContext.User, Final.Yuri_Yuri5);
//                IsShowEdit = EditAnime.Succeeded;
//            }

//            //动画集数列表的相关计算
//            int AllNum = anime.AnimeNum;//动画的总集数
//            int Page = AllNum;
//            foreach (var item in new int[]{ 10, 20, 30, 40 })
//            {
//                if (MoeUtils.NumberCheck(item, ">", ref Page, AllNum, (item / 10)))
//                {
//                    break;
//                }
//            }

//            return View(
//            viewName: "Bangumi_OneAnime",
//            model: new Bangumi_OneAnimeModel
//            {
//                Anime = anime,
//                UserAnimeNumber = userAnimeNumber,
//                Memos = memo,
//                IsSub = IsSub,
//                IsSignIn = IsSignIn,
//                IsShowEdit = IsShowEdit
//            });
//        }

//        /// <summary>
//        /// 订阅一部动画
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize] //需要登陆访问
//        [EnableCors("Test")]
//        [Route("/Bangumi/{id?}/Sub", Name = "BangumiSub")]
//        public async Task<IActionResult> BangumiSub(int id = 0)
//        {
//            // 读取动画数据
//            var anime = await _DB.Anime.FirstOrDefaultAsync(OneAnime => OneAnime.AnimeID.Equals(id));
//            if (anime == null)
//            {
//                // 没有找到动画
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                //找到动画了，进行下一步
//                //看是否已经定阅过了
//                var user = await _userManager.GetUserAsync(HttpContext.User);
//                var info = await _DB.UserAnimeInfos.FirstOrDefaultAsync(animeInfo => animeInfo.Users.Equals(user) && animeInfo.SubAnime.Equals(anime));
//                if (info == null)
//                {
//                    await _DB.UserAnimeInfos.AddAsync(new UserAnimeInfo
//                    {
//                        NowAnimeNum = 0,
//                        SubAnime = anime,
//                        Users = user
//                    });

//                    await _DB.SaveChangesAsync();
//                    List<string> json = new List<string> { "true", "订阅成功" };
//                    return Json(json);
//                }
//                else
//                {
//                    List<string> json = new List<string> { "false", "您已经定阅过" };
//                    return Json(json);
//                }
//            }
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize(Policy = Final.Yuri_Yuri5)]
//        [Route("/Bangumi/{id?}/Edit", Name = "AnimeEdit")]
//        public async Task<IActionResult> GetBangumiEdit(int id = 0)
//        {
//            // 读取动画数据
//            Anime anime;
//            if ((anime = await bangumiProcess.Anime_CacheSetOrGetAsync(id, _memoryCache, _DB)) == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                //找到动画了，进行下一步
//                return View(
//                    viewName: "BangumiEdit",
//                    model: new BangumiEditModel
//                    {
//                        Anime = anime
//                    });
//            }
//        }

//        /// <summary>
//        /// 编辑动画数据
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize(Policy = Final.Yuri_Yuri5)] // 需要登陆访问
//        [ValidateAntiForgeryToken]
//        [Route("/Bangumi/{id?}", Name = "PostBangumi")]
//        public async Task<IActionResult> PostBangumi(BangumiEditModel bangumiEdit, int id = 0)
//        {
//            Anime Anime = bangumiEdit.Anime;
//            string TagName = bangumiEdit.AddTag;
//            Anime anime = await _DB.Anime.Where(a => a.AnimeID == id).Include(a => a.Tags).FirstOrDefaultAsync();
//            if (anime != null)
//            {
//                anime.AnimeInfo = Anime.AnimeInfo;
//                anime.AnimeName = Anime.AnimeName;
//                anime.AnimeNum = Anime.AnimeNum;
//                anime.AnimePic = Anime.AnimePic;
//                anime.AnimeType = Anime.AnimeType;
//                anime.IsEnd = Anime.IsEnd;
//                anime.AnimePlayTime = Anime.AnimePlayTime;

//                if (!string.IsNullOrEmpty(TagName) && !string.IsNullOrWhiteSpace(TagName))
//                {
//                    anime.Tags.Add(new AnimeTag
//                    {
//                        TagName = bangumiEdit.AddTag
//                    });
//                }

//                _DB.Anime.Update(anime);//对数据进行更新
//                await _DB.SaveChangesAsync();
//                //删除旧缓存
//                _memoryCache.Remove(id);
//            }
//            else
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            return RedirectToRoute("GetBangumi", id);
//        }

//        /// <summary>
//        /// 变更用户订阅动画的集数
//        /// </summary>
//        /// <param name="Num"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [Authorize]
//        //[ValidateAntiForgeryToken]
//        [EnableCors("Test")]
//        [Route("/Bangumi/{id?}/Up", Name = "BangumiUp")]
//        public async Task<IActionResult> BangumiUp(int id = 0, int Num = 1)
//        {
//            // 对传送过来的动画集数做检查
//            if (1 > Num)
//            {
//                // 不对动画集数做任何修改
//                return Json("Error");
//            }
//            else
//            {
//                var anime = await _DB.Anime.FirstOrDefaultAsync(ani => ani.AnimeID.Equals(id));
//                var user = await _userManager.GetUserAsync(HttpContext.User);
//                if (anime != null)
//                {
//                    var info = await _DB.UserAnimeInfos.FirstOrDefaultAsync(infos => infos.SubAnime.Equals(anime) && infos.Users.Equals(user));
//                    if (info == null)
//                    {
//                        return Json(new List<string> { "false", "没有订阅动画，请先订阅动画" });
//                    }
//                    info.NowAnimeNum = Num;

//                    await _DB.SaveChangesAsync();
//                    return Json(new List<string> { "true", "更改成功" });
//                }
//                else
//                {
//                    return StatusCode((int)HttpStatusCode.NotFound);
//                }
//            }
//        }

//        /// <summary>
//        /// 返回
//        /// 添加动画的页面
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Authorize(Policy = Final.Yuri_Yuri5)] // 只有yu5权限以上的人才能访问
//        // 特定权限用户才能访问
//        [Route("/Bangumi/Add", Name = "GetAddBangumi")]
//        public IActionResult GetAddBangumi()
//        {
//            return View("AddBangumi");
//        }

//        /// <summary>
//        /// 将动画数据保存到数据库中
//        /// </summary>
//        /// <returns></returns>
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        [Authorize(Policy = Final.Yuri_Yuri5)]
//        // 只有特定用户能添加动画
//        [Route("/Bangumi/Add", Name = "PostAddBagnumi")]
//        public async Task<IActionResult> PostAddBangumi(Anime Anime)
//        {
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    await _DB.Anime.AddAsync(Anime);
//                    await _DB.SaveChangesAsync();
//                }
//                catch (Exception)
//                {
//                    return RedirectToRoute("Index");
//                }
//            }
//            return RedirectToRoute("Index");

//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="memo"></param>
//        /// <returns></returns>
//        [HttpPost]
//        [Route("/Bangumi/{id?}/Memos", Name = "PostMemo")]
//        [Authorize]
//        public async Task<IActionResult> PostMemo(int id, Memo memo)
//        {
//            var Anime = await _DB.Anime.FirstOrDefaultAsync(anime => anime.AnimeID.Equals(id));
//            if (Anime == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                var User = await _userManager.GetUserAsync(HttpContext.User);
//                //获取用户动画信息
//                var UserAnimeInfo = await _DB.UserAnimeInfos.Include(info => info.Memos).FirstOrDefaultAsync(a => a.SubAnime.Equals(Anime) && a.Users.Equals(User));
//                memo.ID = 0;// 这里的ID会与参数的id相同，造成数据库错误，所以，这里是需要初始化一下，使用数据库的Key自增策略
//                //将Memo添加进入
//                UserAnimeInfo.Memos.Add(memo);

//                await _DB.SaveChangesAsync();
//            }
//            return RedirectToRoute("GetBangumi", id);
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Bangumi/{id?}/Memos", Name = "GetMemo")]
//        [Authorize]
//        public IActionResult GetMemoPage(int id)
//        {
//            return View("Memo");
//        }

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="memoid"></param>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("/Bangumi/{id?}/Memos/{memoid?}/Edit", Name = "GetEditMemo")]
//        [Authorize]
//        public async Task<IActionResult> GetMemoEdit(int id, int memoid)
//        {
//            var User = await _userManager.GetUserAsync(HttpContext.User);
//            //获取用户动画信息
//            var UserAnimeInfo = await _DB.UserAnimeInfos.Include(info => info.Memos).FirstOrDefaultAsync(a => a.SubAnime.AnimeID.Equals(id) && a.Users.Equals(User));
//            if (UserAnimeInfo == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            //获取用户的MEMO-ID下的MEMO
//            var memos = await _DB.Memos.FirstOrDefaultAsync(memo => memo.ID.Equals(memoid) && memo.UserAnimeInfo.Equals(UserAnimeInfo));
//            if (memos == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            //渲染页面
//            return View(
//            viewName: "Memo",
//            model: memos
//            );  
//        }

//        /// <summary>
//        /// 编辑动画的Memo页面
//        /// </summary>
//        /// <param name="id">动画ID</param>
//        /// <param name="memoid">MEMO的ID</param>
//        /// <param name="Memo">MEmo实体</param>
//        /// <returns></returns>
//        [HttpPost]
//        [Route("/Bangumi/{id?}/Memos/{memoid?}/Edit", Name = "PostEditMemo")]
//        [Authorize]
//        public async Task<IActionResult> PostMemoEdit(int id, int memoid, Memo Memo)
//        {
//            var Anime = await _DB.Anime.FirstOrDefaultAsync(anime => anime.AnimeID.Equals(id));
//            if (Anime == null)
//            {
//                return StatusCode((int)HttpStatusCode.NotFound);
//            }
//            else
//            {
//                var User = await _userManager.GetUserAsync(HttpContext.User);
//                //获取用户动画信息
//                var UserAnimeInfo = await _DB.UserAnimeInfos.Include(info => info.Memos).FirstOrDefaultAsync(a => a.SubAnime.Equals(Anime) && a.Users.Equals(User));
//                if (UserAnimeInfo == null)
//                {
//                    return StatusCode((int)HttpStatusCode.NotFound);
//                }
//                var memos = await _DB.Memos.FirstOrDefaultAsync(memo => memo.ID.Equals(memoid) && memo.UserAnimeInfo.Equals(UserAnimeInfo));
//                if (memos == null)
//                {
//                    return StatusCode((int)HttpStatusCode.NotFound);
//                }
//                else
//                {
//                    memos.NowAnimeNum = Memo.NowAnimeNum;
//                    memos.MemoStr = Memo.MemoStr;

//                    _DB.Entry(memos).State = EntityState.Modified;
//                    await _DB.SaveChangesAsync();
//                }
//            }
//            return RedirectToRoute("GetBangumi", id);
//        }

        
//    }
//}