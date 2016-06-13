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
    }
}
