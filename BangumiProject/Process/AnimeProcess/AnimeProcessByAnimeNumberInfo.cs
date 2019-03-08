using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Areas.Bangumi.Process;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.AnimeProcess
{
    public class AnimeProcessByAnimeNumberInfo : IProcess2Parm<AnimeNumberInfo, Anime>
    {
        public AnimeNumberInfo Process(Anime anime)
        {
            int AllNumber = anime.AnimeNum;
            int AllPage = 1;
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
            Span<string> PageName = new Span<string>(new string[AllPage]);
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
