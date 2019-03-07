using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IProcess2Parm<p1, p2>
    {
        p1 Process(p2 t);
    }
}
