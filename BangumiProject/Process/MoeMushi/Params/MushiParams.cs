using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Params
{
    /// <summary>
    /// BILIBILI API 整理
    /// 
    /// 新番时间表
    /// https://bangumi.bilibili.com/web_api/timeline_global
    /// 
    /// 动画集数信息
    /// https://api.bilibili.com/pgc/web/season/section?season_id=24588
    /// season_id   动画ID JSON字段：season_id
    /// 
    /// 动画索引（所有的动画）
    /// https://bangumi.bilibili.com/media/web_api/search/result?season_version=-1&area=-1&is_finish=-1&copyright=-1&season_status=-1&season_month=-1&pub_date=-1&style_id=-1&order=3&st=1&sort=0&page=1&season_type=1&pagesize=20
    /// 
    /// 
    /// 
    /// 
    /// 一些字段：
    /// media_id ： 这个字段是查询动画的
    /// 例如：https://www.bilibili.com/bangumi/media/md135652/
    /// 这个135652就是media_id
    /// 
    /// season_id：似乎可以查询动画的集数信息
    /// 
    /// </summary>
    public class MushiParams
    {
        /// <summary>
        /// 开始的URL
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 方法GET？POST？
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 传输的信息（POST）
        /// </summary>
        public string PostData { get; set; }
        /// <summary>
        /// 选择的一个节点
        /// </summary>
        public string SelectNode { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public MoeMushiContext _MoeMushiDB { get; set; }
    }
}
