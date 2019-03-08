using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    /// <summary>
    /// 动画的更多的信息，
    /// 例如动画公司，监督，角色设计等
    /// </summary>
    public class AnimeMoreInfo
    {
        /// <summary>
        /// Key
        /// </summary>
        [Key]
        public int INFOID { get; set; }

        /// <summary>
        /// 对应的哪一部动画
        /// </summary>
        public Anime Anime { get; set; }

        /// <summary>
        /// 动画的片头曲
        /// </summary>
        public MusicAlbum OP { get; set; }

        /// <summary>
        /// 动画的片尾曲
        /// </summary>
        public MusicAlbum ED { get; set; }

        /// <summary>
        /// 其他的音乐专辑信息
        /// </summary>
        public ICollection<MusicAlbum> MusicAlbums { get; set; }
    }
}
