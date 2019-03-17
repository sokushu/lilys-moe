
using BangumiProjectDBServices.Models;
using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProjectProcess.AnimeProcess.AnimeProcessC
{
    public class AnimeProcessByAnimeNumber : IProcess<bool, Anime>
    {
        public AnimeProcessByAnimeNumber()
        {

        }

        public bool Process(Anime anime)
        {
            if (anime.IsEnd == false)
            {
                //获取数据库中的动画集数
                var AnimeNumber = anime.AnimeNum;
                //计算现在实际应该更新到哪里，与数据库中的数据做对比
                var StartDate = anime.AnimePlayTime;
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

                    anime.AnimeNum = num;
                }
                return true;
            }
            return false;
        }
    }
}
