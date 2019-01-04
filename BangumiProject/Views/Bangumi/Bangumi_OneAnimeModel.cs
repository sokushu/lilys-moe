using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoeUtilsBox.String;
using BangumiProject.Areas.Bangumi.Models;
using Memo = BangumiProject.Areas.Bangumi.Models.AnimeMemo;

namespace BangumiProject.Views.Bangumi
{
    public class Bangumi_OneAnimeModel
    {
        public Anime Anime { get; set; }

        public string AnimePic => Anime.AnimePic.IfEmptyReturnNull() ?? Final.DefaultAnimePic;

        public int UserAnimeNumber { get; set; }

        public bool IsSub { get; set; }

        public bool IsSignIn { get; set; }
        public ICollection<Memo> Memos { get; set; }

        public bool IsShowEdit { get; set; }
        /// <summary>
        /// 动画显示多少页
        /// 1 - 10集， 11 - 20集等
        /// </summary>
        public int Page { get; set; }
    }
}
