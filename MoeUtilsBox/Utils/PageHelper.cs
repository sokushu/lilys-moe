using System;
using System.Collections.Generic;
using System.Text;

namespace MoeUtilsBox.Utils
{
    public class PageHelper
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly int Data;
        private int AllPage;
        private int NowPage;
        /// <summary>
        /// 初始化分页，输入单页显示的信息
        /// </summary>
        /// <param name="Data">单页显示多少信息</param>
        public PageHelper(int Data)
        {
            this.Data = Data;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetStartPage()
        {
            return 1;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetAllPage()
        {
            return AllPage;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int GetNowPage()
        {
            return NowPage;
        }
        /// <summary>
        /// 进行分页
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="Page">想要返回的数据是第几页的数据</param>
        /// <param name="list">想要分页的数据</param>
        /// <returns></returns>
        public List<T> GetListPage<T>(int Page, List<T> list)
        {
            if (MoeNullCheck.IsNull(list)) MoeListUtils.GetEmpty<T>();
            int L = list.Count;
            // 计算总页数
            if (L < Data)
            {
                AllPage = 1;
            }
            else
            {
                AllPage = (list.Count / Data);
            }
            // 计算现在的页数，如果请求页数大于总页数，则请求最后一页
            if (Page > AllPage)
            {
                Page = AllPage;
            }
            NowPage = Page;
            // 计算结束
            int Num = Page * Data;
            // 计算开始
            int Start = Num - Data;

            if (Start < 0) Start = 0;

            List<T> RList = new List<T>();

            for (int i = Start; i < Num; i++)
            {
                if (L <= i)
                {
                    break;
                }
                RList.Add(list[i]);
            }

            return RList;
        }
    }
}
