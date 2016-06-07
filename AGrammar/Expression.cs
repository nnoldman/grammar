using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AGrammar
{
    public enum Count
    {
        One,
        Array,
    }
    public enum TokenUsage
    {
        None,
        Type,
        Content,
    }

    public class EasyToken
    {
        public string content
        {
            get
            {
                return mContent;
            }
            set
            {
                mContent = value;
                mUsage = TokenUsage.Content;
            }
        }
        public int type
        {
            get
            {
                return mType;
            }
            set
            {
                mType = value;
                mUsage = TokenUsage.Type;
            }
        }
        public TokenUsage usage
        {
            get
            {
                return mUsage;
            }
        }
        internal bool Match(Token token)
        {
            return mUsage == TokenUsage.Type ? mType == token.WordType : mContent == token.Word;
        }
        int mType = Grammar.InvalidTokenType;
        TokenUsage mUsage = TokenUsage.None;
        string mContent;
    }

    public class Expression : BoolObject
    {
        public EasyToken myToken = new EasyToken();
        public CompositeExpression parent;
        public Expression next;
        public Grammar grammar;
        public Count count = Count.One;

        public virtual void SaveTo(StringBuilder sb,int spaceCount)
        {
            sb.AppendLine(new string(' ',spaceCount));
            sb.Append(this.ToString());
        }

        internal Expression()
        {

        }

        //public  Expression Copy()
        //{
        //    Expression exp = new Expression();
        //    exp.myToken = this.myToken;
        //    exp.count = this.count;
        //    exp.grammar = this.grammar;
        //    return exp;
        //}

        protected bool FirstSetted = false;
        internal virtual void SetNext()
        {
            if (FirstSetted)
                return;

            if (parent && !next)
                this.next = parent.GetNextSubling(this);
        }

        internal virtual bool FastMatch(int start, ref int offset)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx))
                return true;
            Token token = grammar.Tokens[idx];
            return this.myToken.Match(token);
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(myToken.content))
                return myToken.content;
            else
                return myToken.type == Grammar.ID ? "ID" : myToken.type.ToString();
        }
        internal bool IsGrammarEnd(int idx)
        {
            return idx == grammar.Tokens.Count;
        }
        internal virtual bool Match(int start, ref int offset, GrammarTree parent, string propName)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx))
                return true;

            Token token = grammar.Tokens[idx];

            if (this.myToken.Match(token))
            {
                if (!string.IsNullOrEmpty(propName))
                {
                    PropertyTreeNode prop = new PropertyTreeNode();
                    prop.content = token.Word;
                    prop.propName = propName;
                    parent.propertices.Add(prop);
                }
                offset++;
                return true;
            }
            return false;
        }
    }
}
