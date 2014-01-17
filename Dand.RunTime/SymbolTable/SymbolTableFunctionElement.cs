using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class SymbolTableFunctionElement : SymbolTableElement
    {

        private SortedSet<string> allLocals = new SortedSet<string>();
        private List<LocalVariable> arguments = new List<LocalVariable>();
        private List<LocalVariable> locals = new List<LocalVariable>();

        public SymbolTableFunctionElement(string name)
            : base(name)
        {
            
        }

        public List<LocalVariable> Arguments
        {
            get { return locals; }
        }

        public int ArgumentsCount
        {
            get { return arguments.Count; }
            
        }

        public int LocalsCount
        {
            get { return locals.Count;  }
        }


        public void addArgs(LocalVariable var)
        {
            if(allLocals.Contains(var.Name))
                throw new ApplicationException("Variable with name " + var.Name + " has been already defined");
            allLocals.Add(var.Name);
            arguments.Add(var);
        }

        public void addLocals(LocalVariable var)
        {
            if(allLocals.Contains(var.Name))
                throw new ApplicationException("Variable with name " + var.Name + " has been already defined");
            allLocals.Add(var.Name);
            locals.Add(var);
        }

        public int checkInLocals(string name)
        {
            int index = 0;
            foreach (LocalVariable var in locals)
            {
                if (var.Name == name)
                    return index;
                ++index;
            }
            return -1;
        }

        public int checkInArgs(string name)
        {
            int index = 0;
            foreach (LocalVariable var in arguments)
            {
                if (var.Name == name)
                    return index;
                index++;
            }
            return -1;
        }
    }
}
