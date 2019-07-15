using BangumiProjectDBServices.PageModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BangumiProject.Utils
{
    public class AdminSettingReadAndWrite
    {
        /// <summary>
        /// 
        /// </summary>
        private const string NULL = "null";

        /// <summary>
        /// 
        /// </summary>
        public AdminSettingReadAndWrite()
        {
            if (!File.Exists(Final.AdminSetting))
            {
                //第一次初始化，为了不使程序运行出错，要将数据初始化
                File.Create(Final.AdminSetting);
                Write(new AdminSetting()
                {
                    IsOpenSignUp = true,
                    IsWebSiteOpen = true,
                    IsShowTopPic = true,
                    PicPath = "HelloWorld"
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AdminSetting Read()
        {
            //构建对象
            //开始构建
            AdminSetting setting = new AdminSetting();

            var lines = File.ReadAllLines(Final.AdminSetting);
            var obj = setting.GetType();
            List<PropertyInfo> proinfo = new List<PropertyInfo>();
            proinfo.AddRange(obj.GetProperties());

            foreach (var data in lines)
            {
                var one = data.Split(',');
                var type = one[1];
                var KV = one[0].Split(':', 2);

                var key = KV[0];
                var value = KV[1];

                foreach (var item in proinfo)
                {
                    if (item.Name == key)
                    {
                        object valObj = null;
                        TypeCode typef = Type.GetTypeCode(Type.GetType(type));
                        if (value != NULL)
                        {
                            switch (typef)
                            {
                                case TypeCode.Boolean:
                                    valObj = bool.Parse(value);
                                    break;
                                case TypeCode.Byte:
                                    valObj = byte.Parse(value);
                                    break;
                                case TypeCode.Char:
                                    valObj = char.Parse(value);
                                    break;
                                case TypeCode.DateTime:
                                    valObj = DateTime.Parse(value);
                                    break;
                                case TypeCode.Double:
                                    valObj = double.Parse(value);
                                    break;
                                case TypeCode.Int16:
                                    valObj = short.Parse(value);
                                    break;
                                case TypeCode.Int32:
                                    valObj = int.Parse(value);
                                    break;
                                case TypeCode.Int64:
                                    valObj = long.Parse(value);
                                    break;
                                case TypeCode.String:
                                    valObj = value;
                                    break;
                                default:
                                    break;
                            }
                        }
                        item.SetValue(setting, valObj);
                    }
                }
                proinfo.RemoveAll(info => info.Name == key);
            }
            return setting;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="setting"></param>
        public void Write(AdminSetting setting)
        {
            List<string> file = new List<string>();
            var obj = setting.GetType();
            foreach (var item in obj.GetProperties())
            {
                file.Add($"{item.Name}:{item.GetValue(setting) ?? NULL},{item.PropertyType.FullName}");
            }
            File.WriteAllLines(Final.AdminSetting, file);
        }
    }
}
