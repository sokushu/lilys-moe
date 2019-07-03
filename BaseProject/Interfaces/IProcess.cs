using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface IProcess<T>
    {
        void Process(ref T Input);
    }
}
