using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Models
{
    public class AnimeStop
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 动画
        /// </summary>
        public int AnimeID { get; set; }
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
        Year,
        /// <summary>
        /// 播放事故
        /// 例如，烂的没法看了，停播整顿
        /// </summary>
        NONONO,
        /// <summary>
        /// 地震，海啸，之类的
        /// </summary>
        Calamity,
    }
}
