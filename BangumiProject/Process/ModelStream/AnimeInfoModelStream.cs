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
        public AnimeInfoModelStream(bool YuriMode)
        {
            this.YuriMode = YuriMode;
        }

        public override AnimeInfoModel Build()
        {
            AnimeInfoModel animeInfoModel = new AnimeInfoModel
            {
                Anime = Get<Anime>(0),
                IsSignIn = Get<bool>(1)
            };
            if (YuriMode)//如果是百合模式
            {
                //
                return animeInfoModel;
            }

            if (IsSignIn)//如果已经登陆
            {
                return animeInfoModel;
            }

            return animeInfoModel;
        }

        public override void SetModelLoader<T>(IModelLoader<T> modelLoader)
        {
            base.SetModelLoader(modelLoader);
        }
    }
}
