using BangumiProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services
{
    public interface ICommDB
    {
        T GetDate<T>(Func<BangumiProjectContext, T> func);
        void SetCache<T>(char[] key, T Value) where T : class;
        T GetDate<T>(char[] key) where T : class;
        void DBUpdate<T>();
    }
}
