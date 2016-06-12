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
        internal List<Token2> errors = new List<Token2>();
    }
    public class Grammar
    {
        public const int ID = 0;

        public const string EOFToken = "EOF";
        public static int InvalidTokenType = -1;

        Action<string> mMessageHandler;
        KeyWord[] mExternTokens;

        Scanner mScanner;
        List<Token2> mTokens;
        GrammarTree mTree = new GrammarTree();

        Dictionary<int, List<Token2>> mErrorStack = new Dictionary<int, List<Token2>>();

        public static Production Empty
        {
            get
            {
                return new ProductionOfEmpty();
            }
        }
        public Grammar()
        {
            mScanner = new Scanner();
            mTokens = new List<Token2>();
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
        internal List<Token2> Tokens
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

        internal void PushError(int errid, Token2 token)
        {
            mErrorStack[errid].Add(token);
        }
        internal int RequireErrorID()
        {
            mErrorID++;
            mErrorStack.Add(mErrorID, new List<Token2>());
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
        void Error(Token2 token)
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

        public GrammarTree Generate(Production root, string content, KeyWord[] tokens, Action<string> handler)
        {
            if (!root)
                return null;

            this.mExternTokens = tokens;
            this.mMessageHandler = handler;
            this.mScanner.ErrorHandler = handler;

            DateTime t0 = DateTime.Now;

            mTokens = mScanner.Scan(mExternTokens, content);

            mTree = GenerateTree(root);

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

            List<Token2> errors = new List<Token2>();
            foreach (var i in mErrorStack)
            {
                errors.AddRange(i.Value);
            }
            errors.Sort(SortError);
            Error(errors[0].Error());
        }

        int SortError(Token2 a, Token2 b)
        {
            if (a.Line == b.Line && a.Column == b.Column)
                return 0;
            if (a.Line > b.Line || (a.Line == b.Line && a.Column > b.Column))
                return -1;
            return 1;
            throw new Exception();
        }

        GrammarTree GenerateTree(Production p)
        {
            GrammarTree tree = new GrammarTree();
            tree.propName = p.ToString();
            int offset = 0;

            if (p.Match(mTokens, ref offset, tree))
                mTree = tree;

            return mTree;
        }
        
        void LoadExpressions(Action<Grammar> loader)
        {
            if (loader != null)
                loader(this);
            SaveSections();
        }

        void SaveSections()
        {
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var seg in mSections)
        //    {
        //        seg.Value.SaveTo(sb, 0);
        //    }
        //    File.WriteAllBytes("sections.lua",new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
        //
        }
    }
}
