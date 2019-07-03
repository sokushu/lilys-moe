using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Core
{
    class NullCheck<T> : IProcess<T>
    {
        public void Process(ref T Input)
        {
            if (Input == default)
            {
                throw new NullReferenceException();
            }
        }
    }
}
