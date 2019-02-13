using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BaseProject.Common.Models
{
    /// <summary>
    /// 其实就是专辑信息
    /// </summary>
    public class MusicAlbum
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int MIID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ICollection<Music> Musics { get; set; }
    }
}
