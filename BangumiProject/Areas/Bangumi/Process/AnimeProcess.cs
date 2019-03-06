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

    }
}
