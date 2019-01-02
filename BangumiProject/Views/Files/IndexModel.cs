using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Views.Files
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
