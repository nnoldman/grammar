using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AGrammar
{
    public class ExternToken
    {
        public int TokenType;
        public string Content;

        public override string ToString()
        {
            return string.Format("<{0}>{1}", TokenType, Content);
        }
    }
    internal class Token
    {
        public int TokenType;
        public string Content;
        public int Line;
        public int Column;

        public Token()
        {
        }
        public Token(int tt, string content, int line, int indexOnLine)
        {
            this.TokenType = tt;
            this.Content = content;
            this.Line = line;
            this.Column = indexOnLine;
        }
        public override string ToString()
        {
            return string.Format("{0} [{1}]", TokenType, Content);
        }
        public string Error()
        {
            return string.Format("Line:{0},Col:{1},Content:{2}", Line, Column, Content);
        }
    }

    internal class Scanner
    {
        public Action<string> ErrorHandler;

        Dictionary<string, ExternToken> mTokenMap = new Dictionary<string, ExternToken>();

        void Error(string msg)
        {
            if (ErrorHandler != null)
                ErrorHandler(msg);
        }
        public bool Init(ExternToken[] tokens)
        {
            mTokenMap.Clear();

            foreach (var item in tokens)
            {
                if (item.TokenType == Grammar.ID)
                {
                    Error("ID 0 is reserver for global id.");
                    return false;
                }
                mTokenMap.Add(item.Content, item);
            }
            return true;
        }
        bool Check(string content, int idx, char c)
        {
            if (idx < content.Length && idx >= 0)
            {
                return content[idx] == c;
            }
            return false;
        }
        public List<Token> Scan(ExternToken[] tokenParams, string content)
        {
            if (!Init(tokenParams))
                return null;

            content = content.Replace("\r\n", "\n");

            List<Token> tokens = new List<Token>();
            int line = 1;
            int len = content.Length;
            int indexOnLine = 1;
            int lineStartIndex = 0;
            bool commenting = false;

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < len; ++i, ++indexOnLine)
            {
                char ch = content[i];
                if ((commenting && ch == '\n') || !commenting)
                {
                    switch (ch)
                    {
                        case '/':
                            {
                                if (Check(content, i + 1, '/'))
                                {
                                    if (sb.Length > 0)
                                    {
                                        int tp = GetTokenType(sb.ToString());
                                        Token t = new Token(tp, sb.ToString(), line, indexOnLine - sb.Length);
                                        tokens.Add(t);
                                        sb.Clear();
                                    }
                                    commenting = true;
                                }
                            }
                            break;
                        case '\n':
                            {
                                commenting = false;
                                if (sb.Length > 0)
                                {
                                    int tp = GetTokenType(sb.ToString());
                                    Token t = new Token(tp, sb.ToString(), line, indexOnLine - sb.Length);
                                    tokens.Add(t);
                                    sb.Clear();
                                }
                                line++;
                                indexOnLine = 0;
                                lineStartIndex = i;
                            }
                            break;
                        case '=':
                        case '{':
                        case '}':
                        case '(':
                        case ')':
                        case '[':
                        case ']':
                        case ' ':
                        case '\t':
                        case '.':
                        case ':':
                        case ';':
                        case ',':
                            {
                                if (sb.Length > 0)
                                {
                                    int tp = GetTokenType(sb.ToString());
                                    Token t = new Token(tp, sb.ToString(), line, indexOnLine - sb.Length);
                                    tokens.Add(t);
                                    sb.Clear();
                                }
                                if (ch != ' ' && ch != '\t')
                                {
                                    Token t = new Token();
                                    t.TokenType = -1;
                                    t.Content = new string(ch, 1);
                                    t.Line = line;
                                    t.Column = indexOnLine;
                                    tokens.Add(t);
                                }
                            }
                            break;
                        default:
                            {
                                sb.Append(ch);
                            }
                            break;
                    }
                }
            }
            return tokens;
        }

        private int GetTokenType(string lex)
        {
            ExternToken result = null;
            mTokenMap.TryGetValue(lex, out result);
            return result == null ? 0 : result.TokenType;
        }
    }
}
