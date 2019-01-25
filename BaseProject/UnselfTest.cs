using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject
{
    public class UnselfTest
    {
        public unsafe void Test()
        {
            int b = 23;
            int* a = &b;
        }
    }
}
