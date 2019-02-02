using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace BangumiProject.Areas.Admin.Process.Files
{
    public class CreateDir : IFileProcess
    {
        public void Process(string Path)
        {
            Directory.CreateDirectory(Path);
        }
    }
}
