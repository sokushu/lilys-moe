using System;
using System.Collections.Generic;
using System.Text;

namespace MoeUtilsBox.String
{
    public static class MoeStringUtils
    {
        /// <summary>
        /// 返回一个随机的文件名
        /// </summary>
        /// <returns></returns>
        public static string GetFilePath(this string Path)
        {
            return new StringBuilder(Path).Append(Guid.NewGuid().ToString("N")).ToString();
        }

        /// <summary>
        /// 判断是否是数字
        /// </summary>
        /// <param name="Number"></param>
        /// <returns></returns>
        public static bool IsNumber(this string Number)
        {
            foreach (char item in Number)
            {
                if (!char.IsNumber(item))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string IfEmptyReturnNull(this string Str)
        {
            if (Str == null) return null;
            if (Str.Length == 0) return null;
            foreach (var item in Str)
            {
                if (!char.IsWhiteSpace(item))
                {
                    return Str.Trim();
                }
            }
            return null;
        }
    }
}
