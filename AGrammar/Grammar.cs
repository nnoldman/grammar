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
        public const int ID = 0;
        public const string Eof = "EOF";
        public const string LineFeed = "\n";
        public const int InvalidTokenType = -1;

        Scanner mScanner;
        List<Token> mTokens;
        GrammarTree mTree = new GrammarTree();
        Config mConfig;

        static int mErrorID;

        Dictionary<int, List<Token>> mErrorStack = new Dictionary<int, List<Token>>();

        public static Production Empty
        {
            get
            {
                return new ProductionOfEmpty();
            }
        }
        public static Production Any
        {
            get
            {
                return new ProductionAny();
            }
        }
        public Grammar()
        {
            mScanner = new Scanner();
            mTokens = new List<Token>();
        }
        public Config config
        {
            get
            {
                return mConfig;
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
            if (mConfig.msgHaneler != null)
                mConfig.msgHaneler(msg);
        }


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

        public GrammarTree Generate(Config config, string content)
        {
            mConfig = config;

            if (!mConfig.Valid)
                return null;

            DateTime t0 = DateTime.Now;

            mTokens = mScanner.Scan(this, content);

            mTree = GenerateTree(mConfig.grammar.root);

            ErrorReport();

            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - t0.Ticks);

            if (mConfig.msgHaneler != null)
                mConfig.msgHaneler(string.Format("Time:{0:00}:{1:00}:{2:00}:{3:00}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds));

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
