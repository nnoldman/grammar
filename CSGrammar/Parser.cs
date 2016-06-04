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
            public const int Return = 24;
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
            
            new KeyWord(){WordType= TokenID.Return,Word="return"},
            
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

        void Loader(Grammar g)
        {
            var V_B = g.And("V_B");
            var Var = g.Or("Var");
            var Var_B = g.And("Var_B");
            var Vars = g.Or("Vars");
            var Vars2 = g.And("Vars2");
            var Varss = g.Or("Varss");

            var OP1 = g.Or("OP1");
            var OP2 = g.Or("OP2");
            var Var_R = g.And("Var_R");
            var Var_Com = g.And("Var_Com");
            var RightExp = g.Or("RightExp");
            var RightExp1 = g.Or("RightExp1");
            var RightExp2 = g.And("RightExp2");
            var LeftExp = g.Or("LeftExp");
            var Exp1 = g.And("Exp1");
            var Exp2 = g.And("Exp2");
            var Decl = g.And("Decl");
            var Return = g.And("Return");
            var FunBody = g.Or("FunBody");
            var FunArg = g.And("FunArg");
            var Permission = g.Or("Permission");
            var Memory = g.Or("Memory");
            var VType = g.Or("VType");
            var Argument = g.And("Argument");
            var Arguments = g.Or("Arguments");
            var Fun = g.And("Fun");
            var Member1 = g.And("Member1");
            var Member2 = g.And("Member2");
            var ClassBody = g.Or("ClassBody");
            var Class = g.And("Class");
            var Root = g.SectionOr("Root");

            Permission.IsOneOf(TokenID.Permission, Grammar.Empty);
            Memory.IsOneOf(TokenID.Memory, Grammar.Empty);
            VType.IsOneOf(TokenID.InnerType, Grammar.ID);

            var ArgFirst = g.And("ArgFirst");
            var ArgTrial = g.And("ArgTrial");

            ArgFirst.Is(Arg.P("ArgType", VType), Arg.P("ArgV", Grammar.ID));
            ArgTrial.Is(",", Arg.P("ArgType", VType), Arg.P("ArgV", Grammar.ID)).Array();

            Argument.Is(ArgFirst, ArgTrial);

            Arguments.IsOneOf(Grammar.Empty, Argument);

            ///function
            {
                OP1.IsOneOf("+", "-", "*", "/");

                OP2.IsOneOf("+=", "-=", "*=", "/=");

                V_B.Is(Arg.P("(", "("), Grammar.ID, Arg.P(")", ")"));

                Var.IsOneOf(Grammar.ID, Arg.P("V", V_B));

                Var_B.Is(Arg.P("LB", "("), Var, Arg.P("Op", OP1), Var, Arg.P("RB", ")"));

                Vars.IsOneOf(Var, Var_B);

                Vars2.Is(Arg.P("LB", "("), Vars, Arg.P("Op", OP1), Vars, Arg.P("RB", ")"));

                Varss.IsOneOf(Vars, Vars2);

                Var_R.Is(Arg.P("Op", OP1), Arg.P("V", Varss)).Array();

                Var_Com.Is(Arg.P("V", Varss), Arg.P("R", Var_R));

                RightExp1.IsOneOf(Var_Com, Arg.P("V", Grammar.ID));

                RightExp2.Is(Arg.P("(", "("), RightExp1, Arg.P(")", ")"));

                RightExp.IsOneOf(RightExp1, RightExp2);

                Decl.Is(Arg.P("T", VType), Arg.P("V", Grammar.ID));

                LeftExp.IsOneOf(Decl, Arg.P("VL", Grammar.ID));

                Exp1.Is(Arg.P("LeftExp", LeftExp), "=", Arg.P("RightExp", RightExp), ";");

                Exp2.Is(Arg.P("LeftExp", Grammar.ID), Arg.P("OP2", OP2), Arg.P("RightExp", RightExp), ";");

                Return.Is(TokenID.Return, RightExp, ";");

                FunBody.IsOneOf(Exp1, Exp2, Return, Grammar.Empty).Array();
            }

            Fun.Is(Arg.P("Per", Permission)
                , Arg.P("Memory", Memory)
                , Arg.P("RET", VType)
                , Arg.P("Name", Grammar.ID)
                , "(", Arguments, ")"
                , "{", FunBody, "}");

            Member1.Is(Arg.P("Per", Permission), Memory, Arg.P("Type", VType), Arg.P("Name", Grammar.ID), ";");

            ClassBody.IsOneOf(Member1, Fun, Grammar.Empty).Array();

            Class.Is(Permission, TokenID.Class, Arg.P("Name", Grammar.ID), "{", ClassBody, "}");

            Root.IsOneOf(Class).Array();
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
