using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    public interface IModelStream<Model>
    {
        Model Build();
    }
}
