using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class OperationValidator
    {
        private static DandTypes[,] arithmeticOp = new DandTypes[4, 4] 
        {
            { DandTypes.Integer, DandTypes.Double, DandTypes.None,    DandTypes.None },
            { DandTypes.Double,  DandTypes.Double, DandTypes.None,    DandTypes.None },
            { DandTypes.None,    DandTypes.None,   DandTypes.Boolean, DandTypes.None }, 
            { DandTypes.None,    DandTypes.None,   DandTypes.None,    DandTypes.None }
        };

        private static DandTypes[,] logicaOp = new DandTypes[4, 4]
        {
            { DandTypes.Integer, DandTypes.Double, DandTypes.None,    DandTypes.None },
            { DandTypes.Double,  DandTypes.Double, DandTypes.None,    DandTypes.None },
            { DandTypes.None,    DandTypes.None,   DandTypes.Boolean, DandTypes.None }, 
            { DandTypes.None,    DandTypes.None,   DandTypes.None,    DandTypes.String }
        };


        public DandTypes CheckType(DandTypes leftType, DandTypes rigthType)
        {
            return arithmeticOp[(int)leftType, (int)rigthType];
        }

        public DandTypes CheckOperation(DandTypes leftType, DandTypes rigthType)
        {
            return logicaOp[(int)leftType, (int)rigthType];
        }
    }
}
