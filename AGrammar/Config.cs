using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    public class GrammarConfig
    {
        public Production root;
    }

    public class ScannerConfig
    {
        public string lineComment;
        public string blockCommentStart;
        public string blockCommentEnd;
        public KeyWord[] keywords;
        public string[] terminals
        {
            get
            {
                return mTerminals;
            }
            set
            {
                List<string> rawList = new List<string>();
                rawList.AddRange(value);
                rawList.Sort(SortFunc);
                mTerminals = rawList.ToArray();
            }
        }


        static int SortFunc(string a, string b)
        {
            int la = a.Length;
            int lb = b.Length;
            if (la == lb)
                return 0;
            if (la > lb)
                return -1;
            return 1;
        }

        public ScannerConfig()
        {
            lineComment = "//";
            blockCommentStart = "/*";
            blockCommentEnd = "*/";
            terminals = new string[]{
            "+=", "-=", "*=", "/=","<=", "==","!=", ">=", "||", "&&", "++", "--",
            "+", "-", "*", "/", ".", "=", "?", "(", ")", "{", "}", "[", "]","<",">" ,":", ";", ",", "|", "&",
            };
        }

        public int GetKeywordToken(string word)
        {
            int id = Grammar.ID;
            keywordMap.TryGetValue(word, out id);
            return id;
        }

        protected Dictionary<string, int> mKeywordMap;
        protected string[] mTerminals;

        Dictionary<string, int> keywordMap
        {
            get
            {
                if (mKeywordMap == null)
                {
                    mKeywordMap = new Dictionary<string, int>();
                    foreach (var word in keywords)
                        mKeywordMap.Add(word.Word, word.WordType);
                }
                return mKeywordMap;
            }
        }
    }

    public class Config
    {
        public ScannerConfig scanner = new ScannerConfig();
        public GrammarConfig grammar = new GrammarConfig();
        public Action<string> msgHaneler;

        public bool Valid
        {
            get
            {
                return grammar.root != null && scanner.terminals != null;
            }
        }
    }
}
