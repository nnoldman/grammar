using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfEmpty : Production
    {
        public override Production Copy()
        {
            return new ProductionOfEmpty();
        }
        public override string ToString()
        {
            return "Empty";
        }

        internal override bool Match(List<Token> tokens, ref int n, GrammarTree parentTree)
        {
            return true;
        }
    }
}
