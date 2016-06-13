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
        public string[] terminals;
        public KeyWord[] keywords;

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
                return grammar.root != null;
            }
        }
    }
}
