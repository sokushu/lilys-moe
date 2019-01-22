using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IMessageModel
    {
        string Pic { get; set; }
        string Title { get; set; }
        string Info { get; set; }
        DateTime Time { get; set; }
        int MID { get; set; }
    }
}
