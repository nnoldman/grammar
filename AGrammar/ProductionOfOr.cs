using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfOr : ProductionOfComposite
    {
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
            //            d += '|';
            //        ++i;
            //    }
            //    while (i < children.Count);
            //}
            //return d;
            return name;
        }

        public override Production Copy()
        {
            ProductionOfOr production = new ProductionOfOr();
            production.name = this.name;
            this.CopyChildrenTo(production);
            return production;
        }

     

        public override void Add(Production rhs)
        {
            ProductionOfOr or = rhs as ProductionOfOr;
            if (or)
                children.AddRange(or.children);
            else
                children.Add(rhs);
        }

        internal override bool Match(List<Token2> tokens, ref int n, GrammarTree parentTree)
        {
            foreach (var child in children)
            {
                int offset = n;
                if (!child.Match(tokens, ref offset, parentTree))
                {
                    n = offset;
                    return true;
                }
            }
            return false;
        }
    }
}
