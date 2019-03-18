using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Exceptionss
{
    public class AnimeNotFoundException : Exception
    {
        public AnimeNotFoundException() : base() { }
        public AnimeNotFoundException(string ErrorInfo) : base(ErrorInfo) { }
    }
}
