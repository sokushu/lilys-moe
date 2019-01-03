using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Blogs.Models
{
    public class BlogsTags
    {
        [Key]
        public int BolgTagID { get; set; }
        public string BlogTagString { get; set; }

        public Blogs Blogs { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
