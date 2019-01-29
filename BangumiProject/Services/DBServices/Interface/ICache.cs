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
        Data SaveCache<Data>(Data model) where Data : class;

        void RemoveCache();

        IReadCacheFormDB<Data> GetCache<Data>() where Data : class;

        ICache BuildKey(string Key);
    }
}
