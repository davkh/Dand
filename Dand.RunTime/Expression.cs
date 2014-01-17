using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public enum Operation
    {
        Constant,
        SymbolElement,
        UnaryMinus,
        Sub,
        Add,
        Mul,
        Div,
        Modul,
        Great,
        Less,
        And,
        Or,
        Not,
        Equ,
        NotEqu,
        Lt,
        Gt,
        LtEq,
        GtEq
    }

    public class Expression
    {
        private object constValue;
        private DandTypes type;
        private Expression leftExpression;
        private Expression rightExpression;
        private Operation operation;
        private string variableName;
        private IStatement statement;

        public DandTypes Type
        {
            get { return type; }
        }

        public Operation Operation 
        { 
            get
            {
                return this.operation;
            }
        }

        public Expression(IStatement statement)
        {
            this.statement = statement;
        }

        public Expression(string name, bool xx)
        {
            this.operation = Operation.SymbolElement;
            this.variableName = name;
        }

        public Expression(long value)
        {
            this.operation = Operation.Constant;
            type = DandTypes.Integer;
            constValue = value;
        }

        public Expression(bool value)
        {
            this.operation = Operation.Constant;
            type = DandTypes.Boolean;
            constValue = value;
        }

        public Expression(double value)
        {
            this.operation = Operation.Constant;
            type = DandTypes.Double;
            constValue = value;
        }

        public Expression(string value)
        {
            this.operation = Operation.Constant;
            type = DandTypes.String;
            constValue = value;
        }

        /// <summary>
        /// Builds a complex expression.
        /// </summary>
        /// <param name="op">Operation</param>
        /// <param name="left">Left side expression.</param>
        /// <param name="right">Right side expression.</param>
        public Expression(Operation op, Expression left, Expression right)
        {
            #region Argument validation
            
            if (right == null)
            {
                throw new ArgumentNullException("Expression right handside is null.");
            }

            if (left == null)
            {
                // Left can be null only if the operation is unary minus.
                if (operation == Operation.UnaryMinus)
                {
                    switch (right.Type)
                    {
                        case DandTypes.Integer:
                            type = DandTypes.Integer;
                            break;

                        case DandTypes.Double:
                            type = DandTypes.Double;
                            break;

                        default:
                            throw new InvalidOperationException();
                    }
                }
                else
                {
                    throw new ArgumentNullException("");
                }
            }
            #endregion

            this.leftExpression = left;
            this.rightExpression = right;
            this.operation = op;

            #region Operation result type validation
            OperationValidator opValid = new OperationValidator();
            switch (operation)
            {
                case Operation.Add:
                case Operation.Sub:
                case Operation.Mul:
                case Operation.Div:
                case Operation.Modul:
                    //type = opValid.CheckType(left.Type, right.Type);
                    //if (type == DandTypes.None)
                    //{
                    //    throw new InvalidOperationException();
                    //}
                    break;

                case Operation.Great:
                case Operation.Less:
                case Operation.Equ:
                case Operation.NotEqu:
                    //type = opValid.CheckOperation(left.Type, right.Type);
                    //if (type == DandTypes.None)
                    //{
                    //    throw new InvalidOperationException();
                    //}
                    break;
            }
            #endregion
        }

        public string Translate(int currentLocation = -1)
        {
            if (statement != null)
                return statement.Translate(currentLocation);

           // return "strat expr translate" + Environment.NewLine;
            if (type != DandTypes.Integer && type != DandTypes.Boolean)
                return "";
            if (this.operation == RunTime.Operation.Constant)
            {
                if (this.type == DandTypes.Integer)
                    return "PUSH " + this.constValue.ToString() + Environment.NewLine;
                else if (this.type == DandTypes.Boolean)
                    return "PUSH " + ((bool)this.constValue ? 1 : 0).ToString() + Environment.NewLine;
            }
            else if (this.operation == RunTime.Operation.SymbolElement)
            {
                string resultName = "";
                if (currentLocation != -1)
                {
                    SymbolTableFunctionElement element = (SymbolTableFunctionElement)SymbolTable.GetInstance.Get(currentLocation);
                    int position = element.checkInArgs(this.variableName);
                    if (-1 != position)
                    {
                        resultName = "fp+" + (position + 1).ToString();
                    }
                    else
                    {
                        if (-1 != element.checkInLocals(this.variableName))
                        {
                            resultName = "fp+" + (element.ArgumentsCount + element.checkInLocals(this.variableName) + 1);
                        }
                        else if (SymbolTable.GetInstance.contains(this.variableName))
                        {
                            resultName = this.variableName;
                        }
                    }
                }

                else if (SymbolTable.GetInstance.contains(this.variableName))
                {
                    resultName = this.variableName;
                }
                if ("" == resultName)
                {
                    throw new Exception(string.Format("Variable {0} is not declared", this.variableName));
                }

     
                return "PUSH " + resultName + Environment.NewLine;
            }

            string result = "";

            if (leftExpression != null)
                result = result + leftExpression.Translate(currentLocation);

            if (rightExpression != null)
                result = result + rightExpression.Translate(currentLocation);

            switch (this.operation)
            {
                case RunTime.Operation.Add:
                    result = result + "ADD" + Environment.NewLine;
                    break;

                case RunTime.Operation.Mul:
                    result = result + "MUL" + Environment.NewLine;
                    break;

                case RunTime.Operation.Div:
                    result = result + "DIV" + Environment.NewLine;
                    break;

                case RunTime.Operation.Sub:
                    result = result + "SUB" + Environment.NewLine;
                    break;

                case RunTime.Operation.Modul:
                    result = result + "MOD" + Environment.NewLine;
                    break;

                case RunTime.Operation.And:
                    result = result + "AND" + Environment.NewLine;
                    break;

                case RunTime.Operation.Or:
                    result = result + "OR" + Environment.NewLine;
                    break;

                case RunTime.Operation.Not:
                    result = result + "NOT" + Environment.NewLine;
                    break;

                case RunTime.Operation.Great:
                case RunTime.Operation.Gt:
                    result = result + "GT" + Environment.NewLine;
                    break;

                case RunTime.Operation.Less:
                case RunTime.Operation.Lt:
                    result = result + "LT" + Environment.NewLine;
                    break;

                case RunTime.Operation.GtEq:
                    result = result + "GE" + Environment.NewLine;
                    break;

                case RunTime.Operation.LtEq:
                    result = result + "LE" + Environment.NewLine;
                    break;

                case RunTime.Operation.UnaryMinus:
                    result = result + "NEG" + Environment.NewLine;
                    break;
            }

            return result;
        }

        private object Abs(int p)
        {
            if (p < 0)
                return (-p);
            return p;
        }
    }
}
