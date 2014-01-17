using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dand.RunTime
{
    public class IfCondStatement : IStatement
    {
        StatementList ifBranchStatements = null;
        StatementList elseBranchStatements = null;
        Expression conditionExpr = null;

        bool hasElseBranch = false;
        /// <summary>
        /// Property indicating wether ther is an else branch.
        /// </summary>
        public bool HasElseBranch
        {
            get { return this.hasElseBranch; }
            set { this.hasElseBranch = value; }
        }

        public IfCondStatement(Expression condition, StatementList ifBranch,
                            StatementList elseBranch)
        {
            if (condition == null)
            {
                throw new ArgumentException("Invalid condition for IF statement.");
            }
            else
            {
                this.conditionExpr = condition;
            }

            this.ifBranchStatements = ifBranch;
            this.elseBranchStatements = elseBranch;
        }

        #region IStatement Members
        public string Translate(int currentLocation = -1)
        {
            string result;

            string end = IdentGenerator.GetIdent();
            string els = IdentGenerator.GetIdent();

            result = string.Format("{0}JZ {1}\n{2}JMP {3}\n{1}:{4}{3}:NOP\n", conditionExpr.Translate(currentLocation), els, ifBranchStatements.Translate(currentLocation), end, elseBranchStatements.Translate(currentLocation));

            return result;
        }
        #endregion
    }
}
