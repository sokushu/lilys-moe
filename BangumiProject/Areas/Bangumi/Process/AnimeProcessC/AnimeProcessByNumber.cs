using BangumiProject.DBModels;
using BangumiProject.Process.Interface;
using BangumiProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process.AnimeProcessC
{
    public class AnimeProcessByNumber
    {
        private readonly List<AnimeNumInfo> AnimeNumInfo;
        private readonly Anime ProcessAnime;
        public AnimeProcessByNumber(List<AnimeNumInfo> AnimeNumInfo, ref Anime anime)
        {
            this.AnimeNumInfo = AnimeNumInfo.OrderBy(info => info.AnimeNum).ToList();
            this.ProcessAnime = anime;
        }
        /// <summary>
        /// 计算动画最新集数（动画集数机制变更）
        /// 
        /// 动画最新集数，通过开播日期来计算，例如开播日期是12月6日星期四，
        /// 那么，在下个星期四也就是13号，会自动更新第二集，直到设置状态为完结动画状态，
        /// 如果遇到动画事故等需要停播的情况，会有对应的设置的
        /// </summary>
        /// <param name="anime">要处理的动画</param>
        public bool Process()
        {
            if (ProcessAnime.IsEnd == false)//这里只选取没有完结的动画做修改
            {
                //获取数据库中的动画集数
                var AnimeNumber = ProcessAnime.AnimeNum;
                //计算现在实际应该更新到哪里，与数据库中的数据做对比
                var StartDate = ProcessAnime.AnimePlayTime;
                var Tsu = StartDate.AddDays((AnimeNumber) * 7);
                //遇到特殊情况的处理对策（例如，临时停播，放送事故造成的变更）
                bool Stop = false;
                if (DateTime.Compare(DateTime.Now, Tsu) >= 0)//如果今天的日期大于数据库储存的（集数的下一集的日期），那就开始计算吧
                {
                    int num = 0;
                    while (true)
                    {
                        if (DateTime.Compare(DateTime.Now, Tsu) >= 0)
                        {
                            if (!Stop)//如果不是停播的话，动画加一
                            {
                                AnimeNumber = AnimeNumber + 1;
                            }
                            Tsu = Tsu.AddDays(7);
                        }
                        else
                        {
                            num = AnimeNumber;
                            break;
                        }
                    }

                    ProcessAnime.AnimeNum = num;
                    //动画被修改
                    return true;
                }

                ////获取动画的集数
                //var AnimeNumber = ProcessAnime.AnimeNum;
                ////检查是否停播
                //CheckStop(ProcessAnime, AnimeNumInfo);
            }
            //动画没有被修改
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        /// <param name="animeNumInfo"></param>
        private void CheckStop(Anime anime, List<AnimeNumInfo> animeNumInfo)
        {
            var Stop = animeNumInfo.Where(info => info.IsStop).ToList();
        }
    }
}
