using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class OrExpression : CompositeExpression
    {
        public OrExpression()
        {

        }
        public List<Expression> sublings
        {
            get
            {
                return mChildren;
            }
        }
        public OrExpression IsOneOf(params object[] args)
        {
            throw new Exception();
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < sublings.Count; ++i)
            {
                var subling = sublings[i];
                sb.Append(subling.ToString());
                if (i == sublings.Count - 1)
                    break;
                else
                    sb.Append(" | ");
            }
            sb.Append(">");
            return sb.ToString();
        }

        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            if (start + offset == tokens.Count)
                return true;

            for (int i = 0; i < sublings.Count; ++i)
            {
                var sub = sublings[i];
                if (sub.Match(ref tokens, start, ref offset, parent, propName))
                    return true;
            }
            return false;
        }

        internal override void SetNext()
        {
            base.SetNext();

            if (this.next)
            {
                for (int i = 0; i < this.sublings.Count; ++i)
                {
                    var child = sublings[i];
                    child.next = this.next;
                    child.parent = this.parent;
                    child.SetNext();
                }
            }
        }

        internal override Expression GetNextSubling(Expression exp)
        {
            throw new Exception("The method or operation is not implemented.");
        }
    }

}
