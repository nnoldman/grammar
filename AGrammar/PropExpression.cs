using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class PropExpression : Expression
    {
        public string parentName = string.Empty;
        public string propertyName = string.Empty;
        public Expression executer;

        internal PropExpression(string propName, string parentName)
        {
            this.propertyName = propName;
            this.parentName = parentName;
        }
        internal override bool Match(ref List<Token> tokens, int start, ref int offset, GrammarTree parent, string propName)
        {
            if (executer.Match(ref tokens, start, ref offset, parent, this.propertyName))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return parentName + "." + propertyName;
        }
    }
}
