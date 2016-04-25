using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//public class CharTokenType
//{
//    public const int None = 10000;
//    public const int Simion = 10002;//;
//    public const int Return = 10003;///n
//    public const int Dot = 10004;//.
//    public const int Equal = 10005;//=
//    public const int LeftBrack = 10006;//{
//    public const int RightBrack = 10007;//}
//    public const int Slash = 10008;///
//    public const int Table = 10009;///t
//    public const int Space = 100010;// empty
//    public const int Comment = 10011;// //
//};
namespace AGrammar
{

    public class TokenParam
    {
        public int TokenType;
        public string Content;
    }
    public class Token
    {
        public int TokenType;
        public string Content;
        public int line;
        public int indexOnLine;
        public Token() { }
        public Token(int tt, string content, int line, int indexOnLine)
        {
            this.TokenType = tt;
            this.Content = content;
            this.line = line;
            this.indexOnLine = indexOnLine;
        }
        public override string ToString()
        {
            return string.Format("{0} [{1}]", Content, TokenType.ToString());
            //return string.Format("<{0}:{1}>", TokenType.ToString(), Content, line, indexOnLine);
            //return string.Format("<TokenType({0})Content({1})Line({2})Col({3})>", TokenType.ToString(), Content, line, indexOnLine);
        }
    }

    public class Scanner
    {
        //public static CharToken[] CharTokens = new CharToken[]
        //{
        //    new CharToken(){TokenType= CharTokenType.Dot,Content='.'},
        //    new CharToken(){TokenType= CharTokenType.Equal,Content='='},
        //    new CharToken(){TokenType= CharTokenType.Simion,Content=';'},
        //    new CharToken(){TokenType= CharTokenType.LeftBrack,Content='{'},
        //    new CharToken(){TokenType= CharTokenType.RightBrack,Content='}'},
        //    new CharToken(){TokenType= CharTokenType.Table,Content='\t'},
        //    new CharToken(){TokenType= CharTokenType.Space,Content=' '},
        //    new CharToken(){TokenType= CharTokenType.Return,Content='\n'},
        //    new CharToken(){TokenType= CharTokenType.Slash,Content='/'},
        //};
        public const int ID = 0;
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
                if (item.TokenType == ID)
                {
                    Error("ID o is reserver for global id.");
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
        public List<Token> Scan(TokenParam[] tokenParams, string content)
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
                //int charTokenType = GetCharTokenType(ch);
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
                                    int tp = GetStringTokenType(sb.ToString());
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
                        case ' ':
                        case '\t':
                        case '.':
                        case ':':
                        case ';':
                            {
                                if (sb.Length > 0)
                                {
                                    int tp = GetStringTokenType(sb.ToString());
                                    Token t = new Token(tp, sb.ToString(), line, indexOnLine - sb.Length);
                                    tokens.Add(t);
                                    sb.Clear();
                                }
                                if (ch != ' ' && ch != '\t')
                                {
                                    Token t = new Token();
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

        //int GetCharTokenType(char ch)
        //{
        //    for (int i = 0; i < CharTokens.Length; ++i)
        //    {
        //        if (ch == CharTokens[i].Content)
        //        {
        //            return CharTokens[i].TokenType;
        //        }
        //    }
        //    return CharTokenType.None;
        //}

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

