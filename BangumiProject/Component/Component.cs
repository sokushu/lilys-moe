using BangumiProject.Component.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Component
{
    /// <summary>
    /// 提供组件支持
    /// </summary>
    public class Component : IComponents
    {
        public IEnumerable<ComponentInfo> GetInfo()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IComponents> Load(IServiceCollection service)
        {
            throw new NotImplementedException();
        }
    }
}
