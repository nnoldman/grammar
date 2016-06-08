using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Node : BoolObject
    {
        public string name = string.Empty;

        internal void AddContent(GrammarTree tree)
        {
            if (!string.IsNullOrEmpty(name))
            {
                GrammarTreeNode node = new GrammarTreeNode();
                node.propName = name;
                tree.propertices.Add(node);
            }
        }
        internal void AddContent(GrammarTree tree, List<Token> tokens, int n)
        {
            PropertyTreeNode node = new PropertyTreeNode();
            node.propName = name;
            node.content = tokens[n].Word;
            tree.propertices.Add(node);
        }
    }
}
