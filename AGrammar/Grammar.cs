using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum SymbolType
{
    PackageName,
    MessageType,
    MemberType,
    MemberName,
    MemberID,
}

public class Symbol
{
    public string id;
    public SymbolType type;
    public object value;
    public Symbol(string i, SymbolType t, object v)
    {
        id = i;
        type = t;
        value = v;
    }
}
public class MessageMember
{
    public string condtion;
    public string type;
    public string name;
    public int memberID;
}
public class IBool
{
    public static implicit operator bool(IBool b)
    {
        return b != null;
    }
}
public class Message : IBool
{
    public string typename;
    public List<MessageMember> members = new List<MessageMember>();
}
public class GrammarTree
{
    public string packageName;
    public List<Message> messages = new List<Message>();
}
public class GrammarTable : IBool
{
    public string typeName;
    public string content;

    public void AddProperty(GrammarTable id)
    {
        mProtices.Add(id);
    }
    public void AddChildren(GrammarTable id)
    {
        mChildren.Add(id);
    }
    public void RemoveChildren(GrammarTable id)
    {
        mChildren.Remove(id);
    }

    public void WriteTo(StringBuilder sb,int spaceCount)
    {
        string space = new string(' ', spaceCount);

        if (mProtices.Count == 0 && mChildren.Count == 0)
        {
            sb.AppendLine(string.Format("{0}Table {1}({2})", space, content, typeName));
        }
        else
        {
            sb.AppendLine(string.Format("{0}Table {1}({2})", space, content, typeName));
            sb.Append(space + "{\n");

            if (mProtices.Count > 0)
                foreach (var item in mProtices)
                {
                    item.WriteTo(sb, spaceCount + 4);
                }
            if (mChildren.Count > 0)
                foreach (var item in mChildren)
                {
                    item.WriteTo(sb, spaceCount + 4);
                }
            sb.Append(space + "}\n");
            spaceCount -= 4;
        }
    }

    public string GetAllContent()
    {
        StringBuilder sb = new StringBuilder();
        return sb.ToString();
    }
    public override string ToString()
    {
        StringBuilder sb = new StringBuilder();
        foreach (var item in mProtices)
        {
            sb.Append(string.Format("{0} ", item.content));
        }
        return sb.ToString();
    }
    List<GrammarTable> mProtices = new List<GrammarTable>();
    List<GrammarTable> mChildren = new List<GrammarTable>();
}

public class Grammar
{
    public static Grammar Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new Grammar();
            }
            return mInstance;
        }
    }

    static Grammar mInstance;

    public Action<string> ErrorHandler;
    Stack<GrammarTable> mTableStack;
    List<Token> mTokens;
    Scanner mScanner;
    int mCurrentIndex;

    public Grammar()
    {
        mScanner = new Scanner();
        mTableStack = new Stack<GrammarTable>();
    }
    void Error(string msg)
    {
        if (ErrorHandler != null)
            ErrorHandler(msg);
    }
    void Error(Token token)
    {
        Error(string.Format("Load Error:{0}", token.ToString()));
    }
    public Stack<GrammarTable> Parse(TokenParam[] tokens, string content,Func<bool> loader)
    {
        if (ErrorHandler != null)
            mScanner.ErrorHandler = ErrorHandler;

        mTokens = mScanner.Scan(tokens, content);

        if (loader != null)
        {
            if (!loader())
            {
                Error(mCurrentToken);
            }
        }
        return mTableStack;
    }

    Token mCurrentToken
    {
        get
        {
            return mTokens[mCurrentIndex];
        }
    }

    
    bool IsEnd()
    {
        return mCurrentIndex == mTokens.Count - 1;
    }
    public bool NextArrayIs(int old, Func<bool> loader, string startStr, string endStr)
    {
        if (!NextTokenIs(old, startStr))
            return false;

        while (!IsEnd())
        {
            if (mCurrentToken.Content == endStr)
            {
                mCurrentIndex++;
                return true;
            }
            if (!loader())
            {
                PopOnFailed(true);
                return false;
            }
        }
        return true;
    }
    public bool NextArrayIs(int old, Func<bool> loader)
    {
        while (!IsEnd())
        {
            if (!loader())
                return false;
        }
        return true;
    }
    public bool NextTokenIs(int old, int type, string symbolTypeName = "")
    {
        if (IsEnd())
            return false;
        if (this.mCurrentToken.TT == type)
        {
            if (!string.IsNullOrEmpty(symbolTypeName))
            {
                var table = new GrammarTable() { content = this.mCurrentToken.Content, typeName = symbolTypeName };
                mTableStack.Peek().AddProperty(table);
            }
            mCurrentIndex++;
            return true;
        }
        mCurrentIndex = old;
        PopOnFailed(true);
        return false;
    }
    public bool NextTokenIs(int old, string content)
    {
        if (!IsEnd())
        {
            if (mCurrentToken.Content == content)
            {
                mCurrentIndex++;
                return true;
            }
        }
        mCurrentIndex = old;
        PopOnFailed(true);
        return false;
    }

    public int Push(string typename)
    {
        var table = new GrammarTable() { typeName = typename };
        if (mTableStack.Count > 0)
        {
            mTableStack.Peek().AddChildren(table);
            mTableStack.Push(table);
        }
        else
        {
            mTableStack.Push(table);
        }
        return mCurrentIndex;
    }
    public void Pop()
    {
        PopOnFailed(false);
    }
    void PopOnFailed(bool leaf = false)
    {
        var child = mTableStack.Peek();
        mTableStack.Pop();
        if (leaf && mTableStack.Count > 0)
        {
            mTableStack.Peek().RemoveChildren(child);
        }
    }
}
