using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Files.Models
{
    public class FilePhoto
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 相册名字
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 相册描述
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 相册内的图片
        /// </summary>
        public ICollection<FileImages> Images { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
