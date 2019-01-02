using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Model
{
    public class _LogInfo
    {
        [Key]
        public int INFOID { get; set; }
        public string Info { get; set; }
        public DateTime Time { get; set; }
    }
}
