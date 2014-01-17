using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class SymbolTableStringElement : SymbolTableElement
    {
        #region Constructor

        public SymbolTableStringElement(string name, string value)
            : base(name)
        {
            _value = value;
        }

        #endregion

        #region Private Fields

        private string _value;

        #endregion

        #region Public Properties

        public string Value
        {
            get { return _value; }
            set
            {
                _value = value;
                _isInit = true;
            }
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            if (IsInit)
            {
                return _name + " " + _value.ToString();
            }
            else
            {
                return _name + " Not Initialized";
            }
        }

        #endregion
    }
}
