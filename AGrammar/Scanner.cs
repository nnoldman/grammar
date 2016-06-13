using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Scanner
    {
        enum State
        {
            None,
            LineCommenting,
            BlockCommenting,
            Scanning,
        }

        protected string mContent;
        protected int mPosition = -1;
        protected char mPeek;

        protected Grammar mGrammar;

        protected bool ReadChar()
        {
            if (mPosition + 1 < mContent.Length)
            {
                mPosition++;
                mPeek = mContent[mPosition];
                return true;
            }
            return false;
        }
        bool CurrentIs(string target)
        {
            if (target == null)
                return false;

            if (target.Length == 1)
                return mPosition < mContent.Length && mContent[mPosition] == target[0];

            if (target.Length == 2)
                return mPosition + 1 < mContent.Length && mContent[mPosition] == target[0] && mContent[mPosition + 1] == target[1];

            throw new Exception();
        }

        void AddKeyWordOrID(StringBuilder sb, int line, int col, List<Token> tokens)
        {
            if (sb.Length == 0)
                return;

            int tp = GetTokenType(sb.ToString());

            Token t = null;

            if (tp == Grammar.ID)
            {
                t = new IDToken() { Word = sb.ToString(), Line = line, Column = col - sb.Length };
            }
            else
            {
                t = new KeyWordToken() { Tag = tp, Word = sb.ToString(), Line = line, Column = col - sb.Length };
            }
            tokens.Add(t);
            sb.Clear();
        }
        void AddTerminate(string ter, int line, int col, List<Token> tokens)
        {
            TerminalToken t = new TerminalToken();
            t.Word = ter;
            t.Line = line;
            t.Column = col;
            tokens.Add(t);
        }
        bool AddNumber(int line, int col, List<Token> tokens)
        {
            NumberToken number = Number.Parse(mContent, mPosition);
            if (number)
            {
                number.Line = line;
                number.Column = col;
                tokens.Add(number);
                return true;
            }
            return false;
        }
        bool MatchTerminations(List<Token> tokens, StringBuilder sb, int line, int col)
        {
            for (int j = 0; j < mGrammar.config.scanner.terminals.Length; ++j)
            {
                string ter = mGrammar.config.scanner.terminals[j];

                if (CurrentIs(ter))
                {
                    AddKeyWordOrID(sb, line, col, tokens);
                    AddTerminate(ter, line, col, tokens);
                    return true;
                }
            }
            return false;
        }
        internal List<Token> Scan(Grammar grammar, string content)
        {
            mContent = content.Replace("\r\n", "\n");
            mGrammar = grammar;

            List<Token> tokens = new List<Token>();

            int line = 1;
            int col = 1;

            State stat = State.None;

            StringBuilder sb = new StringBuilder();

            while (ReadChar())
            {
                int offset = 1;

                if (stat == State.None || stat == State.Scanning)
                {
                    if (CurrentIs(mGrammar.config.scanner.lineComment))
                    {
                        AddKeyWordOrID(sb, line, col, tokens);
                        stat = State.LineCommenting;
                    }
                    else if (CurrentIs(mGrammar.config.scanner.blockCommentStart))
                    {
                        AddKeyWordOrID(sb, line, col, tokens);
                        stat = State.BlockCommenting;
                    }
                    else if (MatchTerminations(tokens, sb, line, col))
                    {
                        offset = tokens[tokens.Count - 1].Word.Length;
                        stat = State.None;
                    }
                    else if (mPeek == '\n' || mPeek == '\t' || mPeek == ' ')
                    {
                        AddKeyWordOrID(sb, line, col, tokens);
                        stat = State.None;
                    }
                    else if (stat == State.None && Helper.IsDigital(mPeek))
                    {
                        if (AddNumber(line, col, tokens))
                            offset = tokens[tokens.Count - 1].Word.Length;
                        else
                            return null;
                    }
                    else if (stat == State.None && Helper.IsLetter(mPeek))
                    {
                        stat = State.Scanning;
                        sb.Append(mPeek);
                    }
                    else if (stat == State.Scanning)
                    {
                        sb.Append(mPeek);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                else if (stat == State.LineCommenting)
                {
                    if (mPeek == '\n')
                        stat = State.None;
                }
                else if (stat == State.BlockCommenting)
                {
                    if (CurrentIs(mGrammar.config.scanner.blockCommentEnd))
                    {
                        stat = State.None;
                        offset = mGrammar.config.scanner.blockCommentEnd.Length;
                    }
                }

                col += offset;
                mPosition += offset - 1;

                if (mPeek == '\n')
                {
                    col = 1;
                    line++;
                }
            }
            if (tokens.Count > 0)
                tokens.Add(new EOFToken());

            return tokens;
        }
        private int GetTokenType(string lex)
        {
            return mGrammar.config.scanner.GetKeywordToken(lex);
        }
    }
}
