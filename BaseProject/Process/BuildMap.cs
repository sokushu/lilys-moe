using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Process
{
    public static class BuildMap
    {
        public static void Build<Model>()
        {
            Type type = typeof(Model);

            var Name = type.Name;
            var Properties = type.GetProperties();
            if (Properties != null && Properties.Length > 0)
            {
                CoreSettingAndData.PropertyInfos.Add(Name, Properties);
            }
        }
    }
}
