using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Models
{
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
}
