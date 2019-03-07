using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class CoreProcess<T>
    {
        public CoreProcess()
        {

        }
        private List<IProcess<T>> Processes = new List<IProcess<T>>();
        public void SetProcess(IProcess<T> process)
        {
            Processes.Add(process);
        }

        public T Process(T t)
        {
            T a = t;
            foreach (var item in Processes)
            {
                a = item.Process(a);
            }
            return a;
        }

    }
}
