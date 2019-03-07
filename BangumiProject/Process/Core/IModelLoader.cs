using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public abstract class IModelLoader<Model>
    {
        protected Model model;

        public abstract Model LoadModel();

        public abstract Model AfterProcess(Model model);

        public Model BuildModel()
        {
            model = LoadModel();
            return AfterProcess(model);
        }
    }
}
