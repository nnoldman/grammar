using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Token
    {
        public int tag;

        public override string ToString()
        {
            return tag.ToString();
        }
    }
    internal class NumberToken:Token
    {
        public int value;

        public override string ToString()
        {
            return value.ToString();
        }
    }
    internal class FloatToken : Token
    {
        public float value;

        public override string ToString()
        {
            return value.ToString();
        }
    }
    internal class WordToken:Token
    {
        public string lexeme = string.Empty;

        public override string ToString()
        {
            return this.lexeme;
        }
    }
}
