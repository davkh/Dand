using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class SymbolTable
    {
        #region Private Fields

        private Dictionary<int, SymbolTableElement> symbolTable;
        private Dictionary<string, int> symbolNames;
        private int counter = 1;
        private static SymbolTable uniqueInstance;

        #endregion

        private SymbolTable()
        {
            symbolTable = new Dictionary<int, SymbolTableElement>();
            symbolNames = new Dictionary<string, int>();
        }

        #region Public Methods

        public static SymbolTable GetInstance
        {
            get
            {
                if (uniqueInstance == null)
                {
                    uniqueInstance = new SymbolTable();
                }
                return uniqueInstance;
            }
        }

        public int Add(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException();
            }
            if (symbolNames.ContainsKey(name))
            {
                throw new ApplicationException("Variable with name " + name + " has been already defined");
            }
            SymbolTableElement x = new SymbolTableElement(name);
            symbolTable.Add(counter, x);
            symbolNames.Add(name, counter);
            ++counter;
            return counter - 1;
        }

        public void SetType(int id, DandTypes type)
        {
            if (!symbolTable.ContainsKey(id))
            {
                throw new ArgumentException("Undeclared identifier.");
            }
            SymbolTableElement x = symbolTable[id];
            switch (type)
            {
                case DandTypes.Integer:
                    SymbolTableIntegerElement stie = new SymbolTableIntegerElement(x.Name, 0);
                    symbolTable[id] = stie;
                    break;
                case  DandTypes.Double:
                    SymbolTableDoubleElement stde = new SymbolTableDoubleElement(x.Name, 0.0);
                    symbolTable[id] = stde;
                    break;
                case DandTypes.Boolean:
                    SymbolTableBoolElement stbe = new SymbolTableBoolElement(x.Name, false);
                    symbolTable[id] = stbe;
                    break;
                case DandTypes.String:
                    SymbolTableStringElement stse = new SymbolTableStringElement(x.Name, "");
                    symbolTable[id] = stse;
                    break;
                case DandTypes.Function:
                    SymbolTableFunctionElement stfe = new SymbolTableFunctionElement(x.Name);
                    symbolTable[id] = stfe;
                    break;
            }
        }

        public SymbolTableElement Get(string name)
        {
            int id = symbolNames[name];
            return symbolTable[id];
        }

        public SymbolTableElement Get(int id)
        {
            return symbolTable[id];
        }

        public string GetName(int id)
        {
            return symbolTable[id].Name;
        }

        public int GetID(string name)
        {
            int id = symbolNames[name];
            return id;
        }

        public bool contains(string name)
        {
            return symbolNames.ContainsKey(name);
        }

        public void Clear()
        {
            uniqueInstance = new SymbolTable();
        }

        #endregion
    }
}
