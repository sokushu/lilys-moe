using BangumiProject.Areas.Bangumi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Interface
{
    public interface IBangumiCase
    {
        List<Anime> AnimeFilter(List<Anime> animes);
    }
}
