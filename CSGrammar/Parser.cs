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
            public const int Equal = 20;
            public const int Op = 21;
            public const int Equal2 = 22;
            public const int Op2 = 23;
        }
        static KeyWord[] Tokens = new KeyWord[]
        {
            //new KeyWord(){WordType= TokenID.Comment,Word="//"},
            new KeyWord(){WordType= TokenID.Using,Word="using"},
            new KeyWord(){WordType= TokenID.Namespace,Word="namespace"},
            new KeyWord(){WordType= TokenID.Class,Word="class"},
            new KeyWord(){WordType= TokenID.Permission,Word="public"},
            new KeyWord(){WordType= TokenID.Permission,Word="protected"},
            new KeyWord(){WordType= TokenID.Permission,Word="private"},
            new KeyWord(){WordType= TokenID.Memory,Word="static"},
            new KeyWord(){WordType= TokenID.New,Word="new"},

            new KeyWord(){WordType= TokenID.InnerType,Word="void"},
            new KeyWord(){WordType= TokenID.InnerType,Word="int"},
            new KeyWord(){WordType= TokenID.InnerType,Word="uint"},
            new KeyWord(){WordType= TokenID.InnerType,Word="string"},
            new KeyWord(){WordType= TokenID.InnerType,Word="bool"},
            new KeyWord(){WordType= TokenID.InnerType,Word="Int64"},
            new KeyWord(){WordType= TokenID.InnerType,Word="UInt64"},
            new KeyWord(){WordType= TokenID.InnerType,Word="float"},
            new KeyWord(){WordType= TokenID.InnerType,Word="double"},
            new KeyWord(){WordType= TokenID.InnerType,Word="long"},
            new KeyWord(){WordType= TokenID.InnerType,Word="ulong"},
            
            //new KeyWord(){WordType= TokenID.Op,Word="+"},
            //new KeyWord(){WordType= TokenID.Op,Word="-"},
            //new KeyWord(){WordType= TokenID.Op,Word="*"},
            //new KeyWord(){WordType= TokenID.Op,Word="/"},

            //new KeyWord(){WordType= TokenID.Equal2,Word="+="},
            //new KeyWord(){WordType= TokenID.Equal2,Word="-="},
            //new KeyWord(){WordType= TokenID.Equal2,Word="*="},
            //new KeyWord(){WordType= TokenID.Equal2,Word="/="},

            //new KeyWord(){WordType= TokenID.Op2,Word="||"},
            //new KeyWord(){WordType= TokenID.Op2,Word="&&"},
            //new KeyWord(){WordType= TokenID.Op2,Word="=="},
            //new KeyWord(){WordType= TokenID.Op2,Word="!="},
            //new KeyWord(){WordType= TokenID.Op2,Word="++"},
            //new KeyWord(){WordType= TokenID.Op2,Word="--"},

            //new KeyWord(){WordType= TokenID.Equal,Word="="},
        };

        void LoadFunction(Grammar g)
        {
            g.Or("op").IsOneOf("+", "-", "*", "/");
            g.Or("op2").IsOneOf("+=", "-=", "*=", "/=");

            g.And("vr1").Is(Arg.P("OP", "<op>"), Arg.P("VR", Grammar.ID)).Array();

            g.And("vr3").Is(Grammar.ID, Arg.P("R", "<vr1>"));

            g.Or("right_exp").IsOneOf("<vr3>", Grammar.ID);

            g.And("declaration").Is(Arg.P("T", "<type>"), Arg.P("V", Grammar.ID));
            
            g.Or("left_exp").IsOneOf("<declaration>", Grammar.ID);

            g.And("exp1").Is(Arg.P("LE", "<left_exp>"), "=", Arg.P("RE", "<right_exp>"), ";");

            g.And("exp2").Is(Arg.P("LE", Grammar.ID), Arg.P("OP", "<op2>"), Arg.P("RE", "<right_exp>"), ";");

            g.Or("fun_body").IsOneOf("<exp1>", "<exp2>", Grammar.Empty).Array();
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
