using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System
{
    public static class FileUtils
    {
        /// <summary>
        /// 得到文件路径中的文件名
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetFileName(this string str)
        {
            if (str == null)
                return string.Empty;
            return Path.GetFileName(str);
        }

        /// <summary>
        /// 得到图片缩略图文件目录
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetThumbFilePath(this string str)
        {
            if (str == null)
                return string.Empty;
            return $"{Final.FilePath_Image_Thumb}{str}";
        }

        /// <summary>
        /// 得到图片的路径
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetImageFilePath(this string str)
        {
            if (str == null)
                return string.Empty;
            return $"{Final.FilePath_Image}{str}";
        }
    }
}
