using BangumiProject.Areas.Error.Models;
using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BangumiProjectDBServices.Services;
using BaseProject.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.CoreProcess.Bangumi
{
    /// <summary>
    /// 
    /// </summary>
    public class LoadStreamBangumi_One : LoaderStream<Bangumi_OneAnime>
    {
        /// <summary>
        /// 
        /// </summary>
        private IServices Services { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_Services"></param>
        public LoadStreamBangumi_One(
            IServices _Services
            ) : base()
        {
            Services = _Services;
        }

        /// <summary>
        /// 设置参数：AnimeID,UID
        /// </summary>
        /// <param name="Value"></param>
        public override void SetParams(params object[] Value)
        {
            base.SetParams(Value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value">AnimeID,UID</param>
        /// <returns></returns>
        protected override Bangumi_OneAnime Load(params object[] Value)
        {
            int AnimeID = (int)Value[0];
            string UID = (string)Value[1] ?? string.Empty;

            AnimeUserInfo userInfo = null; //进行初始化

            Anime anime = Services.Save_ToFirst<Anime>
                (CacheKey.Anime_One(AnimeID), db => db.Where(an => an.AnimeID == AnimeID).Include(an => an.Tags));

            Bangumi_OneAnime bangumi_OneAnime = new Bangumi_OneAnime
            {
                Anime = anime,
                IsSignIn = UID != string.Empty
            };

            if (bangumi_OneAnime.IsSignIn)//证明已经登录
            {
                userInfo = Services.Save_ToFirst<AnimeUserInfo>
                (CacheKey.Anime_User_Info(UID, AnimeID), db => db.Where(info => info.Users.Id == UID && info.SubAnime.AnimeID == AnimeID));
            }

            if (userInfo != null)//证明已经订阅动画
            {
                bangumi_OneAnime.IsSub = true;
                bangumi_OneAnime.Memos = userInfo.Memos;
                bangumi_OneAnime.UserAnimeNumber = userInfo.NowAnimeNum;
                bangumi_OneAnime.IsShowEdit = true;//这里将来要改变一下
            }
            else
            {
                bangumi_OneAnime.IsSub = false;
            }

            return bangumi_OneAnime;
        }
    }
}
