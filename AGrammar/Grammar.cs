using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AGrammar
{
    public class Grammar
    {
        static readonly Type mTypeString = typeof(string);
        static readonly Type mTypeInt = typeof(int);
        static readonly Type mTypeProperty = typeof(Arg.ArgProp);
        static readonly Type mTypeSeg = typeof(AndExpression);
        static readonly Type mTypeExp = typeof(Expression);
        static readonly Type mTypeEmpty = typeof(EmptyExpression);

        /// <summary>
        /// Extern token id can not be 0.
        /// </summary>
        public const int ID = 0;

        Action<string> mMessageHandler;
        ExternToken[] mExternTokens;

        Scanner mScanner;
        List<Token> mTokens;
        Dictionary<string, CompositeExpression> mSegments;
        Dictionary<string, CompositeExpression> mSections;
        GrammarTree mTree = new GrammarTree();

        public static EmptyExpression Empty
        {
            get
            {
                return new EmptyExpression();
            }
        }
        public Grammar()
        {
            mScanner = new Scanner();
            mTokens = new List<Token>();
            mSegments = new Dictionary<string, CompositeExpression>();
            mSections = new Dictionary<string, CompositeExpression>();
        }
        public GrammarTree Tree
        {
            get
            {
                return mTree;
            }
        }
        internal void Error(string msg)
        {
            if (mMessageHandler != null)
                mMessageHandler(msg);
        }
        internal void Error(Token token)
        {
            Error(string.Format("Load Error:{0}", token.ToString()));
        }

        public byte[] OutPutDebug()
        {
            if (!mTree)
                return null;
            StringBuilder sb = new StringBuilder();
            this.mTree.WriteTo(sb);
            return new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray());
        }
        public GrammarTree Generate(string content, ExternToken[] tokens, Action<string> handler,Action<Grammar> loader)
        {
            if (loader == null)
                return null;

            this.mExternTokens = tokens;
            this.mMessageHandler = handler;
            this.mScanner.ErrorHandler = handler;

            LoadExpressions(loader);

            DateTime t0 = DateTime.Now;

            mTokens = mScanner.Scan(mExternTokens, content);
            mTree = GenerateTree();

            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - t0.Ticks);

            if (mMessageHandler != null)
                mMessageHandler(string.Format("Time:{0:00}:{1:00}:{2:00}:{3:00}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds));
            
            return mTree;
        }
        GrammarTree GenerateTree()
        {
            GrammarTree tree = new GrammarTree();
            tree.propName = "grammar";

            for (int i = 0; i < mTokens.Count;)
            {
                int offset = 0;
                CompositeExpression seg = Parse(i, ref offset, tree);
                if (seg == null)
                {
                    Error(mTokens[i + offset].Error());
                    return null;
                }
                i += offset;
            }
            return tree;
        }
        CompositeExpression Parse(int idx, ref int offset, GrammarTree parent)
        {
            foreach (var exp in this.mSections.Values)
            {
                if (exp.Match(ref mTokens, idx, ref offset, parent, string.Empty))
                    return exp;
            }
            return null;
        }
        /// <summary>
        /// add section
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal T Sec<T>(string name) where T : CompositeExpression, new()
        {
            T seg = TryGetAndCreate<T>(name);
            if (seg)
            {
                mSections.Add(name, seg);
            }
            return (T)seg;
        }

        public OrExpression SecOr(string name)
        {
            return Sec<OrExpression>(name);
        }
        public AndExpression SecAnd(string name)
        {
            return Sec<AndExpression>(name);
        }
        /// <summary>
        /// add or exp
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrExpression Or(string name)
        {
            return TryGetAndCreate<OrExpression>(name);
        }
        /// <summary>
        /// add and exp
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public AndExpression And(string name)
        {
            return TryGetAndCreate<AndExpression>(name);
        }
        T TryGetAndCreate<T>(string name) where T : CompositeExpression, new()
        {
            CompositeExpression exp;
            if (!mSegments.TryGetValue(name, out exp))
            {
                exp = new T();
                exp.name = name;
                exp.grammar = this;
                mSegments.Add(name, exp);
            }
            return (T)exp;
        }
        internal Expression Create(object arg, CompositeExpression parent)
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
            else if (tp == mTypeSeg)
            {
                if (parent)
                    parent.AddChildren((AndExpression)arg);
                return (AndExpression)arg;
            }
            else if (tp == mTypeEmpty)
            {
                if (parent)
                    parent.AddChildren((EmptyExpression)arg);
                return (EmptyExpression)arg;
            }
            throw new Exception();
        }

        public CompositeExpression Get(string name)
        {
            CompositeExpression seg = null;
            mSegments.TryGetValue(name, out seg);
            return seg;
        }

        string GetContent(int tokenid)
        {
            foreach (var param in mExternTokens)
            {
                if (param.TokenType == tokenid)
                    return param.Content;
            }
            return string.Empty;
        }
        void LoadExpressions(Action<Grammar> loader)
        {
            if (mSections.Count == 0 && loader != null)
                loader(this);

            foreach (var seg in mSections)
                seg.Value.SetNext();
        }

        Expression Create(int arg, CompositeExpression parent)
        {
            Expression exp = new Expression();
            exp.tokenType = arg;
            //exp.content = GetContent(arg);
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }

        static string RightBrack = new string('>', 1);

        static bool IsCompositeExpression(string name)
        {
            return name.IndexOf('<') == 0 && name.EndsWith(RightBrack);
        }
        static string GetCompositeExpName(string rawName)
        {
            return rawName.Substring(1, rawName.Length - 2);
        }
        static Expression Create(string arg, CompositeExpression parent)
        {
            if (IsCompositeExpression(arg))
            {
                string name = GetCompositeExpName(arg);

                CompositeExpression comExp = parent.grammar.Get(name);
                
                if (comExp)
                {
                    var newComExp = comExp.Copy();
                    parent.AddChildren(newComExp);
                    return newComExp;
                }
                else
                {
                    throw new Exception("Unknown expression " + name);
                }
            }
            Expression exp = new Expression();
            exp.content = arg;
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }

        static PropExpression Create(Arg.ArgProp arg, CompositeExpression parent)
        {
            PropExpression exp = new PropExpression(arg.propName, parent.name);
            
            if (arg.exp is int)
            {
                Expression executer = new Expression();
                executer.tokenType = (int)arg.exp;
                exp.executer = executer;
            }
            else if (arg.exp is string)
            {
                string name=(string)arg.exp;
                if (IsCompositeExpression(name))
                {
                    CompositeExpression comExp = parent.grammar.Get(GetCompositeExpName(name));

                    if (comExp)
                    {
                        var newComExp = comExp.Copy();
                        exp.executer = comExp;
                    }
                    else
                    {
                        throw new Exception("Unknown expression " + GetCompositeExpName(name));
                    }
                }
                else
                {
                    Expression executer = new Expression();
                    executer.content = name;
                    exp.executer = executer;
                }
            }
            else if (arg.exp is Expression)
            {
                exp.executer = (Expression)arg.exp;
            }
            else
            {
                throw new Exception();
            }
            
            exp.propertyName = arg.propName;

            if (parent)
                parent.AddChildren(exp);

            return exp;
        }
    }
}
