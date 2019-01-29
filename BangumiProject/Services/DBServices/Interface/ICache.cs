using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface ICache
    {
        Data SaveCache<Data>(Data model);

        void RemoveCache();

        Data GetCache<Data>();

        ICache BuildKey(string Key);
    }
}
