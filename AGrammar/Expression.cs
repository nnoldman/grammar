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
        internal virtual bool FastMatch(int start, ref int offset)
        {
            int idx = start + offset;
            if (IsGrammarEnd(idx))
                return true;
            Token token = grammar.Tokens[idx];
            return (InvalidTokenType != tokenType && tokenType == token.WordType) || content == token.Word;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(content))
                return content;
            else
                return tokenType == Grammar.ID ? "ID" : tokenType.ToString();
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

            if ((InvalidTokenType != tokenType && tokenType == token.WordType) || content == token.Word)
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
