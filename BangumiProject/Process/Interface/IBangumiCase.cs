using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    /// <summary>
    /// 要创建新的过滤器：
    /// 
    /// 请确保AnimeFilter方法不会返回Null
    /// 
    /// </summary>
    public interface IBangumiCase
    {
        /// <summary>
        /// 对动画进行过滤
        /// </summary>
        /// <param name="animes">要过滤的动画</param>
        /// <returns>返回过滤完成的动画</returns>
        List<Anime> AnimeFilter(List<Anime> animes);
    }
}
