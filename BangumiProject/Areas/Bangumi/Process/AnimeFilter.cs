
using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
{
    /// <summary>
    /// 动画过滤管理器。
    /// 请结合IBangumiCase接口使用
    /// </summary>
    public class AnimeFilter
    {
        /// <summary>
        /// 要执行的所有过滤类
        /// </summary>
        private ICollection<IAnimeFilter> BangumiCase { get; set; }
        /// <summary>
        /// 初始化
        /// </summary>
        public AnimeFilter()
        {
            BangumiCase = new List<IAnimeFilter>();
        }
        /// <summary>
        /// 添加过滤处理
        /// </summary>
        /// <param name="bangumiCase">自定义的过滤处理</param>
        public void SetAnimeFilter(IAnimeFilter bangumiCase)
        {
            BangumiCase.Add(bangumiCase);
        }
        /// <summary>
        /// 返回最终的结果集
        /// </summary>
        /// <param name="InputAnime">输入的动画集</param>
        /// <returns>返回经过多次过滤后的动画数据</returns>
        public List<Anime> GetAnimeFilter(List<Anime> InputAnime)
        {
            if (InputAnime == null)
                return new List<Anime>();
            List<Anime> animes = InputAnime;
            foreach (IAnimeFilter item in BangumiCase)
            {
                animes = item.AnimeFilter(animes);
                //都已经没了，不需要继续筛选了
                if (animes.Count == 0)
                    break;
            }
            return animes;
        }
    }
}
