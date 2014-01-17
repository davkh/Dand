using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class FunctionCall : IStatement
    {
        private string functionName;
        CallArgList argList;
        public FunctionCall(string functionName_, CallArgList argList_)
        {
            functionName = functionName_;
            argList = argList_;
        }

        private bool isArgumentsCorespondes(SymbolTableFunctionElement element)
        {
            if (element.ArgumentsCount == argList.getArgumentsCount())
            {
                
            }
            return true;
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "";
            if (SymbolTable.GetInstance.contains(functionName))
            {
                SymbolTableElement element = SymbolTable.GetInstance.Get(functionName);
                if (element is SymbolTableFunctionElement)
                {
                    if (argList.getArgumentsCount() == ((SymbolTableFunctionElement)element).ArgumentsCount)
                    {
                        result = argList.Translate(currentLocation);
                        result = result + "CALL " + functionName;
                    }
                    else
                    {
                        throw new ApplicationException("Function with name " + functionName + " and with " + argList.getArgumentsCount() + "  arguments doesn't exists");
                    }
                }
                
            }
            if("" == result)
            {
                throw new ApplicationException("Function with name " + functionName + "  doesn't exists");
            }

            return result + Environment.NewLine;
        }
    }
}
