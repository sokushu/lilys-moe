using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface ISender
    {
        void Send(object obj);

        T GetInfo<T>();
    }
}
