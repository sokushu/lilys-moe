using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.AnimeProcess.AnimeProcessC;
using BangumiProject.Process.Core;
using BangumiProject.Process.DBService;
using BangumiProject.Process.Exception;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BangumiProject.Process.ModelLoader
{
    /// <summary>
    /// 读取指定ID的动画
    /// </summary>
    public class AnimeLoader : IModelLoader<Anime>
    {
        private IServices Services { get; }

        private int AnimeID { get; }
        public AnimeLoader(
            IServices services,
            int AnimeID
            )
        {
            Services = services;
            this.AnimeID = AnimeID;
        }

        public override Anime AfterProcess(Anime model)
        {
            if (model == null)
            {
                throw new NotFoundAnimeInfoException($"Not Found By Anime ID :{AnimeID}");
            }
            else
            {
                // NullCheck
                model.Souce = NullCheck(model.Souce);
                model.Tags = NullCheck(model.Tags);
                model.AnimeComms = NullCheck(model.AnimeComms);

                //AnimeNumber Updata
                CoreProcess<Anime> coreProcess = new CoreProcess<Anime>();
                coreProcess.SetData(model);
                var ReturnValue = coreProcess.SetProcess2(new AnimeProcessByAnimeNumber());
                var ProcessEDAnime = coreProcess.GetData<Anime>();
                if (ReturnValue)
                {
                    //需要更新动画信息
                    var AnimeCacheKey = CacheKey.Anime_One(AnimeID);
                    Services.Save_Updata(AnimeCacheKey, ProcessEDAnime).Commit();
                }
                return ProcessEDAnime;
            }
        }

        public override Anime LoadModel()
        {
            if (Services.HasAnimeID(AnimeID))
            {
                var AnimeCacheKey = CacheKey.Anime_One(AnimeID);
                Anime Anime = Services.Save_ToFirst<Anime>(AnimeCacheKey, db =>
                            db.Where(a => a.AnimeID == AnimeID)
                            .Include(a => a.Souce)
                            .Include(a => a.Tags)
                            .Include(a => a.AnimeComms));
                return Anime;
            }
            else
            {
                return null;
            }
        }

        private ICollection<T> NullCheck<T>(ICollection<T> ts)
        {
            if (ts == null)
            {
                return new List<T>();
            }
            return ts;
        }
    }
}
