using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Admin.Process.Files
{
    public class DeleteFiles : IFileProcess
    {
        /// <summary>
        /// 删除指定文件
        /// </summary>
        /// <param name="Path"></param>
        public void Process(string Path)
        {
            if (!File.Exists(Path))
            {
                return;
            }
            File.Delete(Path);
        }
    }
}
