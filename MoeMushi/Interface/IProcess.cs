using System;
using System.Collections.Generic;
using System.Text;

namespace MoeMushi.Interface
{
    public interface IProcess<T>
    {
        /// <summary>
        /// 返回什么类型的数据，
        /// 你说了算！！
        /// </summary>
        /// <returns></returns>
        T Process(IAnalyzer analyzer);
    }
}
