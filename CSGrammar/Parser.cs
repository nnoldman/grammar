using AGrammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSGrammar
{
    public class Parser
    {
        Grammar g = new Grammar();

        public class TokenID
        {
            /// <summary>
            /// 0 is reserved
            /// </summary>
            //public const int ID = 0;
            public const int Using = 1;
            public const int Namespace = 2;
            public const int Class = 3;
            public const int Permission = 4;
            public const int Memory = 5;
            public const int InnerType = 6;
            public const int Comment = 7;
            public const int New = 8;

            public const int If = 9;
            public const int Else = 10;
            public const int Elseif = 11;
            public const int For = 12;
            public const int Foreach = 13;
            public const int While = 14;
            public const int Do = 15;
            public const int Break = 16;
            public const int Continue = 17;
            public const int Var = 18;
            public const int Const = 19;
        }
        static ExternToken[] Tokens = new ExternToken[]
        {
            new ExternToken(){TokenType= TokenID.Comment,Content="//"},
            new ExternToken(){TokenType= TokenID.Using,Content="using"},
            new ExternToken(){TokenType= TokenID.Namespace,Content="namespace"},
            new ExternToken(){TokenType= TokenID.Class,Content="class"},
            new ExternToken(){TokenType= TokenID.Permission,Content="public"},
            new ExternToken(){TokenType= TokenID.Permission,Content="protected"},
            new ExternToken(){TokenType= TokenID.Permission,Content="private"},
            new ExternToken(){TokenType= TokenID.Memory,Content="static"},
            new ExternToken(){TokenType= TokenID.New,Content="new"},

            new ExternToken(){TokenType= TokenID.InnerType,Content="void"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="int"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="uint"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="string"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="bool"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="Int64"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="UInt64"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="float"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="double"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="long"},
            new ExternToken(){TokenType= TokenID.InnerType,Content="ulong"},
        };

        void LoadFunction(Grammar g)
        {
            g.Or("op").IsOneOf("+", "-", "*", "/");

            g.And("vr1").Is(Arg.P("VL", Grammar.ID), Arg.P("OP", "<op>"), Arg.P("VR", Grammar.ID));
            g.Or("vr2").IsOneOf(Grammar.ID, "<vr1>");
            g.And("vr3").Is(Arg.P("OP", "<op>"), Arg.P("VR", "<vr2>"));

            g.Or("vr4").IsOneOf(Arg.P("VR", "<vr3>")).Array();

            g.And("vr5").Is(Arg.P("VL", "<vr2>"), Arg.P("VR", "<vr4>"));

            g.Or("right_exp").IsOneOf("<vr5>", Grammar.ID);

            g.And("declaration").Is(Arg.P("T", "<type>"), Arg.P("V", Grammar.ID));
            
            g.Or("left_exp").IsOneOf("<declaration>", Grammar.ID);

            g.And("exp").Is(Arg.P("LE", "<left_exp>"), "=", Arg.P("RE", "<right_exp>"), ";");

            g.Or("fun_body").IsOneOf("<exp>", Grammar.Empty).Array();
        }

        void Loader(Grammar g)
        {
            g.Or("permission").IsOneOf(TokenID.Permission, Grammar.Empty);
            g.Or("memory").IsOneOf(TokenID.Memory, Grammar.Empty);
            g.Or("type").IsOneOf(TokenID.InnerType, Grammar.ID);

            g.Or("arg_ter").IsOneOf(",", Grammar.Empty);
            g.And("arg").Is(Arg.P("type", "<type>"), Arg.P("name", Grammar.ID), "<arg_ter>");
            g.Or("args").IsOneOf("<arg>", Grammar.Empty).Array();

            LoadFunction(g);

            g.And("fun").Is("<permission>"
                , "<memory>"
                , Arg.P("ret", "<type>")
                , Arg.P("name", Grammar.ID)
                , "(", "<args>", ")"
                , "{", "<fun_body>", "}");

            g.And("member1").Is("<permission>", "<memory>", Arg.P("type", "<type>"), Arg.P("name", Grammar.ID), ";");
            g.Or("class_body").IsOneOf("<member1>", "<fun>", Grammar.Empty).Array();

            g.And("class").Is("<permission>", TokenID.Class, Arg.P("name", Grammar.ID), "{", "<class_body>", "}");
            g.SecOr("grammar").IsOneOf("<class>").Array();
        }

        public GrammarTree Load(Action<string> messageHandler, string content)
        {
            return g.Generate(content, Tokens, messageHandler, Loader);
        }

        public void Dump(string file)
        {
            if (g.Tree)
                File.WriteAllBytes(file, g.OutPutDebug());
        }
    }
}
