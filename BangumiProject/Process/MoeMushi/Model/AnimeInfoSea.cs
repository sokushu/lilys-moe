using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Model
{
    public class AnimeInfoSea
    {
        [Key]
        public int DBID { get; set; }
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
    }
}
