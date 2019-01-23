﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BangumiProject.Process.Interface
{
    public interface IProcess<R>
    {
        void ProcessAsync(R r);
    }
}
