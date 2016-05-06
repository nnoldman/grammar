using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_Grammar
{
    public class MessageMember
    {
        public string condtion;
        public string type;
        public string name;
        public int memberID;
    }
    public class Message : BoolObject
    {
        public string typename;
        public List<MessageMember> members = new List<MessageMember>();
    }
    public class GrammarTree
    {
        public string packageName;
        public List<Message> messages = new List<Message>();
    }
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
            new TokenParam(){TokenType= PBTokenType.Comment,Content="//"},
            new TokenParam(){TokenType= PBTokenType.Package,Content="package"},
            new TokenParam(){TokenType= PBTokenType.Message,Content="message"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="int32"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="uint32"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="string"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="uint64"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="bytes"},
            new TokenParam(){TokenType= PBTokenType.Option,Content="optional"},
            new TokenParam(){TokenType= PBTokenType.Repeated,Content="repeated"},
            new TokenParam(){TokenType= PBTokenType.TypeName,Content="int64"},
        };

        AGrammar mGrammar ;

        public  void Test()
        {
            Builder.Add("package").Is(PBTokenType.Package, AScanner.ID, ".", AScanner.ID, ";");
            Builder.Add("typename").Is(Or.One(PBTokenType.TypeName, AScanner.ID));
            Builder.Add("condtion").Is(Or.One(PBTokenType.Option, PBTokenType.Repeated));

            Builder.Add("member").Is(
                Builder.Prop("condtion", Builder.Get("condtion"))
                , Builder.Prop("typename", Builder.Get("typename"))
                , Builder.Prop("member_name", AScanner.ID)
                , "="
                , Builder.Prop("member_id", AScanner.ID)
                , ";");

            Builder.Add("message_body").Is(Or.One(Builder.Get("member").OrArray(), Exp.Empty));
            Builder.Add("message").Is(PBTokenType.Message, Builder.Prop("message_name", AScanner.ID), "{", Builder.Get("message_body"), "}", Or.One(";", Exp.Empty));

            BGrammar.Instance.Parse(Builder.exps.Values.ToList<Exp>(), string.Empty, LoadPackage);
        }
        public bool Load()
        {
            Test();
            mGrammar = AGrammar.Instance;
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
            if (!mGrammar.NextTokenIs(old, AScanner.ID)) return false;
            if (!mGrammar.NextTokenIs(old, ".")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID)) return false;
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
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "messageName")) return false;
            if (!mGrammar.NextArrayIs(old, LoadOneMember, "{", "}")) return false;
            mGrammar.Pop();
            return true;
        }
        bool LoadOneMessage2()
        {
            int old = mGrammar.Push("message");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Message)) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "messageName")) return false;
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
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberOption2()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Option, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "type")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberRepeat()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Repeated, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, PBTokenType.TypeName, "type")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "memberID")) return false;
            if (!mGrammar.NextTokenIs(old, ";")) return false;
            mGrammar.Pop();
            return true;
        }
        bool ExpressionMemberRepeat2()
        {
            int old = mGrammar.Push("member");
            if (!mGrammar.NextTokenIs(old, PBTokenType.Repeated, "condtion")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "type")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "name")) return false;
            if (!mGrammar.NextTokenIs(old, "=")) return false;
            if (!mGrammar.NextTokenIs(old, AScanner.ID, "memberID")) return false;
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
