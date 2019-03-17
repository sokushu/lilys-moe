using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class MoeNull
    {
        public static bool IsNull(this object obj)
        {
            return obj == null;
        }

        public static ICollection<T> NullEmpty<T>(this ICollection<T> ts)
        {
            return ts ?? new List<T>();
        }

        public static string NullEmpty(this string str)
        {
            return str ?? string.Empty;
        }
    }
}
