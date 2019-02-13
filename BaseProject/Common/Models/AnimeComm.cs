﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BaseProject.Common.Models
{
    public class AnimeComm
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int CommID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string CommStr { set; get; }
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public User Users { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Anime Anime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
