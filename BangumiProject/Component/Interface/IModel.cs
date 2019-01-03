using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BangumiProject.Component.Interface
{
    public interface IModel
    {
        Dictionary<string, PropertyInfo> Properties { get; }

        Type ModelType { get; }
    }
}
