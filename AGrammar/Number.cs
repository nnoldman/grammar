using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Number
    {
        enum State
        {
            None,
            HeadT,
            TailStart,
            Tail,
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

        internal static NumberToken Parse(string content, int s)
        {
            State state = State.None;

            for (int i = s; i < content.Length; ++i)
            {
                char ch = content[i];

                if (ch == '.')
                {
                    if (state == State.None && NextIsDigital(content, i))
                        state = State.HeadT;
                    else
                        return null;
                }
                else if (ch == 'E' || ch == 'e')
                {
                    if (NextIsTerminal(content, i) && (state == State.None || state == State.HeadT || state == State.HeadT))
                    {
                        state = State.TailStart;
                    }
                    else
                    {
                        return null;
                    }
                }
                else if (ch == 'D' || ch == 'd')
                {
                    if (NextIsTerminal(content, i) && (state == State.None || state == State.Tail))
                    {
                        DoubleToken token = new DoubleToken();
                        token.Word = content.Substring(s, i - s);
                        token.value = double.Parse(token.Word);
                        token.Word += content[i];
                        return token;
                    }
                    else
                        return null;
                }
                else if (ch == 'F' || ch == 'f')
                {
                    if (NextIsTerminal(content, i) && (state == State.None || state == State.Tail))
                    {
                        FloatToken token = new FloatToken();
                        token.Word = content.Substring(s, i - s);
                        token.value = float.Parse(token.Word);
                        token.Word += content[i];
                        return token;
                    }
                    else
                        return null;
                }
                else if (ch == '+' || ch == '-')
                {
                    if (state == State.TailStart)
                        state = State.Tail;
                    else
                        return null;
                }
                else if (Helper.IsDigital(ch))
                {
                    continue;
                }
                else if (!Helper.IsDigital(ch) && !Helper.IsLetter(ch))
                {
                    if (state == State.None || state == State.HeadT)
                    {
                        IntToken token = new IntToken();
                        token.Word = content.Substring(s, i - s);
                        token.value = int.Parse(token.Word);
                        return token;
                    }
                    return null;
                }
            }
            if (state == State.None)
            {
                IntToken token = new IntToken();
                token.Word = content.Substring(s);
                token.value = int.Parse(token.Word);
                return token;
            }
            else if (state == State.HeadT)
            {
                DoubleToken token = new DoubleToken();
                token.Word = content.Substring(s);
                token.value = double.Parse(token.Word);
                return token;
            }
            return null;
        }
    }
}
