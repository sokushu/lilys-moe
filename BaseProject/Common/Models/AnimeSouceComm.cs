using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace BaseProject.Common.Models
{
    public class AnimeSouceComm
    {
        [Key]
        public int CommID { get; set; }
        [Required]
        public string CommStr { set; get; }
        public AnimeSouce AnimeSouce { get; set; }
        public User Users { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
