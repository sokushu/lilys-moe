using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public interface IPageModelLoader
    {
        void SetModelLoader<Model>(IModelLoader<Model> modelLoader);

        A BuildPageData<A>() where A : class;
    }
}
