using BangumiProject.Models;
using BangumiProject.Services.DBServices.DBC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface ICache
    {
        BangumiProjectContext _db { get; }
        Data SaveCache<Data>(Data model);

        void RemoveCache();

        CacheFormDB<Data> GetCache<Data>();

        ICache BuildKey(string Key);
    }
}
