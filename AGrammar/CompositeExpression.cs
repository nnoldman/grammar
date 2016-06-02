using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public enum CompositeType
    {
        None,
        And,
        Or,
    }
    public class CompositeExpression : Expression
    {
        public string name = string.Empty;

        public CompositeExpression()
        {
        }
        protected List<Expression> mChildren = new List<Expression>();

        public List<Expression> children { get {
            return mChildren; } }

        internal void AddChildren(Expression exp)
        {
            children.Add(exp);
            exp.parent = this;
        }

        internal virtual Expression GetNextSubling(Expression exp)
        {
            throw new Exception();
        }

 
    }
}
