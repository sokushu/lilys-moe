using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Views.PlaySouce.Model
{
    public class CreatePlaySouce
    {
        /// <summary>
        /// 网站名
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 网站URL
        /// </summary>
        public string SiteURL { get; set; }
        /// <summary>
        /// 网站的图片
        /// </summary>
        public string SitePic { get; set; }
        /// <summary>
        /// 信息
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 上传图片
        /// </summary>
        public IFormFile Pic { get; set; }
    }
}
