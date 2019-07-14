using BaseProject.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    public abstract class LoaderStream<T>
    {

        private object[] Vs { get; set; }

        public LoaderStream(
            )
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        public virtual void SetParams(params object[] Value)
        {
            this.Vs = Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public T Load(params IProcess<T>[] process)
        {
            T value = Load(Vs);
            foreach (var Process in process)
            {
                Process.Process(ref value);
            }
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        protected abstract T Load(params object[] Value);
    }
}
