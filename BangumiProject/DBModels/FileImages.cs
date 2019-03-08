using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    public class FileImages
    {
        /// <summary>
        /// 一张图片的ID
        /// </summary>
        [Key]
        public string ImageID { get; set; }

        /// <summary>
        /// 图片的名字
        /// </summary>
        [Required]
        public string ImageName { get; set; }

        /// <summary>
        /// 图片所在的路径
        /// </summary>
        [Required]
        public string ImagePath { get; set; }
        /// <summary>
        /// 静态化路径，URL路径
        /// </summary>
        public string StaticPath { get; set; }
        /// <summary>
        /// 用于表示图片的格式
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 上传文件的用户
        /// </summary>
        public User UpLoadUsers { get; set; }
        /// <summary>
        /// 图片在哪一个相册里
        /// </summary>
        public FilePhoto Photos { get; set; }
        /// <summary>
        /// 有读取权限的用户
        /// </summary>
        public string ReadUsers { get; set; }
        /// <summary>
        /// 图片的上传时间
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
