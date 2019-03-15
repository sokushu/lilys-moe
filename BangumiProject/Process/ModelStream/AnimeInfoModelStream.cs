using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Core;
using BangumiProject.Process.PageModels;
using BaseProject.Core;
using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.ModelStream
{
    public class AnimeInfoModelStream : IModelStream<AnimeInfoModel>
    {
        public AnimeInfoModelStream() : base("AnimeInfoModel")
        {
            
        }

        public AnimeInfoModelStream(ISender sender):base(sender, "AnimeInfoModel") { }

        public override void BuildRulu()
        {
            Open();
        }

        public override AnimeInfoModel Build()
        {
            var anime = (Anime)values["Anime"];
            var animeuserinfos = (AnimeUserInfo)values["AnimeUserInfo"];
            var showedit = (bool)values["Authorization"];
            AnimeInfoModel animeInfoModel = new AnimeInfoModel()
            {
                Anime = anime,
                IsSignIn = animeuserinfos == null ? false : true,
                Memos = animeuserinfos?.Memos,
                UserAnimeNumber = animeuserinfos?.NowAnimeNum ?? 0,
                IsShowEdit = animeuserinfos == null ? false : showedit
            };
            
            return animeInfoModel;
        }
    }
}
