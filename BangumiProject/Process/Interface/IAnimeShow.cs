using BangumiProject.Process.PageModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    /// <summary>
    /// 这是一个用于构建相应动画信息的接口
    /// </summary>
    public interface IAnimeShow
    {
        /// <summary>
        /// 构建动画的信息类，用于页面上的动画显示
        /// </summary>
        /// <returns></returns>
        AnimeInfoModel BuildAnimeInfo();
    }
}
