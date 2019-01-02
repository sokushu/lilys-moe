using BangumiProject.Process.MoeMushi;
using HtmlAgilityPack;
using HttpCode.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BangumiProject.Process.BackRun
{
    /// <summary>
    /// 在后面没事就瞎跑的一个类
    /// 
    /// 做一下爬虫工作之流的
    /// </summary>
    public class BackRun
    {
        public void WorkAsync(MushiParams backParams)
        {
            if (Mushi.IsEnd == true)
            {
                Mushi.IsEnd = false;
                Thread thread = new Thread(new ParameterizedThreadStart(Start));
                thread.Start(backParams);
            }
        }

        private void Start(object a)
        {
            MushiParams backParams = a as MushiParams;

            // 开始爬虫处理

            
        }
    }
}
