using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Component.Interface
{
    public interface IComponents
    {
        IEnumerable<IComponents> Load(IServiceCollection service);

        IEnumerable<ComponentInfo> GetInfo();
    }
}
