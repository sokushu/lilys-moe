using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    public abstract class ModelLoader<Model> : IModelLoader<Model>
    {
        public string[] PropertiesName { get; }

        public ModelLoader(params string[] PropertiesName)
        {
            this.PropertiesName = PropertiesName;
        }

        public abstract Model AfterProcess(Model model);
        public abstract Model Load();
    }
}
