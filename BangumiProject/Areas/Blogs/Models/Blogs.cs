using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Areas.Blogs.Models
{
    public class Blogs
    {
        [Key]
        [Required]
        public int BlogID { get; set; }
        /// <summary>
        /// 关联的动画ID
        /// </summary>
        [Required]
        public int AnimeID { get; set; }

        public string AnimeName { get; set; }
        public string AnimePic { get; set; }
        public string AnimeInfo { get; set; }
        /// <summary>
        /// 这一个博客是那个用户写的
        /// </summary>
        public User UpLoadUser { get; set; }

        /// <summary>
        /// 博客的正文
        /// </summary>
        [Required]
        public string BlogStr { get; set; }

        public ICollection<BlogsTags> TagIDs { get; set; }

        public ICollection<BlogsComm> Comments { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
