using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AGrammar
{
    internal class FrameError
    {
        internal List<Token> errors = new List<Token>();
    }
    public class Grammar
    {
        static readonly Type mTypeString = typeof(string);
        static readonly Type mTypeInt = typeof(int);
        static readonly Type mTypeProperty = typeof(Arg.ArgProp);
        static readonly Type mTypeExp = typeof(Expression);
        static readonly Type mTypeAndArg = typeof(Arg.ArgAnd);

        /// <summary>
        /// Extern token id can not be 0.
        /// </summary>
        public const int ID = 0;
        public static int InvalidTokenType = -1;

        Action<string> mMessageHandler;
        KeyWord[] mExternTokens;

        Scanner mScanner;
        List<Token> mTokens;
        Dictionary<string, CompositeExpression> mSegments;
        Dictionary<string, CompositeExpression> mSections;
        GrammarTree mTree = new GrammarTree();

        Dictionary<int, List<Token>> mErrorStack = new Dictionary<int, List<Token>>();

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
        public string[] Termins
        {
            get
            {
                return mScanner.Termins;
            }
            set
            {
                mScanner.Termins = value;
            }
        }
        public GrammarTree Tree
        {
            get
            {
                return mTree;
            }
        }
        internal List<Token> Tokens
        {
            get
            {
                return mTokens;
            }
        }
        internal void Error(string msg)
        {
            if (mMessageHandler != null)
                mMessageHandler(msg);
        }

        static int mErrorID;

        internal void PushError(int errid, Token token)
        {
            mErrorStack[errid].Add(token);
        }
        internal int RequireErrorID()
        {
            mErrorID++;
            mErrorStack.Add(mErrorID, new List<Token>());
            return mErrorID;
        }
        internal void PopError(int errorID)
        {
            List<int> removeKeys = new List<int>();
            foreach (var i in mErrorStack)
            {
                if (i.Key > errorID)
                    removeKeys.Add(i.Key);
            }
            foreach (var i in removeKeys)
                mErrorStack.Remove(i);
            mErrorStack.Remove(errorID);
        }
        void Error(Token token)
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

        public void LineComment(string s)
        {
            mScanner.LineComment = s;
        }
        public void AddTerminations(params string[] args)
        {
            mScanner.Termins = args;
        }
        public void BlockComment(string s, string e)
        {
            mScanner.BlockCommentStart = s;
            mScanner.BlockCommentEnd = e;

            Debug.Assert(!string.IsNullOrEmpty(mScanner.BlockCommentStart));
            Debug.Assert(!string.IsNullOrEmpty(mScanner.BlockCommentEnd));
        }

        public GrammarTree Generate(string content, KeyWord[] tokens, Action<string> handler, Action<Grammar> loader)
        {
            if (loader == null)
                return null;

            this.mExternTokens = tokens;
            this.mMessageHandler = handler;
            this.mScanner.ErrorHandler = handler;

            DateTime t0 = DateTime.Now;

            LoadExpressions(loader);

            mTokens = mScanner.Scan(mExternTokens, content);

            mTree = GenerateTree();

            ErrorReport();

            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - t0.Ticks);

            if (mMessageHandler != null)
                mMessageHandler(string.Format("Time:{0:00}:{1:00}:{2:00}:{3:00}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds));

            return mTree;
        }

        void ErrorReport()
        {
            if (mErrorStack.Count == 0)
                return;

            List<Token> errors = new List<Token>();
            foreach (var i in mErrorStack)
            {
                errors.AddRange(i.Value);
            }
            errors.Sort(SortError);
            Error(errors[0].Error());
        }

        int SortError(Token a, Token b)
        {
            if (a.Line == b.Line && a.Column == b.Column)
                return 0;
            if (a.Line > b.Line || (a.Line == b.Line && a.Column > b.Column))
                return -1;
            return 1;
            throw new Exception();
        }

        GrammarTree GenerateTree()
        {
            GrammarTree tree = new GrammarTree();
            tree.propName = "grammar";

            for (int i = 0; i < mTokens.Count; )
            {
                int offset = 0;
                CompositeExpression seg = Parse(i, ref offset, tree);
                if (seg == null)
                    return null;
                i += offset;
            }
            return tree;
        }
        CompositeExpression Parse(int idx, ref int offset, GrammarTree parent)
        {
            foreach (var exp in this.mSections.Values)
            {
                if (exp.Match(idx, ref offset, parent, string.Empty))
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

        public OrExpression SectionOr(string name)
        {
            return Sec<OrExpression>(name);
        }
        public AndExpression SectionAnd(string name)
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
            else if (tp.IsSubclassOf(mTypeExp))
            {
                var exp = (Expression)arg;
                if (parent)
                    parent.AddChildren(exp);
                return exp;
            }
            else if (tp == mTypeAndArg)
            {
                return Create((Arg.ArgAnd)arg, parent);
            }
            throw new Exception();
        }

        public CompositeExpression Get(string name)
        {
            CompositeExpression seg = null;
            mSegments.TryGetValue(name, out seg);
            return seg;
        }

        void LoadExpressions(Action<Grammar> loader)
        {
            if (mSections.Count == 0 && loader != null)
                loader(this);

            foreach (var seg in mSections)
                seg.Value.SetNext();

            SaveSections();
        }

        void SaveSections()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var seg in mSections)
            {
                seg.Value.SaveTo(sb, 0);
            }
            File.WriteAllBytes("sections.lua",new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
        }

        Expression Create(int arg, CompositeExpression parent)
        {
            Expression exp = new Expression();
            exp.myToken.type = arg;
            if (parent)
                parent.AddChildren(exp);
            return exp;
        }
        Expression Create(Arg.ArgAnd arg, CompositeExpression parent)
        {
            AndExpression aexp = new AndExpression();
            aexp.grammar = parent.grammar;
            aexp.Is(arg.args);
            if (arg.array)
                aexp.Array();
            if (parent)
                parent.AddChildren(aexp);
            return aexp;
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
                parent.AddChildren(comExp);
            }
            Expression exp = new Expression();

            exp.myToken.content = arg;

            if (parent)
            {
                exp.grammar = parent.grammar;
                parent.AddChildren(exp);
            }

            return exp;
        }

        static PropExpression Create(Arg.ArgProp arg, CompositeExpression parent)
        {
            PropExpression exp = new PropExpression(arg.propName, parent.name);

            exp.grammar = parent.grammar;

            if (arg.exp is int)
            {
                Expression executer = new Expression();
                executer.myToken.type = (int)arg.exp;
                exp.executer = executer;
            }
            else if (arg.exp is string)
            {
                string name = (string)arg.exp;

                if (IsCompositeExpression(name))
                {
                    CompositeExpression comExp = parent.grammar.Get(GetCompositeExpName(name));

                    if (comExp)
                    {
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
                    executer.myToken.content = name;
                    exp.executer = executer;
                }
            }
            else if (arg.exp is Expression)
            {
                exp.executer = ((Expression)arg.exp);
            }
            else
            {
                throw new Exception();
            }

            exp.propertyName = arg.propName;

            if (parent)
            {
                exp.executer.grammar = parent.grammar;
                exp.grammar = parent.grammar;
                parent.AddChildren(exp);
            }

            return exp;
        }
    }
}
