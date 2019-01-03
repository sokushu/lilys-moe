using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Areas.Blogs.Models
{
    public class BlogsComm
    {
        [Key]
        public int CommID { get; set; }

        public User Users { get; set; }

        public Blogs Blogs { get; set; }

        public string CommStr { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
