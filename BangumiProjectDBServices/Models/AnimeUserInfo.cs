using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectDBServices.Models
{
    public class AnimeUserInfo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 用户订阅的动画
        /// </summary>
        public Anime SubAnime { get; set; }
        /// <summary>
        /// 现在看到那里了
        /// </summary>
        public int NowAnimeNum { get; set; }
        /// <summary>
        /// 订阅的用户
        /// </summary>
        public User Users { get; set; }
        /// <summary>
        /// 动画分集记录的Memo
        /// </summary>
        public ICollection<AnimeMemo> Memos { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
