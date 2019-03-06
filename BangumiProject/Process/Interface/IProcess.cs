using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">处理数据返回类型</typeparam>
    public interface IProcess
    {
        void Process<T>(ref T Model) where T : class;
    }
}
