using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface IDCache
    {
        void CacheID(int ID);

        bool HasID(int ID);

        void RemoveID(int ID);
    }
}
