using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class PageModelLoader : IPageModelLoader
    {
        protected ICollection<object> Models { get; set; }

        public PageModelLoader()
        {
            Models = new List<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetModelLoader<Model>(IModelLoader<Model> modelLoader)
        {
            Model model = modelLoader.BuildModel();
            Models.Add(model);
        }

        /// <summary>
        /// 加载Model处理程序，返回处理后的Model
        /// 
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public A BuildPageData<A>() where A : class
        {
            var objs = Models.ToArray();
            switch (objs.Length)
            {
                case 0:
                    return default(A);
                case 1:
                    return Tuple.Create(objs[0]) as A;
                case 2:
                    return Tuple.Create(objs[0], objs[1]) as A;
                case 3:
                    return Tuple.Create(objs[0], objs[1], objs[2]) as A;
                case 4:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3]) as A;
                case 5:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4]) as A;
                case 6:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4], objs[5]) as A;
                case 7:
                    return Tuple.Create(objs[0], objs[1], objs[2], objs[3], objs[4], objs[5], objs[6]) as A;
                default:
                    return default(A);
            }
        }
    }
}
