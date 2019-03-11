using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Core;
using BangumiProject.Process.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.ModelStream
{
    public class AnimeInfoModelStream : ModelStream<AnimeInfoModel>
    {
        private bool YuriMode { get; set; } = false;
        private bool IsSignIn { get; set; } = false;
        public AnimeInfoModelStream(bool YuriMode, bool IsSignIn)
        {
            this.YuriMode = YuriMode;
            this.IsSignIn = IsSignIn;
        }

        public override AnimeInfoModel Build()
        {
            AnimeInfoModel animeInfoModel = new AnimeInfoModel
            {
                Anime = (Anime)Get(0),
                IsSignIn = (bool)Get(1)
            };
            if (YuriMode)
            {
                return animeInfoModel;
            }
            animeInfoModel.HasKenGen = false;
            
            

            return new AnimeInfoModel
            {
                Anime = (Anime)     Get(0),
                IsSub = (bool)      Get(1)
            };
        }

        public override void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            base.SetModelLoader(modelLoader);
        }
    }
}
