using BangumiProjectDBServices.Models;
using BangumiProjectDBServices.PageModels.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectDBServices.ParamsModels
{
    public class AnimeEdit : BaseModel
    {
        public Anime Anime { get; set; }

        public string AddTag { get; set; }
    }
}
