using BangumiProject.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Reflection;

namespace BangumiProject.Areas.Admin.Process
{
    public class AdminSettingWriteAndRead
    {
        public AdminSettingWriteAndRead()
        {
            if (!File.Exists(Final.AdminSetting))
            {
                File.Create(Final.AdminSetting);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public AdminSetting Read()
        {
            var lines = File.ReadAllLines(Final.AdminSetting);
            //构建对象
            //开始构建
            AdminSetting setting = new AdminSetting();

            var obj = setting.GetType();
            List<PropertyInfo> proinfo = new List<PropertyInfo>();
            proinfo.AddRange(obj.GetProperties());

            foreach (var data in lines)
            {
                var one = data.Split(',');
                var type = one[1];
                var KV = one[0].Split(':');

                var key = KV[0];
                var value = KV[1];

                foreach (var item in proinfo)
                {
                    if (item.Name == key)
                    {
                        object dynamic = null;
                        TypeCode typef = Type.GetTypeCode(Type.GetType(type));
                        switch (typef)
                        {
                            case TypeCode.Boolean:
                                bool valueBool = bool.Parse(value);
                                dynamic = valueBool;
                                break;
                            case TypeCode.Byte:
                                break;
                            case TypeCode.Char:
                                break;
                            case TypeCode.DateTime:
                                break;
                            case TypeCode.DBNull:
                                break;
                            case TypeCode.Decimal:
                                break;
                            case TypeCode.Double:
                                break;
                            case TypeCode.Empty:
                                break;
                            case TypeCode.Int16:
                                break;
                            case TypeCode.Int32:
                                break;
                            case TypeCode.Int64:
                                break;
                            case TypeCode.Object:
                                break;
                            case TypeCode.SByte:
                                break;
                            case TypeCode.Single:
                                break;
                            case TypeCode.String:
                                break;
                            case TypeCode.UInt16:
                                break;
                            case TypeCode.UInt32:
                                break;
                            case TypeCode.UInt64:
                                break;
                            default:
                                break;
                        }
                        item.SetValue(setting, dynamic);
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
                file.Add($"{item.Name}:{item.GetValue(setting)},{item.PropertyType.FullName}");
            }
            File.WriteAllLines(Final.AdminSetting, file);
        }
    }
}
