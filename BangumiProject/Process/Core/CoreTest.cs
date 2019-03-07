using BangumiProject.Process.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Core
{
    public class CoreTest
    {
        public void Main()
        {
            IPageModelLoader pageModelLoader = new PageModelLoader();
            
            pageModelLoader.SetModelLoader(new StringLoader());

            pageModelLoader.BuildPageData<Tuple<string>>();
        }
    }


    public class StringLoader : IModelLoader<string>
    {
        public override string AfterProcess(string model)
        {
            CoreProcess<string> coreProcess = new CoreProcess<string>();

            coreProcess.SetProcess(new AddWorld());
            coreProcess.SetProcess(new AddWWW());

            return coreProcess.Process(model);
        }

        public override string LoadModel()
        {
            return "Hello";
        }
    }

    public class AddWorld : IProcess<string>
    {
        public string Process(string t)
        {
            return $"{t} World";
        }
    }

    public class AddWWW : IProcess<string>
    {
        public string Process(string t)
        {
            return $"WWW {t}";
        }
    }
}
