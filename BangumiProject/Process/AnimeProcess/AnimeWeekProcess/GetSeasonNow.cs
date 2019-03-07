using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.AnimeProcess.AnimeWeekProcess
{
    public static class GetSeasonNow
    {
        /// <summary>
        /// 得到现在的季度
        /// </summary>
        /// <returns></returns>
        public static int GetSeason()
        {
            int SeasonNow = 0;
            switch (DateTime.Now.Month)
            {
                case 1:
                case 2:
                case 3:
                    SeasonNow = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    SeasonNow = 2;
                    break;
                case 7:
                case 8:
                case 9:
                    SeasonNow = 3;
                    break;
                case 10:
                case 11:
                case 12:
                    SeasonNow = 4;
                    break;
                default:
                    SeasonNow = 0;
                    break;
            }
            return SeasonNow;
        }
    }
}
