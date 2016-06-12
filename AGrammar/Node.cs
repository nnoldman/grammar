using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Node
    {
        public int line;

        public void Error(string text)
        {
            throw new Exception(string.Format("Error({0})=>{1}", line, text));
        }
    }
}
