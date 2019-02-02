using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Admin.Process.Files
{
    public class Rename : IFileProcess
    {
        private string Name { get; set; }
        public Rename(string Rename)
        {
            Name = Rename;
        }

        public void Process(string Path)
        {
            throw new NotImplementedException();
        }
    }
}
