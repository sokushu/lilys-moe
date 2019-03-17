using BangumiProject.DBModels;
using MoeUtilsBox.String;
using MoeUtilsBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Controllers;
using BangumiProject.Areas.Bangumi.Process;

namespace BangumiProject.Areas.Bangumi.Views.Bangumi.Model
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
        public AnimeNumberInfo Page { get; set; }
    }
}
