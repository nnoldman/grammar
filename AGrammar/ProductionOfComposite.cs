using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class ProductionOfComposite : Production
    {
        public ProductionOfComposite()
        {
        }

        public string name;

        public List<Production> children = new List<Production>();

        protected void CopyChildrenTo(ProductionOfComposite p)
        {
            children.ForEach((item) => p.children.Add(item.Copy()));
        }
        public override Production Copy()
        {
            throw new Exception();
        }
    }
}
