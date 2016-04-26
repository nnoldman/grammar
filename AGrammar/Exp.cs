using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
public class Builder
{
    public static Dictionary<string, Exp> mExps = new Dictionary<string, Exp>();

    public static Dictionary<string, Exp> exps
    {
        get
        {
            if (null == mExps)
            {
                mExps = new Dictionary<string, Exp>();
                mExps.Add(Exp.Empty.name, Exp.Empty);
            }
            return mExps;
        }
    }
    public static Exp Add(string name)
    {
        return Create(name, true);
    }
    public static Exp Create(string name,bool sentence)
    {
        Exp exp;
        if (!mExps.TryGetValue(name, out exp))
        {
            exp = new Exp(name);
            mExps.Add(name, exp);
        }
        else
        {
            Debug.Assert(sentence == exp.sentence);
        }
        return exp;
    }
    public static Exp Prop(string name, object exp)
    {
        throw new Exception();
    }
    public static Exp Get(string name)
    {
        return mExps[name];
    }
}

public enum NType
{
    Zero = 1 << 0,
    One = 1 << 1,
    Array = 1 << 2 & One,
}

public class Exp : BoolObject
{
    public static readonly Exp Empty = new Exp("\\^_^//");
    static readonly Type mTypeString = typeof(string);
    static readonly Type mTypeInt = typeof(int);

    public string name = string.Empty;
    public NType ntype = NType.Zero;

    public object tokenType;

    public bool sentence = false;

    List<Exp> mChildren = new List<Exp>();

    public Exp OrArray()
    {
        ntype = NType.Array;
        return this;
    }
    public Exp Is(params object[] para)
    {
        foreach (var arg in para)
        {
            var tp = arg.GetType();
            Exp exp = null;
            if (tp == mTypeString)
            {
                exp = Builder.Create(arg.ToString(),false);
                exp.tokenType = arg;
            }
            else if (tp == mTypeInt)
            {
                exp = Builder.Create(arg.ToString(), false);
                exp.tokenType = arg;
            }
            else
            {
                throw new Exception("unknown type");
            }
            mChildren.Add(exp);
        }
        return this;
    }
    public Exp(string name)
    {
        this.name = name;
    }

}
public class Or
{
    public static Exp One(params object[] para)
    {
        throw new Exception();
    }
}