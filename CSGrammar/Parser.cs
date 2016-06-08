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


        Production Loader()
        {
            Production Root = Factory.Or("CSGrammar");
            Production Class = Factory.And("Class").Array().Node("Class");
            Production ClassBody = Factory.Or("Class_Body").Node("ClassBody");
            Production Member = Factory.And("Member").Node("ClassMember");
            Production Func = Factory.And("Func").Node("ClassFunc");
            Production VType = Factory.Or("Type").Node();
            Production Memory = Factory.Or("Memory").Node();
            Production Permission = Factory.Or("Permission").Node("Permission");
            Production Expressions = Factory.Or("Expressions").Array();
            Production OP2 = Factory.Or("OP2").Node();
            Production OP3 = Factory.Or("OP3").Node();
            Production V = Factory.Or("V").Node();
            Production Exp = Factory.And("Exp");
            Production ExpDecl = Factory.And("ExpDecl");
            Production LHS = Factory.Or("LHS").Node("LHS");
            Production RHS = Factory.Or("RHS").Node("RHS");
            Production ComExpression = Factory.And("ComExpression");
            Production Factor = Factory.Or("Factor");
            Production ClassName = Factory.Symbol(Grammar.ID);
            Production FuncName = Factory.Symbol(Grammar.ID).Node("FuncName");
            Production MemberName = Factory.Symbol(Grammar.ID).Node("MemberName");

            Production OneArg = Factory.And("OneArg");
            Production Arg = Factory.Symbol(Grammar.ID).Node("Arg");
            Production Arguments = Factory.Or("Arguments").Node();

            Production Var = Factory.Symbol(Grammar.ID).Node("Var");
            Production Decl = Factory.And("Decl");

            Production OpAdd = Factory.Symbol("+").Node("Op");
            Production OpSub = Factory.Symbol("-").Node("Op");
            Production OpMul = Factory.Symbol("*").Node("Op");
            Production OpDiv = Factory.Symbol("/").Node("Op");
            Production VTypeID = Factory.Symbol(Grammar.ID).Node("Type");

            Production EOF = Factory.Symbol(Grammar.EOFToken);

            OP2.Add(OpAdd | OpSub );
            OP3.Add(OpMul | OpDiv);

            V.Add(Var);
            V.Add("(" + V + ")");

            Decl.Add(VType + Var);
            LHS.Add(Var | Decl);

            Factor.Add(V);
            Factor.Add(V + OP3 + Factor);
            Factor.Add("(" + Factor + ")");

            RHS.Add(V);
            RHS.Add(V + OP2 + Factor);
            RHS.Add(V + OP2 + RHS);
            RHS.Add(Factor + OP2 + RHS);
            RHS.Add("(" + RHS + ")");

            Exp.Add(LHS + "=" + RHS + ";");

            ExpDecl.Add(Decl + ";");

            Permission.Add(Grammar.Empty | TokenID.Permission);
            Memory.Add(Grammar.Empty | TokenID.Memory);
            VType.Add(VTypeID | TokenID.InnerType);

            Member.Add(Permission + Memory + VType + MemberName + ";");

            Expressions.Add(Exp | ExpDecl);

            ComExpression.Add("{" + Expressions + "}");

            OneArg.Add(VType + Arg);

            Arguments.Add(OneArg);
            Arguments.Add(OneArg + "," + Arguments);
            Arguments.Add(Grammar.Empty);

            Func.Add(Permission + Memory + VType + FuncName + "(" + Arguments + ")" + ComExpression);

            ClassBody.Add(Member | Func | Grammar.Empty);

            Class.Add(Permission + Memory + TokenID.Class + "{" + ClassName + ClassBody + "}");

            Root.Add(Class | EOF);

            return Root;
        }

        public GrammarTree Load(Action<string> messageHandler, string content)
        {
            Production root = Loader();
            return g.Generate(root, content, Tokens, messageHandler);
        }

        public void Dump(string file)
        {
            if (g.Tree)
                File.WriteAllBytes(file, g.OutPutDebug());
        }
    }
}
