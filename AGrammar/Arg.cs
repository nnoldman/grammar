using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

namespace AGrammar
{
    public class Arg
    {
        /// <summary>
        /// property
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="exp"></param>
        /// <returns></returns>
        public static ArgProp P(string propName, CompositeExpression exp)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = exp;
            return arg;
        }
        /// <summary>
        /// property
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="tokenID"></param>
        /// <returns></returns>
        public static ArgProp P(string propName, int tokenID)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = tokenID;
            return arg;
        }
        /// <summary>
        /// property
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static ArgProp P(string propName, string content)
        {
            ArgProp arg = new ArgProp();
            arg.propName = propName;
            arg.exp = content;
            return arg;
        }
        public static ArgAnd And(params object[] args)
        {
            ArgAnd arg = new ArgAnd();
            arg.args = args;
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
        public class ArgAnd : Arg
        {
            public object[] args;
            public bool array = false;
            public ArgAnd Array()
            {
                array = true;
                return this;
            }
        }
    }
}
