using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfInt : Production
    {
        public int tokenid;

        public override string ToString()
        {
            return tokenid == Grammar.ID ? "ID" : "ID" + tokenid.ToString();
        }

        public override Production Copy()
        {
            ProductionOfInt production = new ProductionOfInt();
            production.tokenid = this.tokenid;
            return production;
        }

        protected override PruductionOfOr GetOr()
        {
            return null;
        }

        protected override ProductionOfAnd GetAnd()
        {
            return null;
        }
    }
}
