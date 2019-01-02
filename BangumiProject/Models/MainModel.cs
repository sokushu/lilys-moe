using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BangumiProject.Models
{
    /// <summary>
    /// 用户
    /// 用户名是邮箱
    /// </summary>
    public class Users : IdentityUser, ISerializable
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

        public ICollection<Images> Images { get; set; }
        public ICollection<UserAnimeInfo> UserAnimeInfos { get; set; }
        public ICollection<Blog> Blogs { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<AnimeSouceComm> AnimeSouceComms { get; set; }
        public ICollection<AnimeComm> AnimeComms { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            ((ISerializable)Time).GetObjectData(info, context);
        }
    }

    public class UserRole : IdentityUserRole<string>
    {
        /// <summary>
        /// 经验值
        /// </summary>
        public int KenGenChi { get; set; }
        /// <summary>
        /// 记录最后登录时间，用于改经验值
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    public class UserAnimeInfo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 用户订阅的动画
        /// </summary>
        public Anime SubAnime { get; set; }
        /// <summary>
        /// 现在看到那里了
        /// </summary>
        public int NowAnimeNum { get; set; }
        /// <summary>
        /// 订阅的用户
        /// </summary>
        public Users Users { get; set; }
        /// <summary>
        /// 动画分集记录的Memo
        /// </summary>
        public ICollection<Memo> Memos { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    public class Memo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 记录的是那一集的Memo
        /// </summary>
        public int NowAnimeNum { get; set; }

        /// <summary>
        /// Memo内容
        /// </summary>
        public string MemoStr { get; set; }

        public UserAnimeInfo UserAnimeInfo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 相册
    /// </summary>
    public class Photos
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 相册名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 相册描述
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 相册内的图片
        /// </summary>
        public ICollection<Images> Images { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 图片类
    /// </summary>
    public class Images
    {
        /// <summary>
        /// 一张图片的ID
        /// </summary>
        [Key]
        public string ImageID { get; set; }

        /// <summary>
        /// 图片的名字
        /// </summary>
        [Required]
        public string ImageName { get; set; }

        /// <summary>
        /// 图片所在的路径
        /// </summary>
        [Required]
        public string ImagePath { get; set; }
        /// <summary>
        /// 静态化路径，URL路径
        /// </summary>
        public string StaticPath { get; set; }
        /// <summary>
        /// 用于表示图片的格式
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 上传文件的用户
        /// </summary>
        public Users UpLoadUsers { get; set; }
        /// <summary>
        /// 图片在哪一个相册里
        /// </summary>
        public Photos Photos { get; set; }
        /// <summary>
        /// 有读取权限的用户
        /// </summary>
        public string ReadUsers { get; set; }
        /// <summary>
        /// 图片的上传时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 用户写的博客类
    /// </summary>
    public class Blog
    {
        [Key]
        [Required]
        public int BlogID { get; set; }
        /// <summary>
        /// 关联的动画ID
        /// </summary>
        [Required]
        public int AnimeID { get; set; }

        public string AnimeName { get; set; }
        public string AnimePic { get; set; }
        public string AnimeInfo { get; set; }
        /// <summary>
        /// 这一个博客是那个用户写的
        /// </summary>
        public Users UpLoadUser { get; set; }

        /// <summary>
        /// 博客的正文
        /// </summary>
        [Required]
        public string BlogStr { get; set; }

        public ICollection<BlogTags> TagIDs { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 博客的标签
    /// </summary>
    public class BlogTags
    {
        [Key]
        public int BolgTagID { get; set; }
        public string BlogTagString { get; set; }

        public Blog Blogs { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    /// <summary>
    /// 博客的留言
    /// </summary>
    public class Comment
    {
        [Key]
        public int CommID { get; set; }

        public Users Users { get; set; }

        public Blog Blogs { get; set; }

        public string CommStr { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }


    /// <summary>
    /// 这里其实并不只有动画
    /// 包括动画，漫画，电影，电视剧，音乐，小说等，全部都包含在内
    /// </summary>
    public class Anime
    {
        /// <summary>
        /// 动画ID
        /// </summary>
        [Key]
        public int AnimeID { get; set; }
        /// <summary>
        /// 动画的名字
        /// </summary>
        [Required(ErrorMessage = "请填写动画名称")]
        public string AnimeName { get; set; }
        /// <summary>
        /// 动画的封面图片
        /// </summary>
        public string AnimePic { get; set; }
        /// <summary>
        /// 动画的集数
        /// </summary>
        [Required(ErrorMessage = "请填写动画集数")]
        public int AnimeNum { get; set; }
        /// <summary>
        /// 动画类型：
        /// 1 ： TV动画
        /// 2 ： OVA动画
        /// …………具体请看代码
        /// </summary>
        public AnimeType AnimeType { get; set; }
        /// <summary>
        /// 动画的一些信息，简介什么的
        /// </summary>
        public string AnimeInfo { get; set; }
        /// <summary>
        /// 动画是否已经完结
        /// </summary>
        public bool IsEnd { get; set; }
        /// <summary>
        /// 动画播放时间
        /// </summary>
        [DataType(DataType.DateTime, ErrorMessage = "请输入正确的时间例如：2018-01-01")]
        [Display(Name = "动画的播放时间")]
        public DateTime AnimePlayTime { get; set; }
        /// <summary>
        /// 动画数据创建的时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
        /// <summary>
        /// 关于动画的评论
        /// </summary>
        public ICollection<AnimeComm> AnimeComms { get; set; }
        /// <summary>
        /// 动画所属的标签
        /// </summary>
        public ICollection<AnimeTag> Tags { get; set; }
        /// <summary>
        /// 动画的播放元
        /// </summary>
        public ICollection<AnimeSouce> Souce { get; set; }
        /// <summary>
        /// 可以算出动画的订阅量
        /// </summary>
        public ICollection<UserAnimeInfo> UserAnimeInfos { get; set; }
    }

    public enum AnimeType
    {
        [Display(Name = "TV动画")]
        TVAnime,
        [Display(Name = "OVA动画")]
        OVA,
        [Display(Name = "剧场版动画")]
        MovieAnime,
        [Display(Name = "其他")]
        Other
    }

    /// <summary>
    /// 播放源，
    /// 例如ACfun，Bilibili，或是某下载网站
    /// </summary>
    public class AnimeSouce
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 观看源或是下载源的名字
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public string URL { get; set; }
        /// <summary>
        /// 网站的Logo之类的
        /// </summary>
        public string Pic { get; set; }
        /// <summary>
        /// 关于该网站的信息
        /// </summary>
        public string Info { get; set; }

        public Anime Anime { get; set; }
        /// <summary>
        /// 动画播放元的评论
        /// </summary>
        public ICollection<AnimeSouceComm> AnimeSouceComms { get; set; }
        /// <summary>
        /// 建立的时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    public class AnimeTag
    {
        [Key]
        public int TagID { get; set; }
        public string TagName { get; set; }

        public Anime Anime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    public class AnimeSouceComm
    {
        [Key]
        public int CommID { get; set; }
        [Required]
        public string CommStr { set; get; }
        public AnimeSouce AnimeSouce { get; set; }
        public Users Users { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    public class AnimeComm
    {
        [Key]
        public int CommID { get; set; }
        [Required]
        public string CommStr { set; get; }
        [Required]
        public Users Users { get; set; }
        public Anime Anime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }

    
}
