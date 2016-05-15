using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AGrammar
{
    public class Arg
    {
        public static ArgOr One(params object[] args)
        {
            ArgOr arg = new ArgOr();
            arg.args = args;
            return arg;
        }
        public static ArgProp Prop(string propName, Segment exp)
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
            arg.exp = new Expression();
            arg.exp.tokenType = tokenID;
            return arg;
        }
        public static ArgProp Prop(string propName, string content)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = new Expression();
            arg.exp.content = content;
            return arg;
        }

        public class ArgOr : Arg
        {
            public object[] args;
            internal ArgOr()
            {
            }
        }

        public class ArgProp : Arg
        {
            public string propName;
            public Expression exp;
            internal ArgProp()
            {
            }
        }
    }
}
