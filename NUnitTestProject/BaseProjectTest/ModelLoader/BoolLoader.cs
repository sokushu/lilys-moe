using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.ModelLoader
{
    public class BoolLoader : IModelLoader<bool>
    {
        public BoolLoader() : base("SignIn") { }
        public override bool AfterProcess(bool model)
        {
            return model;
        }

        public override bool LoadModel()
        {
            return true;
        }
    }
}
