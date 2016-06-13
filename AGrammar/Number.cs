using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Number
    {
        internal enum State
        {
            Non,
            Dot,//when dot
            E_e,//when E/e
            A_S,//when +/-
            D_d,//when D/d
            F_f,//when F/f
            F_V,//when terminals
            D_V,//when terminals
            Int,//when terminals
        }

        public delegate NumberToken Make(string content, int start, int end);

        internal class Translation
        {
            internal State Cur;
            internal State Nxt;
            internal Make Maker;
            internal char[] Chars;
        }

        public static Translation[] Tables = new Translation[]
        {
            new Translation(){ Cur= State.Non,Nxt=State.Dot,Chars=new char[]{'.'}},

            new Translation(){ Cur= State.Dot,Nxt=State.E_e,Chars=new char[]{'E','e'}},

            new Translation(){ Cur= State.E_e,Nxt=State.A_S,Chars=new char[]{'+','-'}},

            new Translation(){ Cur= State.A_S,Nxt=State.D_d,Chars=new char[]{'D','d'}},

            new Translation(){ Cur= State.A_S,Nxt=State.F_f,Chars=new char[]{'F','f'}},

            new Translation(){ Cur= State.Non,Nxt=State.D_d,Chars=new char[]{'D','d'}},

            new Translation(){ Cur= State.Non,Nxt=State.F_f,Chars=new char[]{'F','f'}},
            
            new Translation(){ Cur= State.Non,Nxt=State.E_e,Chars=new char[]{'E','e'} },
            
            new Translation(){ Cur= State.D_d,Nxt=State.D_V,Maker=MakeDoubleToken},

            new Translation(){ Cur= State.F_f,Nxt=State.F_V,Maker=MakeFloatToken},

            new Translation(){ Cur= State.Non,Nxt=State.Int,Maker=MakeIntToken},
            
            new Translation(){ Cur= State.Dot,Nxt=State.D_d,Maker=MakeDoubleToken},
        };

        static NumberToken MakeIntToken(string content, int s, int end)
        {
            IntToken token = new IntToken();
            token.Word = content.Substring(s, end - s);
            token.value = int.Parse(token.Word);
            return token;
        }

        static NumberToken MakeFloatToken(string content, int s, int end)
        {
            FloatToken token = new FloatToken();
            token.Word = content.Substring(s, end - s);
            char ch = token.Word[token.Word.Length - 1];
            token.value = float.Parse(ch == 'F' || ch == 'f' ?
                token.Word.Substring(0, token.Word.Length - 1) : token.Word);
            return token;
        }

        static NumberToken MakeDoubleToken(string content, int s, int end)
        {
            DoubleToken token = new DoubleToken();
            token.Word = content.Substring(s, end - s);
            char ch = token.Word[token.Word.Length - 1];
            token.value = double.Parse(ch == 'D' || ch == 'd' ?
               token.Word.Substring(0, token.Word.Length - 1) : token.Word);
            return token;
        }
        static bool IsIn(char ch, params char[] chars)
        {
            for (int i = 0; i < chars.Length; ++i)
                if (ch == chars[i])
                    return true;
            return false;
        }

        static bool IsTerminal(string content, int s)
        {
            char ch = content[s];
            if (Helper.IsDigital(ch) || Helper.IsLetter(ch))
                return false;
            return true;
        }

        static NumberToken MatchTables(string content, int i, int start, ref State state)
        {
            foreach (var trans in Tables)
            {
                if (trans.Cur == state)
                {
                    char cur = content[i];

                    if ((trans.Chars != null && IsIn(cur, trans.Chars))
                        || (trans.Chars == null && IsTerminal(content, i)))
                    {
                        state = trans.Nxt;
                        if (trans.Maker != null)
                            return trans.Maker(content, start, i);
                        break;
                    }
                    else if (Helper.IsDigital(cur))
                    {
                        break;
                    }
                }
            }
            return null;
        }

        internal static NumberToken Parse(string content, int start)
        {
            State state = State.Non;

            for (int i = start; i < content.Length; ++i)
            {
                NumberToken token = MatchTables(content, i, start, ref state);
                if (token)
                    return token;
            }
            throw new Exception();
        }
    }
}
