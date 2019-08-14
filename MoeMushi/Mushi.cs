using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HttpCode.Core;
using System.Reflection;
using MoeMushi.Core.DownLoad;
using System.IO;

namespace MoeMushi
{
    public abstract class Mushi<T>
    {

        protected bool GetOrPost { get; set; }

        protected string POSTData { get; set; } = string.Empty;
        public Mushi()
        {

        }

        public void Run(string URL)
        {
            HtmlDownLoad htmlDownLoad = new HtmlDownLoad(URL);
            string HTML = string.Empty;
            if (GetOrPost)
            {
                HTML = htmlDownLoad.GET();
            }
            else
            {
                if (POSTData == string.Empty || POSTData == null)
                {
                    throw new NullReferenceException();
                }
                else
                {
                    HTML = htmlDownLoad.POST(POSTData);
                }
            }
            //送去解析

        }
    }


    public enum Save
    {
        File,
        DB,
        None,
    }

}
