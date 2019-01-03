using BangumiProject.Models;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services
{
    public abstract class CommDB<Model> : ICommDB
    {
        private readonly IMemoryCache _memoryCache;
        private readonly BangumiProjectContext _db;
        public CommDB(IMemoryCache _memoryCache, BangumiProjectContext _db)
        {
            this._db = _db;
            this._memoryCache = _memoryCache;
        }

        public void DBUpdate<T>()
        {
            throw new NotImplementedException();
        }

        public T GetDate<T>(char[] key) where T : class
        {
            throw new NotImplementedException();
        }

        public void SetCache<T>(char[] key, T Value) where T : class
        {
            throw new NotImplementedException();
        }

        public T GetDate<T>(Func<BangumiProjectContext, T> func)
        {
            T t = func.Invoke(_db);
            return t;
        }

        public void Test()
        {
            GetDate(async f => await f.SaveChangesAsync());
        }
    }
}
