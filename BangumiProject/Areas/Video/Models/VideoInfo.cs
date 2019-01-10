using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Video.Models
{
    public class VideoInfo
    {
        [Key]
        public int ID { get; set; }
        public string VideoName { get; set; }
        public string VInfo { get; set; }
        public string Path { get; set; }
    }
}
