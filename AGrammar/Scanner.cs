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
        enum CommentState
        {
            None,
            Line,
            Block,
        }

        public Action<string> ErrorHandler;

        public string LineComment = "//";
        public string BlockCommentStart = "/*";
        public string BlockCommentEnd = "*/";
        string[] mTermins = new string[]{
            "+=", "-=", "*=", "/=","<=", "==","!=", ">=", "||", "&&", "++", "--",
            "+", "-", "*", "/", ".", "=", "?", "(", ")", "{", "}", "[", "]","<",">" ,":", ";", ",", "|", "&",
            };

        Dictionary<string, KeyWord> mKeyWords = new Dictionary<string, KeyWord>();

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
            mKeyWords.Clear();

            foreach (var item in tokens)
            {
                if (item.WordType == Grammar.ID)
                {
                    Error("ID 0 is reserver for global id.");
                    return false;
                }
                mKeyWords.Add(item.Word, item);
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
        bool MatchTerminations(ref string content, List<Token> tokens,int i, StringBuilder sb, int line, int col,out string terimnal)
        {
            for (int j = 0; j < mTermins.Length; ++j)
            {
                string ter = mTermins[j];

                if (Check(content, i, ter))
                {
                    Terminate(sb, line, col, tokens);
                    AddTerminate(ter, line, col, tokens);
                    terimnal = ter;
                    return true;
                }
            }
            terimnal = null;
            return false;
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

            for (int i = 0; i < len; )
            {
                char ch = content[i];

                int offset = 1;

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
                                if (ch == '\n' || ch == '\t' || ch == ' ')
                                {
                                    Terminate(sb, line, col, tokens);
                                }
                                else
                                {
                                    string ter;

                                    if (MatchTerminations(ref content, tokens, i, sb, line, col, out ter))
                                    {
                                        offset = ter.Length;
                                    }
                                    else
                                    {
                                        sb.Append(ch);
                                    }
                                }
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
                                offset = BlockCommentEnd.Length;
                            }
                        }
                        break;
                }

                col += offset;
                i += offset;

                if (ch == '\n')
                {
                    col = 1;
                    line++;
                }
            }
            return tokens;
        }

        private int GetTokenType(string lex)
        {
            if (mTermins.Contains(lex))
                return Grammar.InvalidTokenType;
            KeyWord word = null;
            mKeyWords.TryGetValue(lex, out word);
            return word == null ? Grammar.ID : word.WordType;
        }
    }
}
