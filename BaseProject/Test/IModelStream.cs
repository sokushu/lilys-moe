using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    public interface IModelStream<Model>
    {
        string PageModelName { get; }

        void SetModelLoader<ProModel>(IModelLoader<ProModel> modelLoader);

        Model Build();
    }
}
