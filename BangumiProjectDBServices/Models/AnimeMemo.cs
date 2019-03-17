using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectDBServices.Models
{
    public class AnimeMemo
    {
        [Key]
        public int ID { get; set; }
        /// <summary>
        /// 记录的是那一集的Memo
        /// </summary>
        public int NowAnimeNum { get; set; }

        /// <summary>
        /// Memo内容
        /// </summary>
        public string MemoStr { get; set; }

        public AnimeUserInfo UserAnimeInfo { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }
    }
}
