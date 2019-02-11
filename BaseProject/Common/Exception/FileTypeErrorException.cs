using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Common.Exception
{
    public class FileTypeErrorException : System.Exception
    {
        public FileTypeErrorException(string message) : base(message)
        {
        }
    }
}
