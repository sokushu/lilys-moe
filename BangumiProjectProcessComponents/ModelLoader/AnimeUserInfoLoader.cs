using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;
using BaseProject.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangumiProjectProcessComponents.ModelLoader
{
    public class AnimeUserInfoLoader : IModelLoader<AnimeUserInfo>
    {
        private IServices Services { get; set; }
        public AnimeUserInfoLoader(IServices Services) : base(nameof(AnimeUserInfo))
        {
            this.Services = Services;
        }
        public override AnimeUserInfo AfterProcess(AnimeUserInfo model)
        {
            return model;
        }

        /// <summary>
        /// param : UID:string, AnimeID:int
        /// </summary>
        /// <param name="Input">UID:string, AnimeID:int</param>
        /// <returns></returns>
        public override AnimeUserInfo LoadModel(params object[] Input)
        {
            string userID = (string)Input[0];
            int id = (int)Input[1];

            if (userID == null)
            {
                return null;
            }
            string cacheKey = CacheKey.Anime_User_Info(userID, id);
            AnimeUserInfo Infos = Services.Save_ToFirst<AnimeUserInfo>(cacheKey, 
                db => db.Where(info => info.Users.Id == userID && info.SubAnime.AnimeID == id)
                                        .Include(info => info.Memos));
            return Infos;
        }
    }
}
