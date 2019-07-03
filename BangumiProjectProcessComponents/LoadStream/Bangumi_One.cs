using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BangumiProjectDBServices.Services;
using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangumiProjectProcessComponents.LoadStream
{
    public class Bangumi_One : LoaderStream<Bangumi_OneAnime>
    {

        private IServices Services { get; set; }

        public Bangumi_One(
            IServices _Services
            ) :base()
        {
            Services = _Services;
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

            Bangumi_OneAnime bangumi_OneAnime = new Bangumi_OneAnime();

            Anime anime = Services.Save_ToFirst<Anime>
                (CacheKey.Anime_One(AnimeID), db => db.Where(an => an.AnimeID == AnimeID));
            AnimeUserInfo userInfo = Services.Save_ToFirst<AnimeUserInfo>
                (CacheKey.Anime_User_Info(UID, AnimeID), db => db.Where(info => info.Users.Id == UID && info.SubAnime.AnimeID == AnimeID));

            bangumi_OneAnime.Anime = anime;
            if (userInfo != null)
            {
                bangumi_OneAnime.UserAnimeNumber = userInfo.NowAnimeNum;
                bangumi_OneAnime.IsSub = true;
                bangumi_OneAnime.IsSignIn = !(UID == string.Empty);
                bangumi_OneAnime.Memos = userInfo.Memos;
                bangumi_OneAnime.IsShowEdit = true;//这里将来要改变一下
            }
            return bangumi_OneAnime;
        }
    }
}
