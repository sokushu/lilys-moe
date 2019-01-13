using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Video.Models
{
    /// <summary>
    /// 写入数据库中
    /// </summary>
    public class VideoInfo
    {
        /// <summary>
        /// 视频的ID
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 视频的名字
        /// </summary>
        public string VideoName { get; set; }
        /// <summary>
        /// 视频的信息
        /// </summary>
        public string VInfo { get; set; }
        /// <summary>
        /// 视频的路径(Static)
        /// 可以直接读取的那种
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 创建的时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}
