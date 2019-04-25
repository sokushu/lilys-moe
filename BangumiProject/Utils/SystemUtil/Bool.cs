using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class Bool
    {
        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static int BoolToInt(this bool b)
        {
            return b ? 1 : 0;
        }
    }
}
