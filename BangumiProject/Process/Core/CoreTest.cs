using BangumiProject.Areas.Bangumi.Models;
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

            coreProcess.SetData(model);
            coreProcess.SetProcess(new AddWorld());
            coreProcess.SetProcess(new AddWWW());
            var value = coreProcess.SetProcess(new AddBGBG());

            coreProcess.SetItem(value);

            var returnvalue = coreProcess.SetProcess2(new ADDCHAR());

            return returnvalue;
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

    public class AddBGBG : IProcess2Parm<List<char>, string>
    {
        public List<char> Process(string t)
        {
            return new List<char>(t.ToCharArray());
        }
    }

    public class ADDCHAR : IProcess2Parm<string, List<char>>
    {
        public string Process(List<char> t)
        {
            return t.ToString();
        }
    }

    public class AAA : ModelStream<Anime>
    {
        public override Anime Build()
        {
            Anime anime = new Anime
            {
                AnimeComms = values[0] as List<AnimeComm>
            };

            return anime;
        }
    }
}
