using AGrammar;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBGrammar
{
    public class Parser
    {
        Grammar g = new Grammar();

        public class TokenID
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
            new ExternToken(){TokenType= TokenID.Comment,Content="//"},
            new ExternToken(){TokenType= TokenID.Package,Content="package"},
            new ExternToken(){TokenType= TokenID.Message,Content="message"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="int32"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="uint32"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="string"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="uint64"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="bytes"},
            new ExternToken(){TokenType= TokenID.Option,Content="optional"},
            new ExternToken(){TokenType= TokenID.Repeated,Content="repeated"},
            new ExternToken(){TokenType= TokenID.TypeName,Content="int64"},
        };
        void Loader(Grammar g)
        {
            g.And("package").Is(TokenID.Package, Grammar.ID, ".", Grammar.ID, ";");
            g.Or("typename").IsOneOf(TokenID.TypeName, Grammar.ID);
            g.Or("condition").IsOneOf(TokenID.Option, TokenID.Repeated);
            g.And("member").Is(Arg.Prop("condition", "<condition>"), Arg.Prop("typename", "<typename>"), Arg.Prop("name", Grammar.ID), "=", Arg.Prop("id", Grammar.ID), ";");
            g.Or("message_body").IsOneOf("<member>", Grammar.Empty).Array();
            g.Or("msg_ter").IsOneOf(";", Grammar.Empty);
            g.And("message").Is(TokenID.Message, Arg.Prop("name", Grammar.ID), "{", "<message_body>", "}", "<msg_ter>");
            g.SecOr("grammar").IsOneOf("<package>", "<message>").Array();
        }
        public GrammarTree Load(Action<string> errorHandler, string content)
        {
            return g.Generate(content, Tokens, errorHandler, Loader);
        }

        public void Dump(string file)
        {
            if (g.Tree)
                File.WriteAllBytes(file, g.OutPutDebug());
        }
    }
}
