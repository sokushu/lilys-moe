using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace BangumiProjectDBServices.Models
{
    /// <summary>
    /// 用户
    /// 用户名是邮箱
    /// </summary>
    public class User : IdentityUser
    {
        /// <summary>
        /// 用户对外显示的昵称
        /// </summary>
        [PersonalData]
        public string Name { get; set; }
        /// <summary>
        /// 用户的创建日期
        /// </summary>
        [PersonalData]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
        /// <summary>
        /// 用户的自定义URL
        /// </summary>
        [PersonalData]
        [DataType(DataType.Url)]
        public string URL { get; set; }
        /// <summary>
        /// 用户的自定义头像
        /// </summary>
        [PersonalData]
        [DataType(DataType.ImageUrl)]
        public string UserPic { get; set; }
        /// <summary>
        /// 用户的个人主页背景
        /// </summary>
        [DataType(DataType.ImageUrl)]
        public string UserBackPic { get; set; }

        public ICollection<FileImages> Images { get; set; }
        public ICollection<AnimeUserInfo> UserAnimeInfos { get; set; }
        public ICollection<AnimeSouceComm> AnimeSouceComms { get; set; }
        public ICollection<AnimeComm> AnimeComms { get; set; }
    }
}
