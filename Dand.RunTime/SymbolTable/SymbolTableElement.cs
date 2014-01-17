using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class SymbolTableElement
    {
        #region Constructor

        public SymbolTableElement(string name)
        {
            this._name = name;
        }

        #endregion

        #region Private and Protected Fields

        protected string _name;

        protected bool _isInit;

        #endregion

        #region Public Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public bool IsInit
        {
            get { return _isInit; }
        }

        #endregion
    }
}
