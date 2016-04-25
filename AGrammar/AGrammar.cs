using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

public class BoolObject
{
    public static implicit operator bool(BoolObject b)
    {
        return b != null;
    }
}
public class GrammarTable : BoolObject
{
    public string typeName;
    public string content;

    public Dictionary<string, string> propertices
    {
        get
        {
            return mPropertices;
        }
    }
    public List<GrammarTable> children
    {
        get
        {
            return mChildren;
        }
    }

    public string this[string propertyName]
    {
        get
        {
            string content;
            if (mPropertices.TryGetValue(propertyName, out content))
                return content;
            return string.Empty;
        }
    }

    public void SetProperty(string key,string content)
    {
        mPropertices.Add(key, content);
    }
    public void AddChildren(GrammarTable id)
    {
        mChildren.Add(id);
    }
    public void RemoveChildren(GrammarTable id)
    {
        mChildren.Remove(id);
    }

    public void WriteTo(StringBuilder sb, int spaceCount)
    {
        string space = new string(' ', spaceCount);

        if (mPropertices.Count == 0 && mChildren.Count == 0)
        {
            sb.AppendLine(string.Format("{0}Table {1}({2})", space, content, typeName));
        }
        else
        {
            sb.AppendLine(string.Format("{0}Table {1}({2})", space, content, typeName));
            sb.Append(space + "{\n");

            if (mPropertices.Count > 0)
                foreach (var item in mPropertices)
                {
                    sb.AppendLine(string.Format("{0}Property:{1} ({2}))", space + 4, item.Value, item.Key));
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
        foreach (var item in mPropertices)
        {
            sb.Append(string.Format("{0} ", item.Value));
        }
        return sb.ToString();
    }
    Dictionary<string, string> mPropertices = new Dictionary<string, string>();
    List<GrammarTable> mChildren = new List<GrammarTable>();
}

public class AGrammar
{
    public static AGrammar Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new AGrammar();
            }
            return mInstance;
        }
    }

    static AGrammar mInstance;

    public Action<string> ErrorHandler;
    Stack<GrammarTable> mTableStack;
    List<GrammarToken> mTokens;
    AScanner mScanner;
    int mCurrentIndex;

    public AGrammar()
    {
        mScanner = new AScanner();
        mTableStack = new Stack<GrammarTable>();
    }
    void Error(string msg)
    {
        if (ErrorHandler != null)
            ErrorHandler(msg);
    }
    void Error(GrammarToken token)
    {
        Error(string.Format("Load Error:{0}", token.ToString()));
    }
    public Stack<GrammarTable> Parse(TokenParam[] tokens, string content, Func<bool> loader)
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

    GrammarToken mCurrentToken
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
        if (this.mCurrentToken.TokenType == type)
        {
            if (!string.IsNullOrEmpty(symbolTypeName))
            {
                mTableStack.Peek().SetProperty(symbolTypeName, this.mCurrentToken.Content);
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
