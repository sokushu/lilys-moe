using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    /// <summary>
    /// 其实就是专辑信息
    /// </summary>
    public class MusicAlbum
    {
        /// <summary>
        /// 专辑的ID
        /// </summary>
        [Key]
        public int MIID { get; set; }

        /// <summary>
        /// 专辑的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 专辑封面
        /// </summary>
        public string Pic { get; set; }

        /// <summary>
        /// 专辑包含的歌曲
        /// </summary>
        public ICollection<Music> Musics { get; set; }
    }
}
