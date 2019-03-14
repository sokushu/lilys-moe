using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.ModelLoader
{
    public class IntLoader : IModelLoader<int>
    {
        public IntLoader() : base("Year") { }
        public override int AfterProcess(int model)
        {
            return model;
        }

        public override int LoadModel()
        {
            return 23;
        }
    }
}
