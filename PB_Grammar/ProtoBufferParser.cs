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
        public class PBTokenType
        {
            public const int Package = 1;
            public const int Message = 2;
            public const int TypeName = 3;
            public const int Option = 4;
            public const int Repeated = 5;
            public const int Comment = 6;
        }
        static TokenParam[] mStringTokens = new TokenParam[]
        {
            new TokenParam(){TT= PBTokenType.Comment,Content="//"},
            new TokenParam(){TT= PBTokenType.Package,Content="package"},
            new TokenParam(){TT= PBTokenType.Message,Content="message"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="int32"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="uint32"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="string"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="uint64"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="bytes"},
            new TokenParam(){TT= PBTokenType.Option,Content="optional"},
            new TokenParam(){TT= PBTokenType.Repeated,Content="repeated"},
            new TokenParam(){TT= PBTokenType.TypeName,Content="int64"},
        };

        Grammar mGrammar ;

        public bool Load()
        {
            mGrammar = Grammar.Instance;
            mGrammar.ErrorHandler = HandleError;
            string content = File.ReadAllText("commondData.proto");
            var res = mGrammar.Parse(mStringTokens, content, LoadPackage);
            if (res.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                res.Peek().WriteTo(sb, 0);
                File.WriteAllBytes("cmdxxx.lua", new UTF8Encoding(false).GetBytes(sb.ToString().ToCharArray()));
            }
            return true;
        }
        void HandleError(string msg)
        {
            Debug.WriteLine(msg);
        }
        bool LoadPackage()
        {
            int old = mGrammar.Push("package");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Package)) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID)) return false;
            if (!mGrammar.NextTokenIs(old, ".")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID)) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            if (!mGrammar.NextArrayIs(old, LoadOneMessage)) return false;
            //Pop();
            return true;
        }
        bool LoadOneMessage()
        {
            //从多到少匹配
            if (LoadOneMessage2()) return true;
            if (LoadOneMessage1()) return true;
            return false;
        }
        bool LoadOneMessage1()
        {
            int old = mGrammar.Push("message");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Message)) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "messageName")) return false;
            if (!mGrammar.NextArrayIs(old, LoadOneMember, "{", "}")) return false;
            mGrammar.Pop();
            return true;
        }
        bool LoadOneMessage2()
        {
            int old = mGrammar.Push("message");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Message)) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "messageName")) return false;
            if (!mGrammar.NextArrayIs(old, LoadOneMember, "{", "}")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;//
            mGrammar.Pop();
            return true;
        }

        bool ExpressionMemberOption()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Option, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, PBTokenType.TypeName, "type")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberOption2()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Option, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "type")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberRepeat()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Repeated, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, PBTokenType.TypeName, "type")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberRepeat2()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Repeated, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "type")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, Scanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }

        bool LoadOneMember()
        {
            if (ExpressionMemberOption()) return true;
            if (ExpressionMemberRepeat()) return true;
            if (ExpressionMemberOption2()) return true;
            if (ExpressionMemberRepeat2()) return true;
            return false;
        }
    }
}
