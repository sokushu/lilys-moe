using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BaseProject.Test
{
    public abstract class CoreProcess : ISender
    {
        /// <summary>
        /// Send 方法保存的临时数据
        /// </summary>
        private object Item { get; set; }

        protected ICollection<object> Models { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        public CoreProcess()
        {
            Models = new List<object>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Model"></typeparam>
        /// <param name="modelStream"></param>
        public void SetModelStream<Model>(IModelStream<Model> modelStream)
        {
            Model model = modelStream.Build();
            Models.Add(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Return"></typeparam>
        /// <typeparam name="Input"></typeparam>
        /// <param name="process"></param>
        /// <returns></returns>
        public Return SetProcess<Return, Input>(IProcess<Return, Input> process)
        {
            try
            {
                Input input = (Input)Item;
                return process.Process(input);
            }
            catch (Exception)
            {
                return default(Return);
            }
            
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        public void Send<T>(T obj)
        {
            Item = obj;
        }

        /// <summary>
        /// 返回需要的数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetInfo<T>()
        {
            return (T)Item;
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
