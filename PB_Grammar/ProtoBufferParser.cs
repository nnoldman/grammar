using AGrammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_Grammar
{
    class ProtoBufferParser
    {
        public class PB
        {
            public const int Package = 1;
            public const int Message = 2;
            public const int TypeName = 3;
            public const int Option = 4;
            public const int Repeated = 5;
            public const int Comment = 6;
        }
        static TokenParam[] Tokens = new TokenParam[]
        {
            new TokenParam(){TokenType= PB.Comment,Content="//"},
            new TokenParam(){TokenType= PB.Package,Content="package"},
            new TokenParam(){TokenType= PB.Message,Content="message"},
            new TokenParam(){TokenType= PB.TypeName,Content="int32"},
            new TokenParam(){TokenType= PB.TypeName,Content="uint32"},
            new TokenParam(){TokenType= PB.TypeName,Content="string"},
            new TokenParam(){TokenType= PB.TypeName,Content="uint64"},
            new TokenParam(){TokenType= PB.TypeName,Content="bytes"},
            new TokenParam(){TokenType= PB.Option,Content="optional"},
            new TokenParam(){TokenType= PB.Repeated,Content="repeated"},
            new TokenParam(){TokenType= PB.TypeName,Content="int64"},
        };
        void Loader(Grammar g)
        {
            g.Add("package").Is(PB.Package, Grammar.ID, ".", Grammar.ID, ";");
            g.Add("typename").Is(Arg.One(PB.TypeName, Grammar.ID));
            g.Add("condtion").Is(Arg.One(PB.Option, PB.Repeated));
            g.Add("member").Is(Arg.Prop("condtion", g.Get("condtion")), Arg.Prop("typename", g.Get("typename")), Arg.Prop("member_name", Grammar.ID), "=", Arg.Prop("member_id", Grammar.ID), ";");
            g.Add("message_body").Is(Arg.One(g.Get("member").Array(), Grammar.Empty));
            g.Add("message").Is(PB.Message, Arg.Prop("message_name", Grammar.ID), "{", g.Get("message_body"), "}", Arg.One(";", Grammar.Empty));
        }
        public bool Load()
        {
            Grammar g = new Grammar();

            g.ErrorHandler = HandleError;
            g.TokenParams = Tokens;

            g.LoadExpression(Loader);

            string content = File.ReadAllText("commondData.proto");

            var tree = g.Generate(content);

            OutPut(tree);

            return true;
        }
        void OutPut(GrammarTree tree)
        {
            StringBuilder sb = new StringBuilder();
            tree.Write(sb, 0);
            File.WriteAllBytes("cmdxxx.lua", new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
            Console.Write(sb.ToString());
        }
        void HandleError(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
