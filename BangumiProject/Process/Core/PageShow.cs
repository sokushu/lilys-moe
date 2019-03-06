using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public abstract class PageShow
    {
        /// <summary>
        /// 创建一个新的页面Model
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public Model Build<Model>(Action<Model> action) where Model : new()
        {
            Model model = new Model();
            action.Invoke(model);
            return model;
        }
    }
}
