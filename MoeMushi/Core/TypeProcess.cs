using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MoeMushi.Core
{
    public class TypeProcess<T>
    {
        private Type Type { get; set; }
        private Dictionary<string, PropertyInfo> PropertyInfos = new Dictionary<string, PropertyInfo>();

        public TypeProcess()
        {
            Type = typeof(T);
            foreach (var item in Type.GetProperties())
            {
                PropertyInfos[item.Name] = item;
            }
        }
    }
}
