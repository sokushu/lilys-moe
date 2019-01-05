using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Services;
using MoeUtilsBox.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
{
    public static class BangumiProcess
    {
        /// <summary>
        /// 计算动画最新集数（动画集数机制变更）
        /// 
        /// 动画最新集数，通过开播日期来计算，例如开播日期是12月6日星期四，
        /// 那么，在下个星期四也就是13号，会自动更新第二集，直到设置状态为完结动画状态，
        /// 如果遇到动画事故等需要停播的情况，会有对应的设置的
        /// </summary>
        /// <param name="anime">要处理的动画</param>
        public static bool AnimeNumUpdata(this Anime anime)
        {
            if (anime.IsEnd == false)//这里只选取没有完结的动画做修改
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
                    //动画被修改
                    return true;
                }
            }
            //动画没有被修改
            return false;
        }

        /// <summary>
        /// 得到动画的分集信息
        /// </summary>
        public static void GetAnimeNumInfo(int AnimeID, ICommDB _DB)
        {

        }

        /// <summary>
        /// 12,4,7,22,32
        /// 2  1 1 3  4
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public static AnimeNumberInfo AnimeNumPage(this Anime anime)
        {
            int AllNumber = anime.AnimeNum;
            int AllPage = 1;
            Span<string> PageName = new Span<string>();
            int[] loop = new int[] { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
            foreach (int item in loop)
            {
                if (AllNumber > item)
                {
                    AllPage++;
                }
                else
                {
                    break;
                }
            }
            for (int i = 0; i < AllPage; i++)
            {
                int num = 0;
                int PageEnd = (num = loop[i]) > AllNumber ? AllNumber : num;
                if (i == 0)
                {
                    PageName[i] = $"第1集 - 第{PageEnd}集";
                }
                else
                {
                    PageName[i] = $"第{num - 9}集 - 第{PageEnd}集";
                }
                
            }
            return new AnimeNumberInfo
            {
                AllPage = AllPage,
                PageName = PageName.ToArray()
        };
        }
    }

    
}
