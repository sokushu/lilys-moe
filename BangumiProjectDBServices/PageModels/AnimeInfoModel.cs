using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectDBServices.PageModels
{
    public class AnimeInfoModel
    {
        public Anime Anime { get; set; } = null;

        public string AnimePic => Anime.AnimePic.IfEmptyReturnNull() ?? Final.DefaultAnimePic;

        public int UserAnimeNumber { get; set; } = 0;

        public string AnimeTime => Anime.AnimePlayTime.ToString("yyyy年MM月dd日");

        public bool IsSub { get; set; } = false;

        public ICollection<AnimeMemo> Memos { get; set; } = new List<AnimeMemo>();

        public bool IsShowEdit { get; set; } = false;
    }
}
