using BaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Test
{
    public class CoreProcess : ISender
    {
        public CoreProcess()
        {

        }

        public void Send<T>(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
