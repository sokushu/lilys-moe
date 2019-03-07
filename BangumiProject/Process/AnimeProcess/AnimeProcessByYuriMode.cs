using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.AnimeProcess
{
    public class AnimeProcessByYuriMode : IAnimeProcess
    {
        private bool Mode { get; set; }
        public AnimeProcessByYuriMode(bool YuriMode)
        {
            Mode = YuriMode;
        }

        public void Process<T>(ref T Model) where T : class
        {
            if (Mode)
            {

            }
        }

        public Anime Process(Anime t)
        {
            throw new NotImplementedException();
        }
    }
}
