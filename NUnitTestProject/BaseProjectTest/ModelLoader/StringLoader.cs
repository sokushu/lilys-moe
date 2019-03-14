using BaseProject.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestProject.BaseProjectTest.ModelLoader
{
    public class StringLoader : IModelLoader<string>
    {
        public StringLoader() : base("Name", "Pic") { }
        public override string AfterProcess(string model)
        {
            return model;
        }

        public override string LoadModel()
        {
            return "Pic.jpg";
        }
    }
}
