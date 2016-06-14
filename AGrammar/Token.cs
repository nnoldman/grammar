using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Token : BoolObject
    {
        public string Word;
        public int Line;
        public int Column;

        public override string ToString()
        {
            return Word.ToString();
        }

        public string Error()
        {
            return string.Format("Error:(Line:{0},Col:{1})", Line, Column);
        }
    }

    internal class NumberToken : Token
    {
    }
    internal class IntToken : NumberToken
    {
        public int value;

        public override string ToString()
        {
            return "int:" + value.ToString();
        }
    }
    internal class FloatToken : NumberToken
    {
        public float value;

        public override string ToString()
        {
            return "float:" + value.ToString();
        }
    }

    internal class DoubleToken : NumberToken
    {
        public double value;

        public override string ToString()
        {
            return "double:" + value.ToString();
        }
    }

    internal class WordToken : Token
    {
        public string lexeme = string.Empty;

        public override string ToString()
        {
            return this.lexeme;
        }
    }
    internal class EofToken : Token
    {
        internal EofToken()
        {
            Word = Grammar.Eof;
        }
    }
    internal class TerminalToken : Token
    {
    }
    internal class LineFeedToken : TerminalToken
    {
        internal LineFeedToken()
        {
            Word = Grammar.LineFeed;
        }

        public override string ToString()
        {
            return "LF";
        }
    }

 
    internal class KeyWordToken : Token
    {
        public int Tag;
    }
    internal class IDToken : Token
    {

    }
}
