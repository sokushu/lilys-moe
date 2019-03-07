using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class MoeString
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <param name="process"></param>
        /// <returns></returns>
        public static string IfStringIsNull(this string str, IProcess<string> process)
        {
            if (str == null)
            {
                return process.Process(str);
            }
            return str;
        }
    }
}
