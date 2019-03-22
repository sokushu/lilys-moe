using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    public interface IModelLoader<Model>
    {
        string[] PropertiesName { get; }
        Model Load();

        Model AfterProcess(Model model);
    }
}
