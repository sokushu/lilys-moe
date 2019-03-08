using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.DBModels
{
    public class AnimeTag
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int TagID { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TagName { get; set; }
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
