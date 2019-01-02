using BangumiProject.Controllers;
using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoeUtilsBox.String;

namespace BangumiProject.Views.Profile
{
    public class ProfileModel
    {
        public Users Users { get; set; }

        public string UserPic => Users.UserPic.IfEmptyReturnNull() ?? Final.DefaultUserPic;

        public string UserName => Users.Name.IfEmptyReturnNull() ?? Users.UserName;

        public bool IsMe { get; set; }

        public List<List<UserAnimeInfo>> AnimeInfos { get; set; }
    }
}
