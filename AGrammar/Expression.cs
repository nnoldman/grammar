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
        public int tokenType = -1;
        public string content = string.Empty;
        public CountType countType = CountType.One;
        public CompositeExpression parent;
        public Expression next;
        public Grammar grammar;

        internal Expression()
        {

        }
        internal virtual void SetNext()
        {
            if (parent)
                this.next = parent.GetNextSubling(this);
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

    
   
    
}
