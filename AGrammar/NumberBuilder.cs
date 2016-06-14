using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
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
    internal delegate NumberToken Make(string content, int start, int end);

    internal class Translation
    {
        internal State Cur;
        internal State Nxt;
        internal Make Maker;
        internal char[] Chars;
    }

    internal class NumberBuilder
    {
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
            
            new Translation(){ Cur= State.D_d,Nxt=State.D_V},

            new Translation(){ Cur= State.F_f,Nxt=State.F_V},

            new Translation(){ Cur= State.Non,Nxt=State.Int},
            
            new Translation(){ Cur= State.Dot,Nxt=State.D_d},
        };


        State mState;

        bool mClosed = false;

        string mContent = string.Empty;

        bool IsEnd()
        {
            return mState == State.D_V || mState == State.F_V || mState == State.Int || mState == State.D_d;
        }

        public NumberToken GetToken()
        {
            NumberToken number = null;
            Debug.Assert(mClosed);
            switch (mState)
            {
                case State.D_V:
                case State.D_d:
                    {
                        DoubleToken token = new DoubleToken();
                        token.Word = mContent;
                        char ch = token.Word[token.Word.Length - 1];
                        token.value = double.Parse(ch == 'D' || ch == 'd' ?
                           token.Word.Substring(0, token.Word.Length - 1) : token.Word);
                        number = token;
                    }
                    break;
                case State.F_V:
                    {
                        FloatToken token = new FloatToken();
                        token.Word = mContent;
                        char ch = token.Word[token.Word.Length - 1];
                        token.value = float.Parse(ch == 'F' || ch == 'f' ?
                            token.Word.Substring(0, token.Word.Length - 1) : token.Word);
                        number = token;
                    }
                    break;
                case State.Int:
                    {
                        IntToken token = new IntToken();
                        token.Word = mContent;
                        token.value = int.Parse(token.Word);
                        number = token;
                    }
                    break;
            }
            return number;
        }

        public bool Push(char ch)
        {
            Debug.Assert(!mClosed);

            foreach (var trans in Tables)
            {
                if (trans.Cur == mState)
                {
                    if ((trans.Chars != null && Helper.IsIn(ch, trans.Chars))
                        || (trans.Chars == null && Helper.IsTerminal(ch)))
                    {
                        mState = trans.Nxt;

                        if (IsEnd())
                            mClosed = true;
                        else
                            mContent += ch;

                        return IsEnd();
                    }
                }
            }
            if (Helper.IsDigital(ch))
            {
                mContent += ch;
                return false;
            }
            throw new Exception();
        }
    }
}
