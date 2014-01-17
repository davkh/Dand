using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class CallArg : IStatement
    {
        private Expression expr;

        public CallArg(Expression expr_)
        {
            expr = expr_;
        }

        public string Translate(int currentLocation = -1)
        {
               return expr.Translate(currentLocation);
        }
    }
}
