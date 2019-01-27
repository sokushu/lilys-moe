using MoeMushi.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoeMushi.Process
{
    public abstract class JObjProcessModel<Model> : IProcess<List<Model>>, ISave
    {
        public abstract List<Model> Process(IAnalyzer analyzer);
        public abstract void Save();
    }
}
