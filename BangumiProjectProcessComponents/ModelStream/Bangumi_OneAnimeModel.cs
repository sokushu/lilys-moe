using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels;
using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectProcessComponents.ModelStream
{
    public class Bangumi_OneAnimeModel : IModelStream<Bangumi_OneAnime>
    {
        public Bangumi_OneAnimeModel() : base("Bangumi_OneAnime")
        {

        }

        /// <summary>
        /// 
        /// </summary>
        public override void BuildRulu()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override Bangumi_OneAnime Build()
        {
            Bangumi_OneAnime ReturnBangumi_OneAnime = new Bangumi_OneAnime();

            Anime Anime = (Anime)values["Anime"];
            AnimeUserInfo animeUserInfo = (AnimeUserInfo)values["AnimeUserInfo"];

            bool IsNull = animeUserInfo == null;

            ReturnBangumi_OneAnime.Anime = Anime;
            ReturnBangumi_OneAnime.UserAnimeNumber = IsNull ? 0 : animeUserInfo.NowAnimeNum;
            ReturnBangumi_OneAnime.IsSub = !IsNull;

            return ReturnBangumi_OneAnime;
        }
    }
}
