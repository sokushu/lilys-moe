using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class CoreProcess<T>
    {
        private object obj = null;

        /// <summary>
        /// 初始化
        /// </summary>
        public CoreProcess(object obj)
        {
            this.obj = obj;
        }

        public CoreProcess() { }

        private List<IProcess<T>> Processes = new List<IProcess<T>>();

        /// <summary>
        /// 设置处理类
        /// </summary>
        /// <param name="process"></param>
        public void SetProcess(IProcess<T> process)
        {
            Processes.Add(process);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="process2"></param>
        /// <param name="t2"></param>
        /// <returns></returns>
        public T1 SetProcess<T1, T2>(IProcess2Parm<T1, T2> process2) where T2 : class
        {
            T2 t2 = obj as T2;
            return process2.Process(t2);
        }

        /// <summary>
        /// 进行处理
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public T Process(T t)
        {
            T a = t;
            foreach (var item in Processes)
            {
                a = item.Process(a);
            }
            return a;
        }

    }
}
