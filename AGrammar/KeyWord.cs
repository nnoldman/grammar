using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class KeyWord
    {
        public int WordType;
        public string Word;

        public override string ToString()
        {
            return string.Format("Keyword:({0}) {1}", WordType, Word);
        }
    }
}
