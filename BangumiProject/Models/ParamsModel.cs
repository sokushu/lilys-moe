using BangumiProject.Areas.Bangumi.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Models
{
    public class ParamsModel
    {
    }

    public class ImageSetting
    {
        public string ImageID { get; set; }
        public string Name { get; set; }
        public string ReadUser { get; set; }
        public bool IsDel { get; set; }
    }

    public class UserAnime
    {
        public string AnimeName { get; set; }
        public string AnimePic { get; set; }
        public int AnimeID { get; set; }
        public int NowNum { get; set; }
    }

    public class UpLoadFiles
    {
        public IFormFile Files { get; set; }
        public bool IsStaticFile { get; set; }
    }
    public class AnimeSetting
    {
        /// <summary>
        /// 动画
        /// </summary>
        public Anime Anime { get; set; }
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
        /// 具体集数的停播（或者说消失不见）
        /// 例如3，6集，那么这部动画就不会有3集和6集
        /// </summary>
        public int[] StopNums { get; set; }
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
        public DateTime[] StopLongStartPlay { get; set; }
        /// <summary>
        /// 特殊的复播，收录到DVD或BD中，或是后来以OVA的方式发售
        /// </summary>
        public bool StopLongStartPlayDVD { get; set; }
    }
}
