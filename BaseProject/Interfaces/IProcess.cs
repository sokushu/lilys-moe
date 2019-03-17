using System;
using System.Collections.Generic;
using System.Text;

namespace BaseProject.Interfaces
{
    public interface IProcess<Return, Input>
    {
        Return Process(Input input);
    }
}
