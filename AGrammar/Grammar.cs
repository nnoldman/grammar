using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AGrammar
{
    public class BoolObject
    {
        public static implicit operator bool(BoolObject obj)
        {
            return obj != null;
        }
    }
    public class GrammarTreeNode : BoolObject
    {
        public string propName;

        public virtual void Write(StringBuilder sb, int tabCount)
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

        public override void Write(StringBuilder sb, int tabCount)
        {
            sb.AppendLine(new string(' ', tabCount) + ToString());
        }
    }
    public class GrammarTree : GrammarTreeNode
    {
        public string segType;
        public List<GrammarTreeNode> propertices = new List<GrammarTreeNode>();

        public string space(int tabcount)
        {
            return new string(' ', tabcount);
        }
        public override void Write(StringBuilder sb, int tabCount)
        {
            sb.AppendLine(space(tabCount) + segType);
            sb.AppendLine(space(tabCount) + '{');
            tabCount += 4;
            foreach (var prop in propertices)
                prop.Write(sb, tabCount);
            tabCount -= 4;
            sb.AppendLine(space(tabCount) + '}');
        }

        public override string ToString()
        {
            return segType;
        }
    }

    public class Grammar
    {
        static readonly Type mTypeString = typeof(string);
        static readonly Type mTypeInt = typeof(int);
        static readonly Type mTypeProperty = typeof(Arg.ArgProp);
        static readonly Type mTypeOr = typeof(Arg.ArgOr);
        static readonly Type mTypeSeg = typeof(Segment);
        static readonly Type mTypeExp = typeof(Expression);
        static readonly Type mTypeEmpty = typeof(EmptyExp);

        public const int ID = 0;

        public Action<string> ErrorHandler;
        public TokenParam[] TokenParams;

        Scanner mScanner;
        List<GrammarToken> mTokens;
        public Dictionary<string, Segment> mSegments;

        public static EmptyExp Empty
        {
            get
            {
                return new EmptyExp();
            }
        }
        public Grammar()
        {
            mScanner = new Scanner();
            mTokens = new List<GrammarToken>();
            mSegments = new Dictionary<string, Segment>();
        }
        void Error(string msg)
        {
            if (ErrorHandler != null)
                ErrorHandler(msg);
        }
        void Error(GrammarToken token)
        {
            Error(string.Format("Load Error:{0}", token.ToString()));
        }
        public GrammarTree Generate(string content)
        {
            if (ErrorHandler != null)
                mScanner.ErrorHandler = ErrorHandler;

            mTokens = mScanner.Scan(TokenParams, content);
            GrammarTree tree = new GrammarTree();
            tree.propName = "grammar";
            int idx = 0;
            while (idx < mTokens.Count)
            {
                int offset = 0;
                Segment seg = Parse(idx, ref offset, tree);
                idx += offset;
            }
            return tree;
        }

        Segment Parse(int idx, ref int offset, GrammarTree parent)
        {
            foreach (var exp in mSegments.Values)
            {
                if (exp.Match(ref mTokens, idx, ref offset, parent, string.Empty))
                    return exp;
            }
            return null;
        }

        public Segment Add(string name)
        {
            return TryGetAndCreate(name);
        }
        Segment TryGetAndCreate(string name)
        {
            Segment exp;
            if (!mSegments.TryGetValue(name, out exp))
            {
                exp = new Segment(name);
                exp.grammar = this;
                mSegments.Add(name, exp);
            }
            return exp;
        }
        internal Expression Create(object arg, Segment parent)
        {
            var tp = arg.GetType();

            if (tp == mTypeString)
            {
                return Create((string)arg, parent);
            }
            else if (tp == mTypeInt)
            {
                return Create((int)arg, parent);
            }
            else if (tp == mTypeProperty)
            {
                return Create((Arg.ArgProp)arg, parent);
            }
            else if (tp == mTypeOr)
            {
                return Create((Arg.ArgOr)arg, parent);
            }
            else if (tp == mTypeSeg)
            {
                if (parent)
                    parent.AddChildren((Segment)arg);
                return (Segment)arg;
            }
            else if (tp == mTypeEmpty)
            {
                if (parent)
                    parent.AddChildren((EmptyExp)arg);
                return (EmptyExp)arg;
            }
            throw new Exception();
        }

        public Segment Get(string name)
        {
            return mSegments[name];
        }

        string GetContent(int tokenid)
        {
            foreach (var param in TokenParams)
            {
                if (param.TokenType == tokenid)
                    return param.Content;
            }
            return string.Empty;
        }
        public void LoadExpression(Action<Grammar> loader)
        {
            if (loader != null)
                loader(this);
            CloseLexer();
        }
        void CloseLexer()
        {
            foreach (var seg in mSegments)
            {
                seg.Value.Process();
            }
        }

        Expression Create(int arg, Segment parent)
        {
            Expression exp = new Expression();
            exp.tokenType = arg;
            exp.content = GetContent(arg);
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }
        static Expression Create(string arg, Segment parent)
        {
            Expression exp = new Expression();
            exp.content = arg;
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }

        static PropExpression Create(Arg.ArgProp arg, Segment parent)
        {
            PropExpression exp = new PropExpression(arg.propName, parent.name);
            exp.executer = arg.exp;
            exp.propertyName = arg.propName;
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }
        Expression Create(Arg.ArgOr arg, Segment parent)
        {
            OrExpression or = new OrExpression();
            parent.AddChildren(or);
            foreach (var orarg in arg.args)
            {
                Expression exp = Create(orarg, null);
                or.sublings.Add(exp);
            }
            return or;
        }
    }
}
