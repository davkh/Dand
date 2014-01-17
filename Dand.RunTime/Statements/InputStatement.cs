using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class InputStatement : IStatement
    {
        #region Constructor

        public InputStatement(string name)
        {
            this.name = name;
        }

        #endregion

        #region Private Fields

        
        private string name;

        #endregion

        #region IStatement Members
        public string Translate(int currentLocation = -1)
        {

            string resultName = "";
            if (currentLocation != -1)
            {
                SymbolTableFunctionElement element = (SymbolTableFunctionElement)SymbolTable.GetInstance.Get(currentLocation);
                int position = element.checkInArgs(this.name);
                if (-1 != position)
                {
                    resultName = "fp - " + Abs(position - element.ArgumentsCount).ToString(); ;
                }
                else if (-1 != element.checkInLocals(this.name))
                {
                    resultName = "fp + " + Abs(element.checkInLocals(this.name) - element.LocalsCount);
                }
                else if (SymbolTable.GetInstance.contains(this.name))
                {
                    resultName = this.name;
                }
            }
            else if (SymbolTable.GetInstance.contains(this.name))
            {
                resultName = this.name;
            }
            if("" == resultName)
            {
                throw new Exception(string.Format("Variable {0} is not declared", this.name));
            }

            return "IN " + resultName + Environment.NewLine;
        }

        private int Abs(int p)
        {
            if (p < 0)
                return (-p);
            return p;
        }

        #endregion
    }
}
