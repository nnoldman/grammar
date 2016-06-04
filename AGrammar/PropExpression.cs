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
        internal override bool Match(int start, ref int offset, GrammarTree parent, string propName)
        {
            if (executer.Match(start, ref offset, parent, this.propertyName))
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return parentName + "." + propertyName;
        }

        internal override bool FastMatch(int start, ref int offset)
        {
            if (executer.FastMatch(start, ref offset))
            {
                return true;
            }
            return false;
        }

        public override Expression Copy()
        {
            PropExpression exp = new PropExpression(this.propertyName, this.parentName);
            exp.myToken = this.myToken;
            exp.count = this.count;
            exp.grammar = this.grammar;
            exp.executer = this.executer.Copy();
            return exp;
        }

        internal override void SetNext()
        {
            base.SetNext();

            if (this.next)
            {
                this.executer.next = this.next;
                this.executer.SetNext();
            }
        }
    }
}
