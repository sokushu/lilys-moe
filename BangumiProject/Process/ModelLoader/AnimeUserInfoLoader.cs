using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.DBService;
using BangumiProject.Process.Exception;
using BaseProject.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BangumiProject.Process.ModelLoader
{
    public class AnimeUserInfoLoader : IModelLoader<AnimeUserInfo>
    {
        private IServices Services { get; set; }
        private int AnimeID { get; set; }
        private string UserID { get; set; }
        public AnimeUserInfoLoader(IServices services, int AnimeID, string UserID) : base("AnimeUserInfo")
        {
            this.AnimeID = AnimeID;
            this.UserID = UserID;
            Services = services;
        }
        public override AnimeUserInfo AfterProcess(AnimeUserInfo model)
        {
            return model;
        }

        public override AnimeUserInfo LoadModel()
        {
            if (UserID == null)
                return null;
            if (Services.HasAnimeID(AnimeID))
            {
                AnimeUserInfo Infos = Services.Save_ToFirst<AnimeUserInfo>(
                    CacheKey.Anime_User_Info(UserID, AnimeID), 
                    db => db.Where(info => info.Users.Id == UserID && info.SubAnime.AnimeID == AnimeID)
                                        .Include(info => info.Memos));
                return Infos;
            }
            else
            {
                return null;
            }
        }
    }
}
