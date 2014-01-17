using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class MainProgram : IStatement
    {
        private StatementList statmentList = new StatementList();
        private FunctionList functionList = new FunctionList();

        public FunctionList Function
        {
            set { functionList = value; }
        }

        public StatementList Statement
        {
            set { statmentList = value; }
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "";

            try
            {
                result += functionList.Translate();

                if (result != "")
                    result = "JMP BEGIN\n" + result;

                result += "BEGIN: NOP\n" + statmentList.Translate();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error: " + e.Message);
                return "";
            }

            return result;
        }
    }
}
