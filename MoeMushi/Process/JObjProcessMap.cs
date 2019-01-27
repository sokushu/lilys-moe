using MoeMushi.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoeMushi.Process
{
    public abstract class JObjProcessMap : IProcess<List<Dictionary<string, object>>>, ISave
    {
        public abstract List<Dictionary<string, object>> Process(IAnalyzer analyzer);
        public abstract void Save();
    }
}
