using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Bangumi.Process
{
    public struct AnimeNumberInfo
    {
        public int AllPage { get; set; }

        public string[] PageName { get; set; }
    }

    public struct AnimeInfo
    {
        public int Num { get; set; }
        public string Name { get; set; }
    }
}
