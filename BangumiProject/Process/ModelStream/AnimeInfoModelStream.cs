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
        public override AnimeInfoModel Build()
        {
            return new AnimeInfoModel
            {
                Anime = (Anime)     Get(0),
                IsSub = (bool)      Get(1)
            };
        }
    }
}
