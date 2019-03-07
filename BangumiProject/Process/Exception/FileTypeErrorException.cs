using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Exception
{
    public class FileTypeErrorException : System.Exception
    {
        public FileTypeErrorException(string message) : base(message)
        {
        }
    }
}
