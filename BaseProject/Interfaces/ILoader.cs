﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface ILoader<T>
    {
        T Load(params object[] Value);
    }
}
