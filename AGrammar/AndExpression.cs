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
            base.SetNext();

            for (int i = 0; i < this.children.Count; ++i)
            {
                var child = this.children[i];
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
                    foreach (var child in mChildren)
                    {
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

        public override string ToString()
        {
            return name;
        }

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

        public override Expression Copy()
        {
            AndExpression exp = new AndExpression();
            this.children.ForEach((child) =>
            {
                var newchild = child.Copy();
                newchild.parent = exp;
                exp.children.Add(newchild);
            });
            exp.name = this.name;
            exp.myToken = this.myToken;
            exp.count = this.count;
            exp.grammar = this.grammar;
            return exp;
        }
    }
}
