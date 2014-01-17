using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class FunctionList : IStatement
    {
        List<FunctionStatement> functions = new List<FunctionStatement>();

        public void add(FunctionStatement func)
        {
            functions.Add(func);
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "";

            foreach (FunctionStatement func in functions)
            {
               result += func.Translate();
            }

            return result;
        }

    }
}
