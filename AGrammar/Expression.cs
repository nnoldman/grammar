using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AGrammar
{
    public enum CountType
    {
        One,
        Array,
    }

    public class Expression : BoolObject
    {
        public static int InvalidTokenType = -1;
        public int tokenType = InvalidTokenType;
        public string content = string.Empty;
        public CompositeExpression parent;
        public Expression next;
        public Grammar grammar;
        public CountType countType = CountType.One;

        internal Expression()
        {

        }

        public virtual Expression Copy()
        {
            Expression exp = new Expression();
            exp.tokenType = this.tokenType;
            exp.content = this.content;
            exp.countType = this.countType;
            exp.grammar = this.grammar;
            return exp;
        }

        internal virtual void SetNext()
        {
            if (parent && !next)
                this.next = parent.GetNextSubling(this);
        }
        internal virtual bool FastMatch(int start, ref int offset, ref List<Token> tokens)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx, ref tokens))
                return true;
            Token token = tokens[idx];
            return (InvalidTokenType != tokenType && tokenType == token.TokenType) || content == token.Content;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(content))
                return content;
            else
                return tokenType == Grammar.ID ? "ID" : tokenType.ToString();
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

            if ((InvalidTokenType != tokenType && tokenType == token.TokenType) || content == token.Content)
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
}
