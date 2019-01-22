using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IAnimeProcess
    {
        bool Process(ref Anime anime);
    }
}
