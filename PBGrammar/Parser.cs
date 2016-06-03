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

        public class TokenType
        {
            public const int Package = 1;
            public const int Message = 2;
            public const int TypeName = 3;
            public const int Option = 4;
            public const int Repeated = 5;
            public const int Comment = 6;
        }
        static KeyWord[] Tokens = new KeyWord[]
        {
            new KeyWord(){WordType= TokenType.Comment,Word="//"},
            new KeyWord(){WordType= TokenType.Package,Word="package"},
            new KeyWord(){WordType= TokenType.Message,Word="message"},
            new KeyWord(){WordType= TokenType.TypeName,Word="int32"},
            new KeyWord(){WordType= TokenType.TypeName,Word="uint32"},
            new KeyWord(){WordType= TokenType.TypeName,Word="string"},
            new KeyWord(){WordType= TokenType.TypeName,Word="uint64"},
            new KeyWord(){WordType= TokenType.TypeName,Word="bytes"},
            new KeyWord(){WordType= TokenType.Option,Word="optional"},
            new KeyWord(){WordType= TokenType.Repeated,Word="repeated"},
            new KeyWord(){WordType= TokenType.TypeName,Word="int64"},
        };
        void Loader(Grammar g)
        {
            g.And("package").Is(TokenType.Package, Grammar.ID, ".", Grammar.ID, ";");
            g.Or("typename").IsOneOf(TokenType.TypeName, Grammar.ID);
            g.Or("condition").IsOneOf(TokenType.Option, TokenType.Repeated);
            g.And("member").Is(Arg.P("condition", "<condition>"), Arg.P("typename", "<typename>"), Arg.P("name", Grammar.ID), "=", Arg.P("id", Grammar.ID), ";");
            g.Or("message_body").IsOneOf("<member>", Grammar.Empty).Array();
            g.Or("msg_ter").IsOneOf(";", Grammar.Empty);
            g.And("message").Is(TokenType.Message, Arg.P("name", Grammar.ID), "{", "<message_body>", "}", "<msg_ter>");
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
