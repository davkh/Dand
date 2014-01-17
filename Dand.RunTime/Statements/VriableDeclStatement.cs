using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class VriableDeclStatement: IStatement 
    {
        private int varID;

        public VriableDeclStatement(int id)
        {
            this.varID = id;
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "DECL " + SymbolTable.GetInstance.Get(this.varID).Name + Environment.NewLine;

            return result;
        }
    }
}
