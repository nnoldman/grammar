using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Helper
    {
        internal static bool IsDigital(char ch)
        {
            return ch <= '9' && ch >= '0';
        }

        internal static bool IsLetter(char ch)
        {
            return (ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z') || ch == '_';
        }

        internal static bool IsIn(char ch, params char[] chars)
        {
            for (int i = 0; i < chars.Length; ++i)
                if (ch == chars[i])
                    return true;
            return false;
        }
        internal static bool IsTerminal(char ch)
        {
            if (Helper.IsDigital(ch) || Helper.IsLetter(ch))
                return false;
            return true;
        }
        internal static bool IsTerminal(string content, int index)
        {
            char ch = content[index];
            if (Helper.IsDigital(ch) || Helper.IsLetter(ch))
                return false;
            return true;
        }
    }
}
