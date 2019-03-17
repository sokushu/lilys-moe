using BangumiProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.DBModels;
using User = BangumiProject.Areas.Users.Models.Users;
using MoeUtilsBox.String;

namespace BangumiProject.Areas.Profile.Views.Profile.Model
{
    public class Profile
    {
        public User Users { get; set; }

        public string UserPic => Users.UserPic.IfEmptyReturnNull() ?? Final.DefaultUserPic;

        public string UserName => Users.Name.IfEmptyReturnNull() ?? Users.UserName;

        public bool IsMe { get; set; }

        public List<List<AnimeUserInfo>> AnimeInfos { get; set; }
    }
}
