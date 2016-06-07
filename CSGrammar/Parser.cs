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
            var OP2 = g.Or("OP2");
            var OP3 = g.Or("OP3");
            var OP4 = g.Or("OP4");
            var LHS = g.Or("LHS");
            var Decl = g.And("Decl");
            var Return = g.And("Return");
            var FunBody = g.Or("FunBody");

            var PermissionClass = g.Or("PermissionClass");
            var PermissionV = g.Or("PermissionV");

            var MemoryFun = g.Or("MemoryFun");
            var MemoryMember = g.Or("MemoryMember");

            var FunRetType = g.Or("FunRetType");
            var MemberType = g.Or("MemberType");

            var FunArg = g.And("FunArg");

            var ArgFirst = g.And("ArgFirst");
            var ArgTrial = g.And("ArgTrial");
            var Argument = g.And("Argument");
            var Arguments = g.Or("Arguments");

            var Fun = g.And("Fun");
            var Member1 = g.And("Member1");
            var Member2 = g.And("Member2");
            var ClassBody = g.Or("ClassBody");
            var Class = g.And("Class");
            var Root = g.SectionOr("Root");

            PermissionV.IsOneOf(TokenID.Permission, Grammar.Empty);
            PermissionClass.IsOneOf(TokenID.Permission, Grammar.Empty);
            
            MemoryFun.IsOneOf(TokenID.Memory, Grammar.Empty);
            FunRetType.IsOneOf(TokenID.InnerType, Grammar.ID);

            MemoryMember.IsOneOf(TokenID.Memory, Grammar.Empty);
            MemberType.IsOneOf(TokenID.InnerType, Grammar.ID);

            ArgFirst.Is(Arg.P("ArgType", FunRetType), Arg.P("ArgV", Grammar.ID));
            ArgTrial.Is(",", Arg.P("ArgType", FunRetType), Arg.P("ArgV", Grammar.ID)).Array();

            Argument.Is(ArgFirst, ArgTrial);
            Arguments.IsOneOf(Grammar.Empty, Argument);

            var Exp1 = g.And("Exp1");
            var Exp2 = g.And("Exp2");

            var RHS1 = g.Or("RHS1");
            var RHS = g.Or("RHS");

            var LP = Arg.P("LP", "(");
            var RP = Arg.P("RP", ")");

            var V = g.And("V").Is(Arg.P("V", Grammar.ID));
            ///function
            {
                OP2.IsOneOf(Arg.P("OP", "+="), Arg.P("OP", "-="), Arg.P("OP", "*="), Arg.P("OP", "/="));
                OP3.IsOneOf(Arg.P("OP3+", "+"), Arg.P("OP3-", "-"), Arg.P("OP4*", "*"), Arg.P("OP4/", "/"));
                //OP4.IsOneOf(Arg.P("OP4*", "*"), Arg.P("OP4/", "/"));

                //RHS1 |= V;
                //RHS1 |= Arg.And(LP, V, RP);
                //RHS1 |= Arg.And(V, Arg.And(OP3, RHS1).Array());
                //RHS1 |= Arg.And(LP, RHS1, RP);

                //RHS |= Arg.And(RHS1, Arg.And(OP4, RHS1).Array());
                //RHS |= Arg.And(LP, RHS, RP);


                RHS |= V;
                RHS |= Arg.And(LP, V, RP);
                RHS |= Arg.And(V, Arg.And(OP3, RHS).Array());
                RHS |= Arg.And(LP, RHS, RP);

                Decl.Is(Arg.P("T", FunRetType), Arg.P("V", Grammar.ID));

                LHS.IsOneOf(Decl, Arg.P("VL", Grammar.ID));

                Exp1.Is(Arg.P("LeftExp", LHS), "=", Arg.P("RExp", RHS), ";");

                Exp2.Is(Arg.P("LeftExp", Grammar.ID), Arg.P("OP2", OP2), Arg.P("RExp", RHS), ";");

                Return.Is(TokenID.Return, RHS, ";");
            }

            FunBody.IsOneOf(Exp1, Exp2, Return, Grammar.Empty).Array();

            Fun.Is(Arg.P("Per", PermissionV)
                , Arg.P("Memory", MemoryFun)
                , Arg.P("RET", FunRetType)
                , Arg.P("Name", Grammar.ID)
                , "(", Arguments, ")"
                , "{", FunBody, "}");

            Member1.Is(Arg.P("Per", PermissionV), MemoryMember, Arg.P("Type", MemberType), Arg.P("Name", Grammar.ID), ";");

            ClassBody.IsOneOf(Member1, Fun, Grammar.Empty).Array();

            Class.Is(PermissionClass, TokenID.Class, Arg.P("Name", Grammar.ID), "{", ClassBody, "}");

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
