using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProjectDBServices.Models
{
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
        public ICollection<AnimeUserInfo> UserAnimeInfos { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public AnimeMoreInfo AnimeMoreInfo { get; set; }
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
}
