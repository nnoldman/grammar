using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Expr
    {
        public Token op;
        public AType type;
        public Expr Gen()
        {
            return this;
        }
        public Expr Reduce()
        {
            return this;
        }
    }
}
