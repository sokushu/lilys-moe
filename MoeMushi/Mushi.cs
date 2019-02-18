using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpCode.Core;
using MoeMushi.Analyzer;
using MoeMushi.Interface;
using MoeMushi.Process;
using System.Reflection;

namespace MoeMushi
{
    public class Mushi<T>
    {
        public static void Run()
        {
            Type type = typeof(T);
            //type.Assembly.
        }

        public static void Run<Setting>()
        {

        }
    }
}
