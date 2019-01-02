using BangumiProject.Controllers;
using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoeUtilsBox.String;

namespace BangumiProject.Views.Home
{
    public class IndexModel
    {
        public ICollection<Anime> Animes { get; set; }

        public string AnimePic(Anime anime) => anime.AnimePic.IfEmptyReturnNull() ?? Final.DefaultAnimePic;

        public string AnimeInfo(Anime anime)
        {
            var str = anime.AnimeInfo;
            var n = 15;
            var End = str.Length >= n ? n : str.Length;
            return str == null ? string.Empty : End == n ? str.Substring(0, End).Insert(End, "......") : str.Substring(0, End);
        }

        public List<List<Anime>> WeekAnimes { get; set; }

        //测试用
        public List<Users> AllUsers { get; set; }

        //更新日记
        public List<List<string>> Log { get; set; }
    }
}
