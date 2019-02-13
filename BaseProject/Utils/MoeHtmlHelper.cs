using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Utils
{
    public static class MoeHtmlHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="S">显示的字数</param>
        /// <returns></returns>
        public static string Sub(string Input, int S)
        {
            return $"{Input.Substring(0, S)}...";
        }
    }
}
