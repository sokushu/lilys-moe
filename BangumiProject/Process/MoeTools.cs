using BangumiProject.Controllers;
using BangumiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BangumiProject.Process
{
    /// <summary>
    /// 本站内的Moe工具
    /// </summary>
    public class MoeTools
    {
        /// <summary>
        /// 得到一张图片的，允许查看用户
        /// </summary>
        /// <param name="Public"></param>
        /// <param name="Users"></param>
        /// <returns></returns>
        public string GetImage_ReadUsers(bool Public, params string[] Users)
        {
            StringBuilder sb = new StringBuilder();
            if (Public == false)
            {
                foreach (var item in Users)
                {
                    sb.Append(item).Append(",");
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<List<List<string>>> GetVersionLog(string path)
        {
            if (File.Exists(path))
            {
                string[] log = await File.ReadAllLinesAsync(path);
                List<List<string>> list = new List<List<string>>();
                List<string> itemList = new List<string>();
                foreach (var item in log)
                {
                    if (item.Contains(Final.OneLineStr))// 找到了不同日记得分隔符号
                    {
                        list.Add(itemList);
                        itemList = new List<string>();
                    }
                    else
                    {
                        itemList.Add(item);
                    }
                }
                return list;
            }
            else
            {
                return new List<List<string>>();
            }
            
        }

        
    }

    /// <summary>
    /// 配置文件的读取用
    /// </summary>
    public class ReadConfig : Hashtable
    {
        private string FilePath { get; set; }
        public ReadConfig() : this(Final.FilePath + "Config"){ }
        public ReadConfig(string FilePath)
        {
            this.FilePath = FilePath;
            if (File.Exists(FilePath) == false)
            {
                File.Create(FilePath);
            }
            else
            {
                var Data = File.ReadAllLines(FilePath);
                foreach (var item in Data)
                {
                    if (item.Length > 0)
                    {
                        int i = 0;
                        var k = item.Substring(0, (i = item.IndexOf('=')) > 0 ? i : item.Length);
                        var v = item.Substring(i > 0 ? i : 0);
                        switch (k.Count())
                        {
                            case 1:
                                base.Add(k[0], string.Empty);
                                break;
                            case 2:
                                base.Add(k[0], k[1]);
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
        }
        public override void Add(object key, object value)
        {
            base.Add(key, value);
            File.AppendAllText(FilePath, $"{key}={value}\n");
        }
    }
}
