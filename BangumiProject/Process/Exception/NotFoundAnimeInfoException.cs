using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Exception
{
    public class NotFoundAnimeInfoException : System.Exception
    {
        public NotFoundAnimeInfoException(string info) : base(info)
        {

        }
        public NotFoundAnimeInfoException() : base() { }
    }
}
