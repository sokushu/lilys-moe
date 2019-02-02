using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace BangumiProject.Areas.Admin.Process.Files
{
    public class CrateFiles : IFileProcess
    {
        /// <summary>
        /// 创建指定文件
        /// </summary>
        /// <param name="Path"></param>
        public void Process(string Path)
        {
            if (File.Exists(Path))
            {
                return;
            }
            File.Create(Path);
        }
    }
}
