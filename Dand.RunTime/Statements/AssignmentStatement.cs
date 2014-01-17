using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class AssignmentStatement : IStatement
    {
        private string name;
        private Expression exp;

        public AssignmentStatement(string name , Expression exp)
        {
            this.name = name;
            this.exp = exp;
        }

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
                    resultName = "fp+" + (position + 1).ToString();
                }
                else
                {
                    if (-1 != element.checkInLocals(this.name))
                    {
                        resultName = "fp+" + (element.ArgumentsCount + element.checkInLocals(this.name) + 1);
                    }
                    else if (SymbolTable.GetInstance.contains(this.name))
                    {
                        resultName = this.name;
                    }
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


            return exp.Translate(currentLocation) + "POP " + resultName + Environment.NewLine;
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
