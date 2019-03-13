using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BaseProject
{
    public static class CoreSettingAndData
    {
        public static Dictionary<string, PropertyInfo[]> PropertyInfos { get; } = new Dictionary<string, PropertyInfo[]>();
    }
}
