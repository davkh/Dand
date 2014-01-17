using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class CallArgList : IStatement
    {
        List<CallArg> argList;
        public CallArgList()
        {
            argList = new List<CallArg>();
        }

        public void add(IStatement arg)
        {
            if (arg is CallArg)
            {
                argList.Add((CallArg)arg);
            }
        }

        public int getArgumentsCount()
        {
            return argList.Count;
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "";
            foreach (CallArg arg in argList)
            {
               result += arg.Translate(currentLocation);
            }
            return result;
        }
    }
}
