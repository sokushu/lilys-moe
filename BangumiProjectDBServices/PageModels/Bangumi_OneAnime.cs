using BangumiProjectDBServices.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectDBServices.PageModels
{
    public class Bangumi_OneAnime
    {
        public Anime Anime { get; set; }

        public string AnimePic => Anime.AnimePic.IfEmptyReturnNull() ?? Final.DefaultAnimePic;

        public int UserAnimeNumber { get; set; }

        public string AnimeTime => Anime.AnimePlayTime.ToString("yyyy年MM月dd日");

        public bool IsSub { get; set; }

        public bool IsSignIn { get; set; }
        public ICollection<AnimeMemo> Memos { get; set; }

        public bool IsShowEdit { get; set; }
        /// <summary>
        /// 动画显示多少页
        /// 1 - 10集， 11 - 20集等
        /// </summary>
        //public AnimeNumberInfo Page { get; set; }
    }
}
