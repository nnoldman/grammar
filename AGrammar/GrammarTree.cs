using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AGrammar
{
    public class GrammarTreeNode : BoolObject
    {
        public string propName;

        public virtual void WriteTo(StringBuilder sb, int tabCount = 0)
        {
            throw new Exception();
        }
    }
    public class PropertyTreeNode : GrammarTreeNode
    {
        public string content;

        public override string ToString()
        {
            return propName + ":" + content;
        }

        public override void WriteTo(StringBuilder sb, int tabCount = 0)
        {
            sb.AppendLine(new string(' ', tabCount) + ToString());
        }
    }
    public class GrammarTree : GrammarTreeNode
    {
        public List<GrammarTreeNode> propertices = new List<GrammarTreeNode>();

        string Space(int tabcount)
        {
            return new string(' ', tabcount);
        }
        public override void WriteTo(StringBuilder sb, int tabCount = 0)
        {
            sb.AppendLine(Space(tabCount) + propName);
            sb.AppendLine(Space(tabCount) + '{');
            tabCount += 4;
            foreach (var prop in propertices)
                prop.WriteTo(sb, tabCount);
            tabCount -= 4;
            sb.AppendLine(Space(tabCount) + '}');
        }

        public override string ToString()
        {
            return propName;
        }
    }
}
