using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class ArgumentList : IStatement
    {
        private List<LocalVariable> argumentList =  new List<LocalVariable>();

        public ArgumentList()
        {
        }

        public void insert(IStatement var)
        {
            argumentList.Add((LocalVariable)var);
        }

        public List<LocalVariable> Arguments
        {
            get { return argumentList;  }
        }

        public string Translate(int currentLocation = -1)
        {
            string result = "argument list translate n .........";

            return result;
        }

    }
}
