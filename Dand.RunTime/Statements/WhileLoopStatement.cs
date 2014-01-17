using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dand.RunTime;
 
namespace Dand.RunTime
{
    public class WhileLoopStatement: IStatement
    {
        private Expression condition = null;
        private StatementList whileBody = null;

        public WhileLoopStatement(Expression cond, StatementList body)
        {
            if (cond == null || body == null)
            {
                throw new ArgumentException("Null argument(s) not valid for While statement.");
            }
            this.condition = cond;
            this.whileBody = body;
        }

        public string Translate(int currentLocation = -1)
        {
            string beg = IdentGenerator.GetIdent();
            string end = IdentGenerator.GetIdent();

            string result = string.Format("{0}:{1}JZ {2}\n{3}JMP {0}\n{2}:NOP\n", beg, condition.Translate(currentLocation), end, whileBody.Translate(currentLocation));

            return result;
        }
    }
}
