using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class EmptyExpression : Expression
    {
        public override string ToString()
        {
            return "Empty";
        }

        internal override bool Match(int start, ref int offset, GrammarTree parent, string propName)
        {
            if (!next)
                return true;
            int idx = start + offset;
            if (idx <= grammar.Tokens.Count - 1)
            {
                if (next.FastMatch(start, ref offset))
                    return true;
            }
            return false;
        }

        public override Expression Copy()
        {
            EmptyExpression exp = new EmptyExpression();
            exp.grammar = this.grammar;
            exp.parent = this.parent;
            return exp;
        }
    }

}
