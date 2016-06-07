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
        /// <summary>
        /// use .Array() if it may be array!
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public OrExpression IsOneOf(params object[] args)
        {
            foreach (var arg in args)
            {
                if (arg == null)
                {
                    grammar.Error(name + ": Arg can not be null");
                    return null;
                }

                var tp = arg.GetType();

                grammar.Create(arg, this);
            }
            return this;
        }
        public static OrExpression operator |(OrExpression rhs, string tokenid)
        {
            rhs.grammar.Create(tokenid, rhs);
            return rhs;
        }
        public static OrExpression operator |(OrExpression rhs, Expression tokenid)
        {
            rhs.grammar.Create(tokenid, rhs);
            return rhs;
        }
        public static OrExpression operator |(OrExpression rhs, int tokenid)
        {
            rhs.grammar.Create(tokenid, rhs);
            return rhs;
        }
        public static OrExpression operator |(OrExpression rhs, Arg.ArgAnd tokenid)
        {
            rhs.grammar.Create(tokenid, rhs);
            return rhs;
        }
        public static OrExpression operator |(OrExpression rhs, Arg.ArgProp tokenid)
        {
            rhs.grammar.Create(tokenid, rhs);
            return rhs;
        }
        public override string ToString()
        {
            if (!string.IsNullOrEmpty(name))
                return name;
            StringBuilder sb = new StringBuilder();
            //sb.Append("<");
            for (int i = 0; i < sublings.Count; ++i)
            {
                var subling = sublings[i];
                sb.Append(subling.ToString());
                if (i == sublings.Count - 1)
                    break;
                else
                    sb.Append(" | ");
            }
            //sb.Append(">");
            return sb.ToString();
        }

        internal override bool Match(int start, ref int offset, GrammarTree parent, string propName)
        {
            if (start + offset == grammar.Tokens.Count)
                return true;

            int eid = grammar.RequireErrorID();

            do
            {
                if (this.next && count == Count.Array && this.next.FastMatch(start, ref offset))
                    return true;

                if (!MatchOne(start, ref offset, parent, propName))
                {
                    grammar.PushError(eid, grammar.Tokens[start + offset]);
                    return false;
                }


            } while (this.next && count == Count.Array && !IsGrammarEnd(start + offset));

            grammar.PopError(eid);

            return true;
        }
        bool MatchOne(int start, ref int offset, GrammarTree parent, string propName)
        {
            for (int i = 0; i < sublings.Count; ++i)
            {
                var sub = sublings[i];

                int thisOffset = offset;

                if (sub.Match(start, ref thisOffset, parent, propName))
                {
                    offset = thisOffset;
                    return true;
                }
            }
            return false;
        }
        internal override void SetNext()
        {
            if (FirstSetted)
                return;
            base.SetNext();
            FirstSetted = true;

            this.sublings.Reverse();

            for (int i = 0; i < this.sublings.Count; ++i)
            {
                var child = sublings[i];
                child.next = this.next;
                child.SetNext();
            }
        }

        internal override Expression GetNextSubling(Expression exp)
        {
            if (parent)
            {
                var pparent = parent.parent;
                if (pparent)
                    return pparent.GetNextSubling(parent);
            }
            return null;
        }

        //public override Expression Copy()
        //{
        //    OrExpression exp = new OrExpression();
        //    exp.name = this.name;
        //    exp.myToken = this.myToken;
        //    exp.count = this.count;
        //    exp.grammar = this.grammar;

        //    this.children.ForEach((child) =>
        //    {
        //        var newchild = child.Copy();
        //        newchild.parent = exp;
        //        exp.children.Add(newchild);
        //    });
        //    return exp;
        //}

        internal override bool FastMatch(int start, ref int offset)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx))
                return true;
            for (int i = 0; i < sublings.Count; ++i)
            {
                var sub = sublings[i];
                if (sub.FastMatch(start, ref offset))
                    return true;
            }
            return false;
        }

        public override void SaveTo(StringBuilder sb,int spaceCount)
        {
            sb.AppendLine(new string(' ', spaceCount) + this.ToString());
            sb.AppendLine(new string(' ', spaceCount) + "{");
            spaceCount += 4;

            int idx = 0;
            Expression child = null;
            if (mChildren.Count > 0)
            {
                do
                {
                    child = mChildren[idx];
                    sb.Append(child.ToString());
                    ++idx;
                    if (idx < mChildren.Count)
                        sb.Append(" | ");
                    else
                        break;
                } while (true);
            }

            sb.Append("}");
            spaceCount -= 4;
        }
    }
}
