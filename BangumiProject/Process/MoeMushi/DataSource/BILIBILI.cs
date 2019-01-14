using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.DataSource
{
    public static class BILIBILI
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string BILIBILI_Timeline_Global()
        {
            return "https://bangumi.bilibili.com/web_api/timeline_global";
        }

        /// <summary>
        /// 
        /// 默认是《工作细胞》的剧集信息（用于测试）
        /// </summary>
        /// <param name="season_id"></param>
        /// <returns></returns>
        public static string BILIBILI_AnimeNumInfo(int season_id = 24588)
        {
            return $"https://api.bilibili.com/pgc/web/season/section?season_id={season_id}";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="season_version"></param>
        /// <param name="area"></param>
        /// <param name="is_finish"></param>
        /// <param name="copyright"></param>
        /// <param name="season_status"></param>
        /// <param name="season_month"></param>
        /// <param name="pub_date"></param>
        /// <param name="style_id"></param>
        /// <param name="order"></param>
        /// <param name="st"></param>
        /// <param name="sort"></param>
        /// <param name="page"></param>
        /// <param name="season_type"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public static string BILIBILI_BangumiFilter(
            int season_version = -1, int area = -1, int is_finish = -1, int copyright = -1, int season_status = -1,
            int season_month = -1, int pub_date = -1, int style_id = -1, int order = 3, int st = 1, int sort = 0,
            int page = 1, int season_type = 1, int pagesize = 20)
        {
            return $"https://bangumi.bilibili.com/media/web_api/search/result?season_version={season_version}&area={area}&is_finish={is_finish}&copyright={copyright}&season_status={season_status}&season_month={season_month}&pub_date={pub_date}&style_id={style_id}&order={order}&st={st}&sort={sort}&page={page}&season_type={season_type}&pagesize={pagesize}";
        }
    }
}
