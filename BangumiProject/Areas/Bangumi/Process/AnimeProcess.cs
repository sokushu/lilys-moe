using BangumiProject.Areas.Bangumi.Models;
using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
{
    public class AnimeProcess
    {
        private readonly List<IAnimeProcess> animeProcesses = new List<IAnimeProcess>();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public T BuildModel<T>(Action<T> action) where T : new()
        {
            T obj = new T();
            action.Invoke(obj);
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="animeProcess"></param>
        public void SetProcess(IAnimeProcess animeProcess)
        {
            animeProcesses.Add(animeProcess);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="anime"></param>
        /// <returns></returns>
        public bool Run(Anime anime)
        {
            HashSet<bool> vs = new HashSet<bool>();
            foreach (IAnimeProcess item in animeProcesses)
            {
                vs.Add(item.Process(ref anime));
            }
            return vs.Count == 1;
        }

    }
}
