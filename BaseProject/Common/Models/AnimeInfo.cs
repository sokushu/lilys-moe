using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Common.Models
{
    public class AnimeNumInfo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 动画
        /// </summary>
        [Required]
        public int AnimeID { get; set; }
        /// <summary>
        /// 动画的集数
        /// </summary>
        public int AnimeNum { get; set; }
        /// <summary>
        /// 这一集显示的标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 这一集的播放地址
        /// </summary>
        public string PlayURL { get; set; }
        /// <summary>
        /// 这一集的信息
        /// </summary>
        public string AnimeNumbInfo { get; set; }
        /// <summary>
        /// 这一集正常播放时间
        /// </summary>
        public DateTime PlayTime { get; set; }
        /// <summary>
        /// 是否停播
        /// </summary>
        public bool IsStop { get; set; }
        /// <summary>
        /// 停播几集？
        /// 例如2集，两个星期后系统会自动集数+1
        /// </summary>
        public int StopNum { get; set; }
        /// <summary>
        /// 开始停播的日期
        /// </summary>
        public DateTime StopTime { get; set; }
        /// <summary>
        /// 长时间停播，恢复时间未知
        /// 基本等同于完结，不过还是未完结状态
        /// 长期停播会从时间表中删除
        /// </summary>
        public bool IsStopLong { get; set; }
        /// <summary>
        /// 长时间之后的复播
        /// 一般就是放送事故，长时间延迟后选定时间的复播,
        /// 这个时间可能是有规律的每个星期定时，也有可能是指定的某个，或某几个日期。
        /// </summary>
        public string StopLongStartPlay { get; set; }
        /// <summary>
        /// 特殊的复播，收录到DVD或BD中，或是后来以OVA的方式发售
        /// </summary>
        public bool StopLongStartPlayDVD { get; set; }
        /// <summary>
        /// 停播的理由
        /// </summary>
        public StopCause StopCause { get; set; }
    }

    public enum StopCause
    {
        /// <summary>
        /// 过年了
        /// </summary>
        [Display(Name = "电视台档期原因")]
        Year,
        /// <summary>
        /// 播放事故
        /// 例如，烂的没法看了，停播整顿
        /// </summary>
        [Display(Name = "播放事故")]
        NONONO,
        /// <summary>
        /// 地震，海啸，之类的
        /// </summary>
        [Display(Name = "灾难原因")]
        Calamity,
        [Display(Name = "其他原因")]
        Other
    }
}
