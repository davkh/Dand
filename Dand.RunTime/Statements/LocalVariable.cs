using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class LocalVariable : IStatement
    {
        private string m_name;
        private DandTypes m_type;
        public LocalVariable(string name, DandTypes type)
        {
            m_name = name;
            m_type = type;
        }

        public string Name
        {
            get { return m_name; }
        }

        public DandTypes Type
        {
            get { return m_type; }
        }

        public string Translate(int currentLocation = -1)
        {
            string resultName = m_name;
            if (currentLocation != -1)
            {
                resultName = SymbolTable.GetInstance.GetName(currentLocation) + "_" + m_name;
            }
            string result = "";//"DECL " + resultName + Environment.NewLine;
            return result;
        }
    }
}
