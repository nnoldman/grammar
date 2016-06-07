using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class AndExpression : CompositeExpression
    {
        public AndExpression()
        {
        }

        internal override void SetNext()
        {
            if (FirstSetted)
                return;

            base.SetNext();

            FirstSetted = true;

            for (int i = 0; i < this.children.Count; ++i)
            {
                var child = this.children[i];
                if (child != this)
                    child.SetNext();
            }
        }
        internal override bool Match(int start, ref int offset, GrammarTree parent, string propName)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx))
                return true;

            int eid = grammar.RequireErrorID();

            do
            {
                if (this.next && count == Count.Array && this.next.FastMatch(start, ref offset))
                    return true;

                if (mChildren.Count > 0)
                {
                    GrammarTree childProp = new GrammarTree();
                    childProp.propName = propName.Length > 0 ? propName : name;
                    for(int i=0;i<mChildren.Count;++i)
                    {
                        var child = mChildren[i];
                        if (!child.Match(start, ref offset, childProp, propName))
                        {
                            grammar.PushError(eid, grammar.Tokens[start + offset]);
                            return false;
                        }
                    }
                    parent.propertices.Add(childProp);
                }
                else
                {
                    Token token = grammar.Tokens[idx];
                    if (this.myToken.Match(token))
                    {
                        if (propName.Length > 0)
                        {
                            PropertyTreeNode prop = new PropertyTreeNode();
                            prop.propName = propName;
                            prop.content = token.Word;
                            parent.propertices.Add(prop);
                        }
                        offset++;
                    }
                    else
                    {
                        return false;
                    }
                }

            } while (this.next && count == Count.Array && !IsGrammarEnd(start + offset));

            grammar.PopError(eid);

            return true;
        }

        public CompositeExpression Is(params object[] para)
        {
            foreach (var arg in para)
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
        //internal AndExpression(string name)
        //{
        //    this.name = name;
        //}
        //internal AndExpression(int tokenID)
        //{
        //    this.tokenType = tokenID;
        //}

        internal override Expression GetNextSubling(Expression exp)
        {
            int idx = 0;
            for (; idx < children.Count; ++idx)
            {
                if (exp == mChildren[idx])
                    break;
            }
            if (idx < children.Count - 1)
            {
                return children[idx + 1];
            }
            return this.next;
        }

        public override void SaveTo(StringBuilder sb, int spaceCount)
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
                    child.SaveTo(sb, spaceCount);
                    ++idx;
                    if (idx < mChildren.Count)
                        sb.Append(" ");
                    else
                        break;
                } while (true);
            }
            sb.Append("}");
            spaceCount -= 4;
        }
        //public override Expression Copy()
        //{
        //    AndExpression exp = new AndExpression();
        //    this.children.ForEach((child) =>
        //    {
        //        var newchild = child.Copy();
        //        newchild.parent = exp;
        //        exp.children.Add(newchild);
        //    });
        //    exp.name = this.name;
        //    exp.myToken = this.myToken;
        //    exp.count = this.count;
        //    exp.grammar = this.grammar;
        //    return exp;
        //}
    }
}
