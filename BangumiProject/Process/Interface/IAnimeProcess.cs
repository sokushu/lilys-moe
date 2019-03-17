using BangumiProject.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IAnimeProcess : IProcess<Anime>
    {
    }

    public interface IAnimeProcessList : IProcess<List<Anime>>
    {
    }
}
