using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Services.DBServices.Interface
{
    public interface IReadCacheFormDB<T> where T : class
    {
        List<T> GetCacheFormDB(Func<DbSet<T>, List<T>> func);

        T GetCacheFormDB(Func<DbSet<T>, T> func);
    }

    public interface IReadCacheFormDBA<T>
    {
        List<C> GetCacheFormDB<C>(Func<DbSet<T>, List<C>> func);

        C GetCacheFormDB<C>(Func<DbSet<T>, C> func);
    }
}
