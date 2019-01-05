using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MoeUtilsBox.Utils
{
    public static class MoeListUtils
    {
        public static List<T> GetEmpty<T>()
        {
            return new List<T>();
        }

        public static List<T> GetListDate<T>(this List<T> date)
        {
            if (date == null)
                return GetEmpty<T>();
            return date;
        }

        public static List<T> GetListDate<T>(this ICollection<T> date)
        {
            if (date == null)
                return GetEmpty<T>();
            return date.ToList();
        }
    }
}
