using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public interface IPageModelLoader
    {
        void SetModelLoader<Model>(IModelLoader<Model> modelLoader);

        void SetModel<Model>(Model model);

        object BuildPageData();
    }
}
