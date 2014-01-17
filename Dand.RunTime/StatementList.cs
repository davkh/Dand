using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class StatementList : IStatement
    {
        public void Add(IStatement statement, bool need_to_check = false)
        {
            if (need_to_check)
            {
                if (statement is LocalVariable)
                {
                    locals.Add((LocalVariable)statement);
                }
            }
            statements.Add(statement);
        }

        public void InsertFront(IStatement statement, bool need_to_check = false)
        {
            if (need_to_check)
            {
                if (statement is LocalVariable)
                {
                    locals.Add((LocalVariable)statement);
                }
            }
            statements.Insert(0, statement);
        }

        #region IStatement Members

        public string Translate(int currentLocation = -1)
        {
            string result = "";
            if (statements.Count != 0)
            {
                foreach (IStatement st in statements)
                {
                    if (st != null)
                    {
                        if (st is FunctionReturn && currentLocation == -1)
                            continue;

                        result = result + st.Translate(currentLocation);
                    }
                }

                if (result == "")
                    result = "NOP\n";
            }

            return result;
        }

        public List<LocalVariable> Arguments
        {
            get { return locals; }
        }

        public bool ReturnExists(int currentLocation = - 1)
        {
            if (currentLocation == -1)
                return true;

            foreach (var st in statements)
                if (st is FunctionReturn)
                    return true;

            return false;
        }

        #endregion
        private List<LocalVariable> locals = new List<LocalVariable>();
        private List<IStatement> statements = new List<IStatement>();
    }
}
