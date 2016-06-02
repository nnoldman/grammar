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

        void Loader(Grammar g)
        {
            g.Or("permission").IsOneOf(TokenID.Permission, Grammar.Empty);
            g.Or("memory").IsOneOf(TokenID.Memory, Grammar.Empty);
            g.Or("type").IsOneOf(TokenID.InnerType, Grammar.ID);

            g.Or("arg_ter").IsOneOf(",", Grammar.Empty);
            g.And("arg").Is(Arg.Prop("type", "<type>"), Arg.Prop("name", Grammar.ID), "<arg_ter>");
            g.Or("args").IsOneOf("<arg>", Grammar.Empty).Array();

            g.And("fun").Is("<permission>", "<memory>", Arg.Prop("ret", "<type>"), Arg.Prop("name", Grammar.ID), "(", "<args>", ")", "{", "}");
            g.And("member1").Is("<permission>", "<memory>", Arg.Prop("type", "<type>"), Arg.Prop("name", Grammar.ID), ";");
            g.Or("class_body").IsOneOf("<member1>", "<fun>", Grammar.Empty).Array();

            g.And("class").Is("<permission>", TokenID.Class, Arg.Prop("name", Grammar.ID), "{", "<class_body>", "}");
            g.SecOr("grammar").IsOneOf("<class>", Grammar.Empty).Array();
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
