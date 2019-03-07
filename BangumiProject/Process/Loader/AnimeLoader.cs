using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Loader
{
    public class AnimeLoader : IModelLoader<Anime>
    {
        public override Anime AfterProcess(Anime model)
        {
            throw new NotImplementedException();
        }

        public override Anime LoadModel()
        {
            throw new NotImplementedException();
        }
    }
}
