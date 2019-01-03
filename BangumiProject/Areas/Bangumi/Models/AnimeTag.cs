using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Models
{
    public class AnimeTag
    {
        [Key]
        public int TagID { get; set; }
        public string TagName { get; set; }

        public Anime Anime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
