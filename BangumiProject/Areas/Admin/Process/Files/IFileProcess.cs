﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Areas.Admin.Process.Files
{
    public interface IFileProcess
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        void Process(string Path);
    }
}
