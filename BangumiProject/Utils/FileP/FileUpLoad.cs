using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace BangumiProject.Utils.FileP
{
    public class FileUpLoad
    {
        public FileUpLoad()
        {

        }

        public void SaveFile(string Path, IFormFile[] iformfile)
        {
            foreach (IFormFile file in iformfile)
            {
                string fileName = file.FileName;
                string contentType = file.ContentType;
                long fileSize = file.Length;

                Stream stream = null;
                try
                {
                    using (stream = new FileStream(Path, FileMode.CreateNew))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }
                }
                catch (IOException)
                {
                    using (stream = new FileStream(Path, FileMode.Open))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    stream = null;
                }

            }
        }
    }
}
