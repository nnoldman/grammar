using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
namespace AGrammar
{
    public class KeyWord
    {
        public int WordType;
        public string Word;

        public override string ToString()
        {
            return string.Format("<{0}>{1}", WordType, Word);
        }
    }
    internal class Token
    {
        public int WordType;
        public string Word;
        public int Line;
        public int Column;

        public Token()
        {
        }
        public Token(int tt, string content, int line, int indexOnLine)
        {
            this.WordType = tt;
            this.Word = content;
            this.Line = line;
            this.Column = indexOnLine;
        }
        public override string ToString()
        {
            return string.Format("{0} [{1}]", WordType, Word);
        }
        public string Error()
        {
            return string.Format("Error=>Line:{0},Col:{1},Content:{2}", Line, Column, Word);
        }
    }

    internal class Scanner
    {
        public Action<string> ErrorHandler;

        public string LineComment = "//";
        public string BlockCommentStart = "/*";
        public string BlockCommentEnd = "*/";
        string[] mTermins = new string[]{
            "+=", "-=", "*=", "/=","<=", "==","!=", ">=", "||", "&&", "++", "--",
            "+", "-", "*", "/", ".", "=", "?", "(", ")", "{", "}", "[", "]","<",">" ,":", ";", ",", "|", "&",
            };

        public string[] Termins
        {
            get
            {
                return mTermins;
            }
            set
            {
                List<string> rawList = new List<string>();
                rawList.AddRange(value);
                rawList.Sort(SortFunc);
                mTermins = rawList.ToArray();
            }
        }



        Dictionary<string, KeyWord> mTokenMap = new Dictionary<string, KeyWord>();


        static int SortFunc(string a, string b)
        {
            int la = a.Length;
            int lb = b.Length;
            if (la == lb)
                return 0;
            if (la > lb)
                return -1;
            return 1;
        }

        void Error(string msg)
        {
            if (ErrorHandler != null)
                ErrorHandler(msg);
        }
        public bool Init(KeyWord[] tokens)
        {
            mTokenMap.Clear();

            foreach (var item in tokens)
            {
                if (item.WordType == Grammar.ID)
                {
                    Error("ID 0 is reserver for global id.");
                    return false;
                }
                mTokenMap.Add(item.Word, item);
            }
            return true;
        }

        bool Check(string content, int idx, string target)
        {
            if (target == null)
                return false;
            
            if (target.Length == 1)
                return idx < content.Length && content[idx] == target[0];

            if (target.Length == 2)
                return idx + 1 < content.Length && content[idx] == target[0] && content[idx + 1] == target[1];

            throw new Exception();
        }

        public enum CommentState
        {
            None,
            Line,
            Block,
        }
        void Terminate(StringBuilder sb,int line,int col,List<Token> tokens)
        {
            if (sb.Length == 0)
                return;
            int tp = GetTokenType(sb.ToString());
            Token t = new Token(tp, sb.ToString(), line, col - sb.Length);
            tokens.Add(t);
            sb.Clear();
        }
        void AddTerminate(string ter, int line, int col, List<Token> tokens)
        {
            Token t = new Token();
            t.WordType = GetTokenType(ter);
            t.Word = ter;
            t.Line = line;
            t.Column = col;
            tokens.Add(t);
        }
        void MatchTerminations(ref string content, List<Token> tokens, char ch, int i, StringBuilder sb, int line, int col)
        {
            if (ch == '\n' || ch == '\t' || ch == ' ')
            {
                Terminate(sb, line, col, tokens);
            }
            else
            {
                bool hit = false;

                for (int j = 0; j < mTermins.Length; ++j)
                {
                    string ter = mTermins[j];

                    if (Check(content, i, ter))
                    {
                        hit = true;
                        Terminate(sb, line, col, tokens);
                        AddTerminate(ter, line, col, tokens);
                        break;
                    }
                }
                if (!hit)
                    sb.Append(ch);
            }
        }
        public List<Token> Scan(KeyWord[] tokenParams, string content)
        {
            if (!Init(tokenParams))
                return null;

            Debug.Assert(mTermins != null);

            content = content.Replace("\r\n", "\n");

            List<Token> tokens = new List<Token>();
            int line = 1;
            int len = content.Length;
            int col = 1;

            CommentState commenting = CommentState.None;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; ++i, ++col)
            {
                char ch = content[i];

                switch (commenting)
                {
                    case CommentState.None:
                        {
                            if (Check(content, i, LineComment))
                            {
                                Terminate(sb, line, col, tokens);
                                commenting = CommentState.Line;
                            }
                            else if (Check(content, i, BlockCommentStart))
                            {
                                Terminate(sb, line, col, tokens);
                                commenting = CommentState.Block;
                            }
                            else
                            {
                                MatchTerminations(ref content, tokens, ch, i, sb, line, col);
                            }
                        }
                        break;
                    case CommentState.Line:
                        {
                            if (ch == '\n')
                                commenting = CommentState.None;
                        }
                        break;
                    case CommentState.Block:
                        {
                            if (Check(content, i, BlockCommentEnd))
                            {
                                commenting = CommentState.None;
                                ++i;
                            }
                        }
                        break;
                }

                if (ch == '\n')
                {
                    col = 0;
                    line++;
                }
            }
            return tokens;
        }

        private int GetTokenType(string lex)
        {
            KeyWord result = null;
            mTokenMap.TryGetValue(lex, out result);
            return result == null ? 0 : result.WordType;
        }
    }
}
