using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    public class Sender : ISender
    {
        private object Obj { get; set; }
        public T GetInfo<T>()
        {
            return (T)Obj;
        }

        public void Send(object obj)
        {
            this.Obj = obj;
        }

        public void Send<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
