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
        static ExternToken[] Tokens = new ExternToken[]
        {
            new ExternToken(){TokenType= PB.Comment,Content="//"},
            new ExternToken(){TokenType= PB.Package,Content="package"},
            new ExternToken(){TokenType= PB.Message,Content="message"},
            new ExternToken(){TokenType= PB.TypeName,Content="int32"},
            new ExternToken(){TokenType= PB.TypeName,Content="uint32"},
            new ExternToken(){TokenType= PB.TypeName,Content="string"},
            new ExternToken(){TokenType= PB.TypeName,Content="uint64"},
            new ExternToken(){TokenType= PB.TypeName,Content="bytes"},
            new ExternToken(){TokenType= PB.Option,Content="optional"},
            new ExternToken(){TokenType= PB.Repeated,Content="repeated"},
            new ExternToken(){TokenType= PB.TypeName,Content="int64"},
        };
        void Loader(Grammar g)
        {
            g.Add("package").Is(PB.Package, Grammar.ID, ".", Grammar.ID, ";");
            g.Add("typename").Is(Arg.One(PB.TypeName, Grammar.ID));
            g.Add("condition").Is(Arg.One(PB.Option, PB.Repeated));
            g.Add("member").Is(Arg.Prop("condition", g.Get("condition")), Arg.Prop("typename", g.Get("typename")), Arg.Prop("member_name", Grammar.ID), "=", Arg.Prop("member_id", Grammar.ID), ";");
            g.Add("message_body").Is(Arg.One(g.Get("member").Array(), Grammar.Empty));
            g.Add("message").Is(PB.Message, Arg.Prop("message_name", Grammar.ID), "{", g.Get("message_body"), "}", Arg.One(";", Grammar.Empty));
        }
        public bool Load()
        {
            DateTime t0 = DateTime.Now;

            Grammar g = new Grammar();

            g.ErrorHandler = HandleError;
            g.ExternTokens = Tokens;

            g.LoadExpression(Loader);

            string content = File.ReadAllText("commondData.proto");

            var tree = g.Generate(content);

            TimeSpan span = new TimeSpan(DateTime.Now.Ticks - t0.Ticks);
            Debug.WriteLine(string.Format("Time:{0:00}:{1:00}:{2:00}:{3:00}", span.Hours, span.Minutes, span.Seconds, span.Milliseconds));

            OutPut(tree);

            return true;
        }
        void OutPut(GrammarTree tree)
        {
            if (!tree)
                return;
            StringBuilder sb = new StringBuilder();
            tree.WriteTo(sb);
            File.WriteAllBytes("cmdxxx.lua", new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
            //Debug.Write(sb.ToString());
        }
        void HandleError(string msg)
        {
            Debug.WriteLine(msg);
        }
    }
}
