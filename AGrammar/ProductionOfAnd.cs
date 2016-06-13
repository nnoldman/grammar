using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfAnd : ProductionOfComposite
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

        public override void Add(Production rhs)
        {
            var and = rhs as ProductionOfAnd;
            if (and)
                children.AddRange(and.children);
            else
                children.Add(rhs);
        }

        internal override bool Match(List<Token> tokens, ref int n, GrammarTree parentTree)
        {
            if (node)
                node.AddContent(parentTree);
            int offset = n;
            foreach (var child in children)
            {
                if (!child.Match(tokens, ref offset, parentTree))
                {
                    parentTree.Clear();
                    return false;
                }
            }
            n = offset;
            return true;
        }
    }
}
