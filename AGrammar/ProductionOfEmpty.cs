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

        protected override PruductionOfOr GetOr()
        {
            return null;
        }

        protected override ProductionOfAnd GetAnd()
        {
            return null;
        }

        public override string ToString()
        {
            return "Empty";
        }
    }
}
