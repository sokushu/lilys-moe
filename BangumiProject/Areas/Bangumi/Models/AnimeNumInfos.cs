using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Models
{
    public class AnimeNumInfos
    {
        [Key]
        public int ID { get; set; }
        //==================================
        //以下由爬虫进行处理
        //==================================
        /// <summary>
        /// 以AV号显示的，这一集的AV号
        /// 例如 av33634246
        /// </summary>
        public int Aid { get; set; }
        /// <summary>
        /// 不知道是做什么的ID
        /// </summary>
        public int Cid { get; set; }
        /// <summary>
        /// 这一集的封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 这一集的ID
        /// 可以通过这个找到播放页面
        /// 例如 ID 250498
        /// m.bilibili.com/bangumi/play/ep250498
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 这一集的标题
        /// </summary>
        public string Long_title { get; set; }
        /// <summary>
        /// 这一集的播放数量
        /// </summary>
        public int Stat_play { get; set; }
        /// <summary>
        /// 这一集动画的B站链接
        /// </summary>
        public string Share_url { get; set; }
        /// <summary>
        /// 这一集的短标题（第几集？）
        /// 实际上这是集数(也会有像 sp 特别篇 这样的值)
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 动画的Season_id
        /// </summary>
        public int Season_id { get; set; }
        /// <summary>
        /// 动画集数
        /// </summary>
        public int AnimeNum { get; set; }
        //==================================
        //以上由爬虫进行处理
        //==================================

        /// <summary>
        /// 对应的本站的动画
        /// </summary>
        public int Anime { get; set; }
        /// <summary>
        /// 我给他上传的图片（尽量不要用B站的图）
        /// </summary>
        public string PIC { get; set; }
        /// <summary>
        /// 这一集动画的评分
        /// </summary>
        public double Score { get; set; }
    }
}
