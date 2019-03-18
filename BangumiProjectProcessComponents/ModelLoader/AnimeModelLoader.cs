using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.Services;
using BaseProject.Core;
using BaseProject.Exceptionss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BangumiProjectProcessComponents.ModelLoader
{
    public class AnimeModelLoader : IModelLoader<Anime>
    {
        private IServices Services { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public AnimeModelLoader(IServices services) : base("Anime")
        {
            Services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override Anime AfterProcess(Anime model)
        {
            if (AnimeNumUpdata(ref model))
            {
                int AnimeID = model.AnimeID;
                string cacheKey = CacheKey.Anime_One(AnimeID);
                Services.Save_Updata(cacheKey, model).Commit();
            }
            return model;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Input">AnimeID:int</param>
        /// <returns></returns>
        public override Anime LoadModel(params object[] Input)
        {
            if (Input.Length > 0)
            {
                int AnimeID = (int)Input[0];
                if (Services.HasAnimeID(AnimeID))
                {
                    string cacheKey = CacheKey.Anime_One(AnimeID);
                    Anime anime = Services.Save_ToFirst<Anime>(cacheKey,
                        db => db.Where(ani => ani.AnimeID == AnimeID));
                    return anime;
                }
                else
                {
                    throw NotFound("Not Found");
                }
            }
            else
            {
                throw NotFound("Input Is Null");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        private Exception NotFound(string info)
        {
            return new AnimeNotFoundException(info);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        private bool AnimeNumUpdata(ref Anime anime)
        {
            if (anime.IsEnd == false)
            {
                //获取数据库中的动画集数
                var AnimeNumber = anime.AnimeNum;
                //计算现在实际应该更新到哪里，与数据库中的数据做对比
                var StartDate = anime.AnimePlayTime;
                var Tsu = StartDate.AddDays((AnimeNumber) * 7);
                //遇到特殊情况的处理对策（例如，临时停播，放送事故造成的变更）
                bool Stop = false;
                if (DateTime.Compare(DateTime.Now, Tsu) >= 0)//如果今天的日期大于数据库储存的（集数的下一集的日期），那就开始计算吧
                {
                    int num = 0;
                    while (true)
                    {
                        if (DateTime.Compare(DateTime.Now, Tsu) >= 0)
                        {
                            if (!Stop)//如果不是停播的话，动画加一
                            {
                                AnimeNumber = AnimeNumber + 1;
                            }
                            Tsu = Tsu.AddDays(7);
                        }
                        else
                        {
                            num = AnimeNumber;
                            break;
                        }
                    }

                    anime.AnimeNum = num;
                }
                return true;
            }
            return false;
        }
    }
}
