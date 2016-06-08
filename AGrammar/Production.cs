using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class Node : BoolObject
    {
        public string name = string.Empty;
    }
    public class Production : BoolObject
    {
        protected bool barray = false;
        protected Node node;

        public Production Array()
        {
            barray = true;
            return this;
        }

        public Production Node()
        {
            if (!node)
                node = new Node();
            return this;
        }

        public Production Node(string name)
        {
            if (!node)
            {
                node = new Node();
                node.name = name;
            }
            return this;
        }

        public virtual Production Copy()
        {
            throw new Exception();
        }

        protected virtual PruductionOfOr GetOr()
        {
            throw new Exception();
        }

        protected virtual ProductionOfAnd GetAnd()
        {
            throw new Exception();
        }

        public virtual void Add(Production rhs)
        {
            if (GetOr())
            {
                if (rhs.GetOr())
                    GetOr().children.AddRange(rhs.GetOr().children);
                else
                    GetOr().children.Add(rhs);
            }
            else if (GetAnd())
            {
                if (rhs.GetAnd())
                    GetAnd().children.AddRange(rhs.GetAnd().children);
                else
                    GetAnd().children.Add(rhs);
            }
            else
            {
                throw new Exception();
            }
        }

        internal virtual int Match(List<Token> tokens, int s, GrammarTree parentTree)
        {
            throw new Exception();
        }



        public static Production operator |(string content, Production rhs)
        {
            PruductionOfOr parent = new PruductionOfOr();
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
            PruductionOfOr parent = new PruductionOfOr();
            parent.Add(new ProductionOfInt() { tokenid = tokenID });
            parent.Add(rhs);
            return parent;
        }

        public static Production operator |(Production lhs, int tokenID)
        {
            PruductionOfOr parent = new PruductionOfOr();
            parent.Add(lhs);
            parent.Add(new ProductionOfInt() { tokenid = tokenID });
            return parent;
        }

        public static Production operator |(Production lhs, string content)
        {
            PruductionOfOr parent = new PruductionOfOr();
            parent.Add(lhs);
            parent.Add(new ProductionOfString() { content = content });
            return parent;
        }
        
        public static Production operator |(Production lhs, Production rhs)
        {
            PruductionOfOr parent = new PruductionOfOr();
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
