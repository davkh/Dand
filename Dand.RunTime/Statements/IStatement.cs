using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public interface IStatement
    {
        string Translate(int currentLocation = -1);
    }
}
