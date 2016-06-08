using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public enum ItemType
    {
        None,
        And,
        Or,
    }
    public enum Count
    {
        One,
        Array,
    }
    public static class Factory
    {
        public static Production And(string name)
        {
            ProductionOfAnd production = new ProductionOfAnd();
            production.name = name;
            return production;
        }
        public static Production Or(string name)
        {
            ProductionOfOr production = new ProductionOfOr();
            production.name = name;
            return production;
        }
        public static Production Symbol(string name)
        {
            ProductionOfString production = new ProductionOfString();
            production.content = name;
            return production;
        }

        public static Production Symbol(int tokenid)
        {
            ProductionOfInt production = new ProductionOfInt();
            production.tokenid = tokenid;
            return production;
        }
    }
}
