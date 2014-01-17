using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class DeclerationStatement : IStatement
    {
        #region Constructor

        public DeclerationStatement(int id)
        {
            this.symbolId = id;
        }

        #endregion

        #region Private Fields

        private int symbolId;

        #endregion

        #region IStatement Members
        public string Translate(int currentLocation = -1)
        {
            string result = "";

            //TODO: Implement.

            return result;
        }
        #endregion
    }
}
