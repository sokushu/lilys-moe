using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BangumiProject.DBModels
{
    public class Music
    {
        /// <summary>
        /// ID
        /// </summary>
        [Key]
        public int MID { get; set; }

        /// <summary>
        /// 这首歌的名字
        /// </summary>
        public string MusicName { get; set; }

        /// <summary>
        /// 这首歌的一些信息
        /// </summary>
        public string Info { get; set; }

        /// <summary>
        /// 时长
        /// </summary>
        public int Time { get; set; }

        /// <summary>
        /// 这条数据的创建日期
        /// </summary>
        public DateTime DateTime { get; set; }
    }
}
