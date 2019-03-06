using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IModels<T> where T : class
    {
        void Process(IProcess process, ref T Model);

        void Process(IProcess process, IProcessDoSome doSome, ref T Model);
    }
}
