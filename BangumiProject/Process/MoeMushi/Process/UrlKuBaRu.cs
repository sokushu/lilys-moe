using BangumiProject.Process.MoeMushi.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.MoeMushi.Process
{
    /// <summary>
    /// 一个简单的URL分配器
    /// </summary>
    public class UrlKuBaRu : IUrl
    {
        private LinkedList<string> LinkURL = new LinkedList<string>();

        public UrlKuBaRu(params string[] urls)
        {
            foreach (string item in urls)
            {
                LinkURL.AddLast(item);
            }
        }
        public UrlKuBaRu(IEnumerable<string> urls)
        {
            foreach (string item in urls)
            {
                LinkURL.AddLast(item);
            }
        }
        public UrlKuBaRu() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="URL"></param>
        public void AddURL(string URL)
        {

        }

        public int GetSize()
        {
            return LinkURL.Count;
        }

        public string GetURL()
        {
            string value = LinkURL.FirstOrDefault();
            if (value != null)
                LinkURL.RemoveFirst();
            return value;
        }
    }
}
