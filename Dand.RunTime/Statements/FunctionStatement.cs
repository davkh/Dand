using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class FunctionStatement : IStatement
    {
        int id = 0;
        StatementList functionBody = null;
        ArgumentList argumentList = null;
        public FunctionStatement()
        {
        }
        public FunctionStatement(int name_id, IStatement args, StatementList funcbody)
        {
            id = name_id;
            SymbolTableFunctionElement element = (SymbolTableFunctionElement)SymbolTable.GetInstance.Get(name_id);
            argumentList = (ArgumentList)args;
            functionBody = funcbody;
            List<LocalVariable> Localargs = argumentList.Arguments;
            
            foreach (LocalVariable var in Localargs)
            {
                element.addArgs(var);
            }
            List<LocalVariable> allLocals = funcbody.Arguments;
            foreach (LocalVariable var in allLocals)
            {
                element.addLocals(var);
            }

        }

        public string Translate(int currentLocation = -1)
        {
            string result = "";
        
            result += "PROC " + SymbolTable.GetInstance.Get(this.id).Name + Environment.NewLine;
            SymbolTableFunctionElement element = (SymbolTableFunctionElement)SymbolTable.GetInstance.Get(this.id);
            int argCount = element.ArgumentsCount;
            for (int i = 0; i < argCount; ++i)
            {
                result += "PUSH fp-" + (argCount - i).ToString() + Environment.NewLine;
            }
            
            for(int i = 0; i < element.LocalsCount; i++)
            {
                result += "PUSH 0" + Environment.NewLine;
            }

            result += functionBody.Translate(id);

            if (!functionBody.ReturnExists(id))
                result = result + "PUSH 0" + Environment.NewLine;

            result += "ENDPROC" + Environment.NewLine;
            return result;
        }
    }
}
