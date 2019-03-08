using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class CoreProcess<T> where T : class
    {
        private T obj = null;
        private object item = null;
        /// <summary>
        /// 初始化
        /// </summary>
        public CoreProcess()
        {}

        /// <summary>
        /// 设置处理类
        /// </summary>
        /// <param name="process"></param>
        public T SetProcess(IProcess<T> process)
        {
            T value = obj as T;
            var returnvalue = process.Process(value);
            return returnvalue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <param name="process2"></param>
        /// <returns></returns>
        public T1 SetProcess<T1>(IProcess2Parm<T1, T> process2)
        {
            T value = obj as T;
            var returnvalue = process2.Process(value);
            return returnvalue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="process2"></param>
        /// <returns></returns>
        public T1 SetProcess2<T1, T2>(IProcess2Parm<T1, T2> process2) where T2 : class
        {
            T2 value = item as T2;
            return process2.Process(value);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void SetData(T obj)
        {
            this.obj = obj;
        }

        public void SetItem(object obj)
        {
            item = obj;
        }
    }
}
