using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AGrammar
{
    public class Arg
    {
        public static ArgProp Prop(string propName, CompositeExpression exp)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = exp;
            return arg;
        }
        public static ArgProp Prop(string propName, int tokenID)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = tokenID;
            return arg;
        }
        public static ArgProp Prop(string propName, string content)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp= content;
            return arg;
        }

        public class ArgProp : Arg
        {
            public string propName;
            public object exp;
            internal ArgProp()
            {
            }
        }
    }
}
