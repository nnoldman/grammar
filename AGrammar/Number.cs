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
            Dot,//if dot
            E_e,//if E/e
            A_S,//if +/-
            D_d,//if D/d
            F_f,//if F/f
            Int,//if terminals
        }

        public delegate bool Translate(string content, int s, params char[] chars);
        public delegate NumberToken Make(string content, int s, int end);

        internal class Translation
        {
            internal State Cur;
            internal State Nxt;
            internal Translate Func;
            internal Make Make;
            internal char[] Chars;
        }

        public static Translation[] Tables = new Translation[]
        {
            new Translation(){ Cur= State.Non,Nxt= State.Dot,Func=Non2Dot,Chars=new char[]{'.'}},

            new Translation(){ Cur= State.Dot,Nxt= State.E_e,Func=Dot2E,Chars=new char[]{'E','e'}},
            new Translation(){ Cur= State.E_e,Nxt= State.A_S,Func=E2AS,Chars=new char[]{'+','-'}},

            new Translation(){ Cur= State.A_S,Nxt= State.D_d,Func=AS2DF,Chars=new char[]{'D','d'}, Make=MakeDoubleToken},
            new Translation(){ Cur= State.A_S,Nxt= State.F_f,Func=AS2DF,Chars=new char[]{'F','f'}, Make=MakeFloatToken},
            
            new Translation(){ Cur= State.Non,Nxt= State.D_d,Func=Non2DF,Chars=new char[]{'D','d'}, Make=MakeDoubleToken},
            new Translation(){ Cur= State.Non,Nxt= State.F_f,Func=Non2DF,Chars=new char[]{'F','f'}, Make=MakeFloatToken},
            
            new Translation(){ Cur= State.Non,Nxt= State.E_e,Func=Non2E,Chars=new char[]{'E','e'}, },
            
            new Translation(){ Cur= State.Non,Nxt= State.Int,Func=Non2Int,Chars=new char[]{'E','e','.'}, Make=MakeIntToken},
            
            new Translation(){ Cur= State.Dot,Nxt= State.D_d,Func=Dot2D,Chars=null, Make=MakeDoubleToken},
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
            token.value = float.Parse(token.Word);
            return token;
        }

        static NumberToken MakeDoubleToken(string content, int s, int end)
        {
            DoubleToken token = new DoubleToken();
            token.Word = content.Substring(s, end - s);
            token.value = double.Parse(token.Word);
            return token;
        }

        static bool Non2DF(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars))
                return true;
            return false;
        }

        static bool IsIn(char ch, params char[] chars)
        {
            for (int i = 0; i < chars.Length; ++i)
                if (ch == chars[i])
                    return true;
            return false;
        }

        static bool Non2E(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars))
                return true;
            return false;
        }

        static bool Non2Int(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (!IsIn(cur, chars) && !Helper.IsDigital(cur))
                return true;
            return false;
        }

        static bool Non2Dot(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars) && NextIsDigital(content, s))
                return true;
            return false;
        }
        static bool Dot2E(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars) && NextIsDigital(content, s))
                return true;
            return false;
        }

        static bool Dot2D(string content, int s, params char[] chars)
        {
            if (IsTerminal(content, s))
                return true;
            return false;
        }

        static bool E2AS(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars) && NextIsDigital(content, s))
                return true;
            return false;
        }
        static bool AS2DF(string content, int s, params char[] chars)
        {
            char cur = content[s];
            if (IsIn(cur, chars))
                return true;
            return false;
        }
        static bool NextIsDigital(string content, int s)
        {
            if (s + 1 < content.Length && Helper.IsDigital(content[s + 1]))
                return true;
            return false;
        }

        static bool NextIsTerminal(string content, int s)
        {
            if (s + 1 < content.Length)
            {
                char ch = content[s + 1];
                if (Helper.IsDigital(ch) || Helper.IsLetter(ch))
                    return false;
            }
            return true;
        }
        static bool IsTerminal(string content, int s)
        {
            char ch = content[s];
            if (Helper.IsDigital(ch) || Helper.IsLetter(ch))
                return false;
            return true;
        }
        internal static NumberToken Parse(string content, int s)
        {
            State state = State.Non;

            for (int i = s; i < content.Length; ++i)
            {
                foreach (var trans in Tables)
                {
                    if (trans.Cur == state)
                    {
                        if (trans.Func != null)
                        {
                            if (trans.Func(content, i, trans.Chars))
                            {
                                state = trans.Nxt;
                                if (trans.Make != null)
                                    return trans.Make(content, s, i);
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
