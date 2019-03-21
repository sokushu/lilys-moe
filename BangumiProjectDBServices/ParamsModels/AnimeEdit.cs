using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectDBServices.ParamsModels
{
    public class AnimeEdit
    {
        public Anime Anime { get; set; }

        public string AddTag { get; set; }
    }
}
