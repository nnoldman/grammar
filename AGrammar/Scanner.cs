using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace AGrammar
{

    public class TokenParam
    {
        public int TokenType;
        public string Content;
    }
    public class GrammarToken
    {
        public int TokenType;
        public string Content;
        public int line;
        public int indexOnLine;
        public int index;

        public GrammarToken() { }
        public GrammarToken(int tt, string content, int line, int indexOnLine)
        {
            this.TokenType = tt;
            this.Content = content;
            this.line = line;
            this.indexOnLine = indexOnLine;
        }
        public override string ToString()
        {
            return string.Format("{0} [{1}]", Content);
        }
    }

    public class Scanner
    {
        public Action<string> ErrorHandler;

        Dictionary<string, TokenParam> mTokenMap = new Dictionary<string, TokenParam>();
        void Error(string msg)
        {
            if (ErrorHandler != null)
                ErrorHandler(msg);
        }
        public void ClearTokenMap()
        {
            mTokenMap.Clear();
        }
        public bool Init(TokenParam[] tokens)
        {
            ClearTokenMap();

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
        public List<GrammarToken> Scan(TokenParam[] tokenParams, string content)
        {
            if (!Init(tokenParams))
                return null;

            content = content.Replace("\r\n", "\n");

            List<GrammarToken> tokens = new List<GrammarToken>();
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
                                        int tp = GetStringTokenType(sb.ToString());
                                        GrammarToken t = new GrammarToken(tp, sb.ToString(), line, indexOnLine - sb.Length);
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
                                    int tp = GetStringTokenType(sb.ToString());
                                    GrammarToken t = new GrammarToken(tp, sb.ToString(), line, indexOnLine - sb.Length);
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
                        case ' ':
                        case '\t':
                        case '.':
                        case ':':
                        case ';':
                            {
                                if (sb.Length > 0)
                                {
                                    int tp = GetStringTokenType(sb.ToString());
                                    GrammarToken t = new GrammarToken(tp, sb.ToString(), line, indexOnLine - sb.Length);
                                    tokens.Add(t);
                                    sb.Clear();
                                }
                                if (ch != ' ' && ch != '\t')
                                {
                                    GrammarToken t = new GrammarToken();
                                    t.TokenType = 0;
                                    t.Content = new string(ch, 1);
                                    t.line = line;
                                    t.indexOnLine = indexOnLine;
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

        private int GetStringTokenType(string lex)
        {
            TokenParam result = null;
            mTokenMap.TryGetValue(lex, out result);
            if (result == null)
            {
                return 0;
            }
            return result.TokenType;
        }

        private static bool isStr(string lex)
        {
            if (lex.ElementAt(0) != '\"' || lex.ElementAt(lex.Length - 1) != '\"')
                return false;
            for (int i = 1; i <= lex.Length - 2; i++)
            {
                if (lex.ElementAt(i) == '\"')
                {
                    return false;
                }
            }
            return true;
        }
        private static bool isNumber(string str)
        {
            for (int i = 0; i < str.Length; ++i)
            {
                if (str[i] < '0')
                    return false;
                if (str[i] > '9')
                    return false;
            }
            return true;
        }
    }
}
