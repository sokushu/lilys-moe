using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface ISender
    {
        void Send<T>(T obj);
    }
}
