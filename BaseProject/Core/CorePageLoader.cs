using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseProject.Core
{
    public class CorePageLoader : IPageModelLoader
    {
        protected ICollection<object> Models { get; set; }

        protected string Page { get; set; }

        public CorePageLoader()
        {
            Models = new List<object>();
        }

        public void SetSwitchPage(IPage page)
        {
            Page = page.Build();
        }

        public string GetPage()
        {
            return Page;
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
        public void SetModelStream<Model>(IModelStream<Model> modelStream) where Model : new()
        {
            Model model = modelStream.Build();
            Models.Add(model);
        }

        public Tuple<T, T1> Build<T, T1>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1]);
        }

        public Tuple<T, T1, T2> Build<T, T1, T2>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2]);
        }

        public Tuple<T, T1, T2, T3> Build<T, T1, T2, T3>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3]);
        }

        public Tuple<T, T1, T2, T3, T4> Build<T, T1, T2, T3, T4>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4]);
        }

        public Tuple<T, T1, T2, T3, T4, T5> Build<T, T1, T2, T3, T4, T5>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4], (T5)arr[5]);
        }

        public Tuple<T, T1, T2, T3, T4, T5, T6> Build<T, T1, T2, T3, T4, T5, T6>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0], (T1)arr[1], (T2)arr[2], (T3)arr[3], (T4)arr[4], (T5)arr[5], (T6)arr[6]);
        }

        public Tuple<T> Build<T>()
        {
            var arr = Models.ToArray();
            return Tuple.Create((T)arr[0]);
        }
    }
}
