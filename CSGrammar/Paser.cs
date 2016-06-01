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
    public class Paser
    {
        Grammar g = new Grammar();
        bool mInit = false;
        GrammarTree mTree;

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
            //g.Add("class_func").Is();

            g.Add("class_mem_non_init").Is(Arg.One(TokenID.Permission, Grammar.Empty)
                , Arg.One(TokenID.Memory, Grammar.Empty)
                , Arg.One(TokenID.InnerType, Grammar.ID)
                , Arg.Prop("mem_name", Grammar.ID)
                , ";");

            //g.Add("class_mem").Is(Arg.One(g.Get("class_mem_non_init")));
            //g.Add("class_body").Is(Arg.One(g.Get("class_func").Array(), g.Get("class_mem_non_init").Array()));
            g.Add("class_body").Is(g.Get("class_mem_non_init").Array());
            g.Add("class").Is(Arg.One(TokenID.Permission, Grammar.Empty), TokenID.Class, Arg.Prop("class_name", Grammar.ID), "{", g.Get("class_body"), "}");
        }
        public GrammarTree Load(Action<string> errorHandler, string content)
        {
            if (!mInit)
            {
                g.ExternTokens = Tokens;
                g.ErrorHandler = errorHandler;
                g.LoadExpression(Loader);
                mInit = true;
            }

            DateTime t0 = DateTime.Now;
            mTree = g.Generate(content);
            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - t0.Ticks);
            Debug.WriteLine(string.Format("Time:{0:00}:{1:00}:{2:00}:{3:00}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds));
            return mTree;
        }

        public void OutPut(string fileName)
        {
            if (!mTree)
                return;
            StringBuilder sb = new StringBuilder();
            mTree.WriteTo(sb);
            File.WriteAllBytes(fileName, new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
        }
    }
}
