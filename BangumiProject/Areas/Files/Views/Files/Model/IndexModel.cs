using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Images = BangumiProject.Areas.Files.Models.FileImages;

namespace BangumiProject.Areas.Files.Views.Files.Model
{
    public class IndexModel
    {

        public string Tilte { get; set; }

        public string Message { get; set; }

        /// <summary>
        /// 图片列表
        /// </summary>
        public List<Images> Pic { get; set; }

    }
}
