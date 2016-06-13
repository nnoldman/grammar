using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class Production : BoolObject
    {
        protected bool barray = false;
        internal Node2 node;
        protected ProductionOfOr followSet;

        public Production Array()
        {
            barray = true;
            return this;
        }

        public Production Node()
        {
            if (!node)
                node = new Node2();
            return this;
        }

        public Production Node(string name)
        {
            if (!node)
            {
                node = new Node2();
                node.name = name;
            }
            return this;
        }

        public virtual Production Copy()
        {
            throw new Exception();
        }

        public virtual void Add(Production rhs)
        {
            throw new Exception();
        }

        internal virtual bool Match(List<Token> tokens, ref int n, GrammarTree parentTree)
        {
            throw new Exception();
        }

        public static Production operator |(string content, Production rhs)
        {
            ProductionOfOr parent = new ProductionOfOr();
            parent.Add(new ProductionOfString() { content = content });
            parent.Add(rhs);
            return parent;
        }

        public static Production operator +(string content, Production rhs)
        {
            ProductionOfAnd parent = new ProductionOfAnd();
            parent.Add(new ProductionOfString() { content = content });
            parent.Add(rhs);
            return parent;
        }

        public static Production operator |(int tokenID, Production rhs)
        {
            ProductionOfOr parent = new ProductionOfOr();
            parent.Add(new ProductionOfInt() { tokenid = tokenID });
            parent.Add(rhs);
            return parent;
        }

        public static Production operator |(Production lhs, int tokenID)
        {
            ProductionOfOr parent = new ProductionOfOr();
            parent.Add(lhs);
            parent.Add(new ProductionOfInt() { tokenid = tokenID });
            return parent;
        }

        public static Production operator |(Production lhs, string content)
        {
            ProductionOfOr parent = new ProductionOfOr();
            parent.Add(lhs);
            parent.Add(new ProductionOfString() { content = content });
            return parent;
        }
        
        public static Production operator |(Production lhs, Production rhs)
        {
            ProductionOfOr parent = new ProductionOfOr();
            parent.Add(lhs);
            parent.Add(rhs);
            return parent;
        }

        public static Production operator +(Production lhs, int tokenID)
        {
            ProductionOfAnd parent = new ProductionOfAnd();
            parent.Add(lhs);
            parent.Add(new ProductionOfInt() { tokenid = tokenID });
            return parent;
        }

        public static Production operator +(Production lhs, string content)
        {
            ProductionOfAnd parent = new ProductionOfAnd();
            parent.Add(lhs);
            parent.Add(new ProductionOfString() { content = content });
            return parent;
        }

        public static Production operator +(Production lhs, Production rhs)
        {
            ProductionOfAnd parent = new ProductionOfAnd();
            parent.Add(lhs);
            parent.Add(rhs);
            return parent;
        }
    }
}
