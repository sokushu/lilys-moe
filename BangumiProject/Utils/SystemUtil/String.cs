using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public static class String
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool ToBool(this string str)
        {
            return bool.TryParse(str, out bool b) == true ? b : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int ToInt(this string str)
        {
            return 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="policyName"></param>
        /// <returns></returns>
        public static Final.YURI_TYPE GetYuri_Type(this string policyName)
        {
            switch (policyName)
            {
                case Final.Yuri_Admin:
                    return Final.YURI_TYPE.Yuri_Admin;
                case Final.Yuri_Girl:
                    return Final.YURI_TYPE.Yuri_Girl;
                case Final.Yuri_Yuri1:
                    return Final.YURI_TYPE.Yuri_Yuri1;
                case Final.Yuri_Yuri2:
                    return Final.YURI_TYPE.Yuri_Yuri2;
                case Final.Yuri_Yuri3:
                    return Final.YURI_TYPE.Yuri_Yuri3;
                case Final.Yuri_Yuri4:
                    return Final.YURI_TYPE.Yuri_Yuri4;
                case Final.Yuri_Yuri5:
                    return Final.YURI_TYPE.Yuri_Yuri5;
                case Final.Yuri_Boy:
                    return Final.YURI_TYPE.Yuri_Boy;
                default:
                    return Final.YURI_TYPE.Yuri_Yuri1;
            }
        }
    }
}
