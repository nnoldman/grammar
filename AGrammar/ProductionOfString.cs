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
