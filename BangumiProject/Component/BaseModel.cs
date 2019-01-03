using BangumiProject.Component.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BangumiProject.Component
{
    public class BaseModel<T> : IModel
    {
        public Dictionary<string, PropertyInfo> Properties { get; private set; }

        public Type ModelType { get; private set; }

        public BaseModel()
        {

        }
    }
}
