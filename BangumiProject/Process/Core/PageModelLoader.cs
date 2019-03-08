using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BangumiProject.Process.Interface;

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
        /// <typeparam name="Model"></typeparam>
        /// <param name="model"></param>
        public void SetModel<Model>(Model model)
        {
            Models.Add(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="modelStream"></param>
        public void SetModelStream<Model>(ModelStream<Model> modelStream)
        {
            Model model = modelStream.Build();
            Models.Add(model);
        }

        public Tuple<T, T1> BuildPageData<T, T1>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1]);
        }

        public Tuple<T, T1, T2> BuildPageData<T, T1, T2>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2]);
        }

        public Tuple<T, T1, T2, T3> BuildPageData<T, T1, T2, T3>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3]);
        }

        public Tuple<T, T1, T2, T3, T4> BuildPageData<T, T1, T2, T3, T4>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4]);
        }

        public Tuple<T, T1, T2, T3, T4, T5> BuildPageData<T, T1, T2, T3, T4, T5>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4], (T5)arr[5]);
        }

        public Tuple<T, T1, T2, T3, T4, T5, T6> BuildPageData<T, T1, T2, T3, T4, T5, T6>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4], (T5)arr[5], (T6)arr[6]);
        }

        public Tuple<T> BuildPageData<T>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0]);
        }
    }
}
