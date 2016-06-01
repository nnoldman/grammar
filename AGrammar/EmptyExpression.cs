﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class EmptyExpression : Expression
    {
        public override string ToString()
        {
            return "Empty";
        }

        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            if (!next)
                return true;
            int idx = start + offset;
            if (idx <= tokens.Count - 1)
            {
                if (next.FastMatch(start, ref offset, ref tokens))
                    return true;
            }
            return false;
        }
    }
  
}