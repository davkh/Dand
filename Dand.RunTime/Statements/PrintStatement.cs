using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class PrintStatement : IStatement
    {
        private Expression expr;

        public PrintStatement(Expression expr)
        {
            this.expr = expr;
        }

        #region IStatement Members

        public string Translate(int currentLocation = -1)
        {
            string result = expr.Translate(currentLocation);
            
            if(result != "")
                result = result + "OUT" + Environment.NewLine;

            return result;
        }

        #endregion
    }
}
