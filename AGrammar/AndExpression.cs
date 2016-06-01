using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
  public class AndExpression : CompositeExpression
    {
        public string name = string.Empty;
        public List<Expression> children
        {
            get
            {
                return mChildren;
            }
        }
        public AndExpression()
        {

        }



        internal override void SetNext()
        {
            base.SetNext();

            for (int i = 0; i < mChildren.Count; ++i)
            {
                var child = mChildren[i];
                child.SetNext();
            }
        }
        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            int idx = start + offset;
            if (idx == tokens.Count)
                return true;

            if (countType == CountType.Array)
            {
                do
                {
                    if (!next)
                        return true;
                    if (next.FastMatch(start, ref offset, ref tokens))
                        return true;
                    if (mChildren.Count > 0)
                    {
                        GrammarTree childProp = new GrammarTree();
                        childProp.segType = name;
                        childProp.propName = propName.Length > 0 ? propName : name;
                        foreach (var child in mChildren)
                        {
                            if (!child.Match(ref tokens, start, ref offset, childProp, propName))
                            {
                                childProp.propertices.Clear();
                                return false;
                            }
                        }
                        if (parent)
                            parent.propertices.Add(childProp);
                        //return true;
                    }
                    else
                    {
                        Token token = tokens[idx];
                        if (token.TokenType == tokenType || token.Content == content)
                        {
                            if (propName.Length > 0)
                            {
                                PropertyTreeNode prop = new PropertyTreeNode();
                                prop.propName = propName;
                                prop.content = token.Content;
                                parent.propertices.Add(prop);
                            }
                            offset++;
                            return true;
                        }
                    }
                } while (true);
            }
            else if (countType == CountType.One)
            {
                if (mChildren.Count > 0)
                {
                    GrammarTree childProp = new GrammarTree();
                    childProp.segType = name;
                    childProp.propName = propName.Length > 0 ? propName : name;
                    foreach (var child in mChildren)
                    {
                        if (!child.Match(ref tokens, start, ref offset, childProp, propName))
                        {
                            return false;
                        }
                    }
                    parent.propertices.Add(childProp);
                    return true;
                }
                else
                {
                    Token token = tokens[idx];
                    if (token.TokenType == tokenType || token.Content == content)
                    {
                        if (propName.Length > 0)
                        {
                            PropertyTreeNode prop = new PropertyTreeNode();
                            prop.propName = propName;
                            prop.content = token.Content;
                            parent.propertices.Add(prop);
                        }
                        offset++;
                        return true;
                    }
                }
            }
            return false;
        }


        public AndExpression Array()
        {
            countType = CountType.Array;
            return this;
        }

        public AndExpression Is(params object[] para)
        {
            foreach (var arg in para)
            {
                if (arg == null)
                {
                    grammar.Error(name + ": Arg can not null");
                    return null;
                }
                var tp = arg.GetType();
                grammar.Create(arg, this);
            }
            return this;
        }
        internal AndExpression(string name)
        {
            this.name = name;
        }
        internal AndExpression(int tokenID)
        {
            this.tokenType = tokenID;
        }

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
            return null;
        }
    }

}
