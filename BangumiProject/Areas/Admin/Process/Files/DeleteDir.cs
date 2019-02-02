using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
namespace BangumiProject.Areas.Admin.Process.Files
{
    public class DeleteDir : IFileProcess
    {
        public void Process(string Path)
        {
            if (!Directory.Exists(Path))
            {
                return;
            }
            Delete(Path);
        }


        private void Delete(string Path)
        {
            string[] dir = Directory.GetDirectories(Path);
            string[] files = Directory.GetFiles(Path);
            foreach (var item in files)
            {
                File.Delete(item);
            }
            if (dir.Length > 0)
            {
                foreach (var item in dir)
                {
                    Delete(item);
                    Directory.Delete(item);
                }
            }
            Directory.Delete(Path);
        }
    }
}
