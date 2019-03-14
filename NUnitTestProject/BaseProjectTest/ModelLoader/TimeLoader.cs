using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.ModelLoader
{
    public class TimeLoader : IModelLoader<DateTime>
    {
        public TimeLoader() : base("DateTime") { }
        public override DateTime AfterProcess(DateTime model)
        {
            return model;
        }

        public override DateTime LoadModel()
        {
            return DateTime.Now;
        }
    }
}
