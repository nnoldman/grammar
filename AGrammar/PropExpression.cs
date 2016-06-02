using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class PropExpression : Expression
    {
        public string parentName = string.Empty;
        public string propertyName = string.Empty;
        public Expression executer;

        internal PropExpression(string propName, string parentName)
        {
            this.propertyName = propName;
            this.parentName = parentName;
        }
        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            if (executer.Match(ref tokens, start, ref offset, parent, this.propertyName))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return parentName + "." + propertyName;
        }

        internal override bool FastMatch(int start, ref int offset, ref List<Token> tokens)
        {
            if (executer.FastMatch(start, ref offset, ref tokens))
            {
                return true;
            }
            return false;
        }

        public override Expression Copy()
        {
            PropExpression exp = new PropExpression(this.propertyName, this.parentName);
            exp.tokenType = this.tokenType;
            exp.content = this.content;
            exp.countType = this.countType;
            exp.grammar = this.grammar;
            exp.executer = this.executer.Copy();
            return exp;
        }

        internal override void SetNext()
        {
            if (this.next)
                this.executer.next = this.next;
        }
    }
}
