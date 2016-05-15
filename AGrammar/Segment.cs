using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AGrammar
{
    public enum CountType
    {
        Zero = 1 << 0,
        One = 1 << 1,
        Array = 1 << 2 & One,
    }

    public class Expression : BoolObject
    {
        public int tokenType = -1;
        public string content = string.Empty;
        public CountType countType = CountType.One;
        public Segment parent;
        public Expression endExpression;
        internal Expression()
        {

        }
        internal virtual void Process()
        {

        }
        internal virtual bool FastMatch(int start, ref int offset, ref List<Token> tokens)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx, ref tokens))
                return true;
            Token token = tokens[idx];
            return tokenType == token.TokenType || content == token.Content;
        }

        public override string ToString()
        {
            return tokenType == Grammar.ID && string.IsNullOrEmpty(content) ? "ID" : content;
        }
        internal bool IsGrammarEnd(int idx, ref List<Token> tokens)
        {
            return idx == tokens.Count;
        }
        internal virtual bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx, ref tokens))
                return true;
            Token token = tokens[idx];
            if (tokenType == token.TokenType || content == token.Content)
            {
                if (!string.IsNullOrEmpty(propName))
                {
                    PropertyTreeNode prop = new PropertyTreeNode();
                    prop.content = token.Content;
                    prop.propName = propName;
                    parent.propertices.Add(prop);
                }
                offset++;
                return true;
            }
            return false;
        }
    }
    public class EmptyExp : Expression
    {
        public override string ToString()
        {
            return "Empty";
        }

        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            if (!endExpression)
                return true;
            int idx = start + offset;
            if (idx <= tokens.Count - 1)
            {
                if (endExpression.FastMatch(start, ref offset, ref tokens))
                    return true;
            }
            return false;
        }

        internal override void Process()
        {
            if (parent)
                this.endExpression = parent.GetNextSubling(this);
        }

    }
    public class Segment : Expression
    {
        public string name = string.Empty;

        public Grammar grammar;

        internal void AddChildren(Expression exp)
        {
            children.Add(exp);
            exp.parent = this;
        }

        internal Expression GetNextSubling(Expression exp)
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
            if (parent)
                return parent.GetNextSubling(this);
            return null;
        }
        internal override void Process()
        {
            if (countType == CountType.Array)
            {
                this.endExpression = GetNextSubling(this);
            }
            for (int i = 0; i < mChildren.Count; ++i)
            {
                var child = mChildren[i];
                child.Process();
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
                    if (!endExpression)
                        return true;
                    if (endExpression.FastMatch(start, ref offset, ref tokens))
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

        public List<Expression> children
        {
            get
            {
                return mChildren;
            }
        }

        List<Expression> mChildren = new List<Expression>();

        internal Segment()
        {

        }
        public Segment Array()
        {
            countType = CountType.Array;
            return this;
        }

        public Segment Is(params object[] para)
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
        internal Segment(string name)
        {
            this.name = name;
        }
        internal Segment(int tokenID)
        {
            this.tokenType = tokenID;
        }

        public override string ToString()
        {
            return name;
        }
    }

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
    }
    public class OrExpression : Expression
    {
        List<Expression> mSublings = new List<Expression>();
        public List<Expression> sublings
        {
            get
            {
                return mSublings;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<");
            for (int i = 0; i < mSublings.Count; ++i)
            {
                var subling = mSublings[i];
                sb.Append(subling.ToString());
                if (i == mSublings.Count - 1)
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

            for (int i = 0; i < mSublings.Count; ++i)
            {
                var sub = mSublings[i];
                if (sub.Match(ref tokens, start, ref offset, parent, propName))
                    return true;
            }
            return false;
        }

        internal override void Process()
        {
            for (int i = 0; i < mSublings.Count; ++i)
            {
                var child = mSublings[i];
                child.parent = this.parent;
                child.Process();
            }
        }
    }
}
