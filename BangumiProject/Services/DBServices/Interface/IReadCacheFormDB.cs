using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    interface IReadCacheFormDB<T>
    {
        T GetCacheFormDB(Func<string> Linq);
    }
}
