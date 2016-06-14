using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Scanner
    {
        internal enum State
        {
            None,
            Lettering,
            Numbering,
        }

        protected Grammar mGrammar;
        protected TerminalToken CurrentTerminal;
        protected NumberBuilder NumberBuilder;

        protected string mContent;
        protected int mPosition = -1;
        protected int mLine = 1;
        protected int mCol = 1;
        protected int OldPosition = 0;

        protected char Peek
        {
            get { return mContent[mPosition]; }
        }
        protected string Content
        {
            get { return mContent; }
        }
        protected int Position
        {
            get { return mPosition; }
            set { mPosition = value; }
        }
        protected int Line
        {
            get { return mLine; }
        }
        protected int Column
        {
            get { return mCol; }
        }

        public delegate Token MakeToken(Scanner scanner);
        public delegate bool CheckFunc(Scanner scanner);

        internal class Translation
        {
            internal State Cur;
            internal State Nxt;
            internal CheckFunc Checker;
            internal MakeToken Maker;
        }

        public static Translation[] Tables = new Translation[]
        {
            new Translation(){Cur=State.None,Nxt=State.None,Checker=IsTerminal,Maker= MakeTerminal},
            new Translation(){Cur=State.None,Nxt=State.Lettering,Checker=IsLetter},
            new Translation(){Cur=State.None,Nxt=State.Numbering,Checker=IsNumber},
            new Translation(){Cur=State.Numbering,Nxt=State.None,Checker=IsNumberEnd,Maker= MakeNumber},
            new Translation(){Cur=State.Lettering,Nxt=State.None,Checker=IsLetterEnd,Maker= MakeLetter},
        };

        protected bool ReadChar()
        {
            if (mPosition + 1 < mContent.Length)
            {
                mPosition++;
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

        static bool IsLetter(Scanner scanner)
        {
            if (Helper.IsLetter(scanner.Peek))
            {
                scanner.OldPosition = scanner.Position;
                return true;
            }
            return false;
        }
        static bool IsNumberEnd(Scanner scanner)
        {
            return scanner.NumberBuilder.Push(scanner.Peek);
        }

        static bool IsTerminal(Scanner scanner)
        {
            if (scanner.Peek == '\n')
            {
                LineFeedToken token = new LineFeedToken();
                token.Line = scanner.Line;
                token.Column = scanner.Column;
                scanner.CurrentTerminal = token;
                return true;
            }

            int len = scanner.GetTerminal();

            if (len > 0)
            {
                TerminalToken token = new TerminalToken();
                token.Word = scanner.Content.Substring(scanner.Position, len);
                token.Line = scanner.Line;
                token.Column = scanner.Column;
                scanner.CurrentTerminal = token;
                return true;
            }
            return false;
        }
        static bool IsNumber(Scanner scanner)
        {
            if (Helper.IsDigital(scanner.Peek))
            {
                scanner.NumberBuilder = new NumberBuilder();
                scanner.NumberBuilder.Push(scanner.Peek);
                scanner.OldPosition = scanner.Position;
                return true;
            }
            return false;
        }
        static bool IsLetterEnd(Scanner scanner)
        {
            if (scanner.Peek == '\t' || scanner.Peek == ' ')
                return true;

            return IsTerminal(scanner);
        }
        static Token MakeLetter(Scanner scanner)
        {
            Token t = null;

            string word = string.Empty;

            if (scanner.CurrentTerminal)
                word = scanner.Content.Substring(scanner.OldPosition, scanner.Position - scanner.OldPosition - scanner.CurrentTerminal.Word.Length + 1);
            else
                word = scanner.Content.Substring(scanner.OldPosition, scanner.Position - scanner.OldPosition);

            int tp = scanner.GetTokenType(word);

            if (tp == Grammar.ID)
            {
                t = new IDToken() { Word = word, Line = scanner.Line, Column = scanner.Column - word.Length };
            }
            else
            {
                t = new KeyWordToken() { Tag = tp, Word = word, Line = scanner.Line, Column = scanner.Column - word.Length };
            }
            return t;
        }
        static Token MakeTerminal(Scanner scanner)
        {
            scanner.Position += scanner.CurrentTerminal.Word.Length - 1;
            return null;
        }

        static Token MakeNumber(Scanner scanner)
        {
            NumberToken token = scanner.NumberBuilder.GetToken();
            scanner.Position--;
            scanner.NumberBuilder = null;
            token.Line = scanner.Line;
            token.Column = scanner.Column - token.Word.Length;
            return token;
        }

        Token GetToken(ref State state)
        {
            foreach (var trans in Tables)
            {
                if (trans.Cur == state)
                {
                    if (trans.Checker != null && trans.Checker(this))
                    {
                        state = trans.Nxt;
                        if (trans.Maker != null)
                            return trans.Maker(this);
                        break;
                    }
                }
            }
            return null;
        }
        internal List<Token> Scan(Grammar grammar, string content)
        {
            mContent = content.Replace("\r\n", "\n");
            mGrammar = grammar;

            List<Token> tokens = new List<Token>();

            mLine = 1;
            mCol = 1;

            State state = State.None;

            while (ReadChar())
            {
                Token token = GetToken(ref state);

                if (token || CurrentTerminal)
                    OldPosition = mPosition;

                if (token)
                {
                    tokens.Add(token);
                }
                if (CurrentTerminal)
                {
                    tokens.Add(CurrentTerminal);
                    CurrentTerminal = null;
                }

                if (Peek == '\n')
                {
                    mCol = 1;
                    mLine++;
                }
                else
                {
                    mCol++;
                }
            }
            return tokens;
        }

        protected int GetTerminal()
        {
            for (int j = 0; j < mGrammar.config.scanner.terminals.Length; ++j)
            {
                string ter = mGrammar.config.scanner.terminals[j];
                if (CurrentIs(ter))
                    return ter.Length;
            }
            return 0;
        }

        protected int GetTokenType(string lex)
        {
            return mGrammar.config.scanner.GetKeywordToken(lex);
        }
    }
}
