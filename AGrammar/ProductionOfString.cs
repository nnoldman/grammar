using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfString : Production
    {
        public string content;

        public override string ToString()
        {
            return content;
        }

        public override Production Copy()
        {
            ProductionOfString production = new ProductionOfString();
            production.content = this.content;
            return production;
        }

        internal override bool Match(List<Token> tokens, ref int n, GrammarTree parentTree)
        {
            if (tokens[n] is EOFToken)
                return true;
            if (tokens[n].Word == content)
            {
                n++;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
