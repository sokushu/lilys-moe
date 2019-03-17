using BangumiProjectDBServices.Models;
using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BangumiProjectProcess.AnimeProcess
{
    public interface IAnimeFilter : IProcess<List<Anime>>
    {
    }
}
