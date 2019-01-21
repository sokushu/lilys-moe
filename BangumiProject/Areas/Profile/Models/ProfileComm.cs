using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Profile.Models
{
    public class ProfileComm
    {
        [Key]
        public int Pid { get; set; }
        public string UserID { get; set; }
        public string UserPic { get; set; }
        public string UserComm { get; set; }
        public DateTime Time { get; set; }
    }
}
