using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace System
{
    public static class StringUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTimes"></param>
        /// <returns></returns>
        public static string DateTimeToString(this ICollection<DateTime> dateTimes)
        {
            StringBuilder builder = new StringBuilder();
            foreach (var item in dateTimes)
            {
                builder.Append(item.ToString("yyyy-MM-dd"));
                builder.Append(',');
            }
            return builder.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="DateTimes"></param>
        /// <returns></returns>
        public static ICollection<DateTime> StringToDateTime(this string DateTimes)
        {
            string[] date = DateTimes.Split(',');
            DateTimeFormatInfo dtFormat = new DateTimeFormatInfo
            {
                ShortDatePattern = "yyyy-MM-dd"
            };
            List<DateTime> ReturnList = new List<DateTime>();
            foreach (string item in date)
            {
                ReturnList.Add(Convert.ToDateTime(item, dtFormat));
            }
            return ReturnList;
        }
    }
}
