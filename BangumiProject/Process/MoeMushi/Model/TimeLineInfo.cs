using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Model
{
    public class TimeLineInfo
    {
        [Key]
        public int DBID { get; set; }
        /// <summary>
        /// 动画标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// 大封面
        /// </summary>
        public string Cover { get; set; }
        /// <summary>
        /// 小封面
        /// </summary>
        public string Square_cover { get; set; }
        /// <summary>
        /// 播放时间（例如08：00）
        /// </summary>
        public string Pub_time { get; set; }
        /// <summary>
        /// 这一集的ID（可以直接打开播放页面）
        /// </summary>
        public int Ep_id { get; set; }
        /// <summary>
        /// 动画剧集信息的ID
        /// </summary>
        public int Season_id { get; set; }
        /// <summary>
        /// 显示的标题？（动画集数例如第1集之类的）
        /// </summary>
        public string Pub_index { get; set; }
        /// <summary>
        /// 追番人数？
        /// </summary>
        public int Favorites { get; set; }
        /// <summary>
        /// 是否已经更新（0：没有，1：更新了）
        /// </summary>
        public bool Is_published { get; set; }
        /// <summary>
        /// 是否停止更新
        /// </summary>
        public bool Delay { get; set; }
        /// <summary>
        /// 停止更新的集数
        /// </summary>
        public string Delay_index { get; set; }
        /// <summary>
        /// 停止更新的信息（例如：本周停更）
        /// </summary>
        public string Delay_reason { get; set; }
    }
}
