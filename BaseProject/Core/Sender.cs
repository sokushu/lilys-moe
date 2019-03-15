using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    public class Sender : ISender
    {
        private object obj { get; set; }
        public T GetInfo<T>()
        {
            return (T)obj;
        }

        public void Send(object obj)
        {
            this.obj = obj;
        }
    }
}
