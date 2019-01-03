using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using User = BangumiProject.Areas.Users.Models.Users;

namespace BangumiProject.Areas.Bangumi.Models
{
    public class AnimeComm
    {
        [Key]
        public int CommID { get; set; }
        [Required]
        public string CommStr { set; get; }
        [Required]
        public User Users { get; set; }
        public Anime Anime { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
