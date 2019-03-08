using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    public class FilePhoto
    {
        /// <summary>
        /// 这条数据的ID
        /// </summary>
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
        /// <summary>
        /// 这条数据的创建时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
