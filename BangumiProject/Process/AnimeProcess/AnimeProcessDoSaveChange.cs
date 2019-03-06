using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Interface;
using BangumiProject.Services.DBServices.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.AnimeProcess
{
    public class AnimeProcessDoSaveChange : IProcessDoSome
    {
        private IDBCore dBCore { get; set; }
        private string SaveKey { get; set; }
        private Anime anime { get; set; }

        public AnimeProcessDoSaveChange(
            IDBCore dBCore,
            string SaveKey,
            Anime anime
            )
        {
            this.dBCore = dBCore;
            this.SaveKey = SaveKey;
            this.anime = anime;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Process()
        {
            //需要更新动画信息
            dBCore.Save_Updata(SaveKey, anime).Commit();
        }
    }
}
