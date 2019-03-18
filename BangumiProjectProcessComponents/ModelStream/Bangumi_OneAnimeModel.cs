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
        public override void BuildRulu()
        {
            
        }

        public override Bangumi_OneAnime Build()
        {
            Bangumi_OneAnime ReturnBangumi_OneAnime = new Bangumi_OneAnime();
            return ReturnBangumi_OneAnime;
        }
    }
}
