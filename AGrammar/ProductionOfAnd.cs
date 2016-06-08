using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfAnd : ProductionOfComposite
    {
        protected override PruductionOfOr GetOr()
        {
            return null;
        }

        protected override ProductionOfAnd GetAnd()
        {
            return this;
        }

        public override string ToString()
        {
            //string d = name + ':';

            //if (children.Count > 0)
            //{
            //    int i = 0;
            //    do
            //    {
            //        d += children[i].ToString();
            //        if (i < children.Count)
            //            d += '+';
            //        ++i;
            //    }
            //    while (i < children.Count);
            //}

            //return d;
            return name;
        }

        public override Production Copy()
        {
            ProductionOfAnd production = new ProductionOfAnd();
            production.name = this.name;
            this.CopyChildrenTo(production);
            return production;
        }
    }
}
