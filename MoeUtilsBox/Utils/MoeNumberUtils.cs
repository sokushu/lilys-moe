using System;
using System.Collections.Generic;
using System.Text;

namespace MoeUtilsBox.Utils
{
    public static class MoeNumberUtils
    {
        public static bool BangumiIDChack(this int num)
        {
            return num > 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LineNum"></param>
        /// <param name="InputNumber"></param>
        /// <param name="ProcessStr"></param>
        /// <returns></returns>
        public static bool NumberCheck(this int LineNum, string ProcessStr, int InputNumber)
        {
            switch (ProcessStr)
            {
                case ">=":
                    return LineNum >= InputNumber;
                case "<=":
                    return LineNum <= InputNumber;
                case "==":
                    return LineNum == InputNumber;
                case ">":
                    return LineNum > InputNumber;
                case "<":
                    return LineNum < InputNumber;
                case "!=":
                    return LineNum != InputNumber;
                default:
                    return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LineNum">做比较的数</param>
        /// <param name="InputNumber">输入的数字</param>
        /// <param name="ProcessStr">要进行处理的数学比较符号</param>
        /// <param name="IfFalseDefauleNumber">如果不符合条件，返回False的时候的默认值</param>
        /// <returns></returns>
        public static bool NumberCheck(this int LineNum, string ProcessStr, ref int InputNumber, int IfFalseDefauleNumber)
        {
            bool returnF;
            if (!(returnF = NumberCheck(LineNum, ProcessStr, InputNumber)))
            {
                InputNumber = IfFalseDefauleNumber;
            }
            return returnF;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="LineNum"></param>
        /// <param name="ProcessStr"></param>
        /// <param name="InputNumber"></param>
        /// <param name="IfFalseDefauleNumber"></param>
        /// <param name="IfTrueDefauleNumber"></param>
        /// <returns></returns>
        public static bool NumberCheck(this int LineNum, string ProcessStr, ref int InputNumber, int IfFalseDefauleNumber, int IfTrueDefauleNumber)
        {
            bool returnF;
            if (returnF = NumberCheck(LineNum, ProcessStr, InputNumber))
            {
                InputNumber = IfTrueDefauleNumber;
            }
            else
            {
                InputNumber = IfFalseDefauleNumber;
            }
            return returnF;
        }
    }
}
