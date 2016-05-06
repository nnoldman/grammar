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
        return TryGetAndCreate(name);
    }
    public static Exp TryGetAndCreate(string name)
    {
        Exp exp;
        if (!mExps.TryGetValue(name, out exp))
        {
            exp = new Exp(name);
            mExps.Add(name, exp);
        }
        return exp;
    }
    public static Prop Prop(string propName, Exp exp)
    {
        Prop prop = new Prop(propName);
        prop.exp = exp;
        return prop;
    }
    public static Prop Prop(string propName, int tokenID)
    {
        Prop prop = new Prop(propName);
        prop.exp = TryGetAndCreate(tokenID.ToString());
        return prop;
    }
    public static Prop Prop(string propName, string content)
    {
        Prop prop = new Prop(propName);
        prop.exp = TryGetAndCreate(content);
        return prop;
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
    static readonly Type mTypeProperty = typeof(Prop);
    static readonly Type mTypeOr = typeof(Or);
    static readonly Type mTypeExp = typeof(Exp);

    public string name = string.Empty;
    public NType ntype = NType.Zero;

    public object tokenType;

    public List<Prop> propertices
    {
        get
        {
            return mProperties;
        }
    }
    public List<Exp> children
    {
        get
        {
            return mChildren;
        }
    }
    public List<Exp> sublings
    {
        get
        {
            return mSublings;
        }
    }
    List<Prop> mProperties = new List<Prop>();

    List<Exp> mChildren = new List<Exp>();
    List<Exp> mSublings = new List<Exp>();
    public Exp()
    {

    }
    public Exp OrArray()
    {
        ntype = NType.Array;
        return this;
    }
    public Exp AddProperty(Prop exp)
    {
        return this;
    }
    public static Exp Create(object arg)
    {
        Exp exp = null;
        var tp = arg.GetType();
        if (tp == mTypeString)
        {
            return Create((string)arg);
        }
        else if (tp == mTypeInt)
        {
            return Create((int)arg);
        }
        else if (tp == mTypeProperty)
        {
            return Create((Prop)arg);
        }
        else
        {
            return (Exp)arg;
        }
    }
    public static Exp Create(Exp arg)
    {
        return arg;
    }
    public static Exp Create(string arg)
    {
        Exp exp = Builder.TryGetAndCreate(arg.ToString());
        return exp;
    }
    public static Exp Create(int arg)
    {
        Exp exp = Builder.TryGetAndCreate(arg.ToString());
        exp.tokenType = arg;
        return exp;
    }
    public static Exp Create(Prop arg)
    {
        return arg.exp;
    }

    public Exp Is(params object[] para)
    {
        foreach (var arg in para)
        {
            var tp = arg.GetType();
            Exp exp = Create(arg);
            if (tp == mTypeProperty)
            {
                Prop prop = (Prop)arg;
                mProperties.Add(prop);
            }
            mChildren.Add(exp);
        }
        return this;
    }
    public Exp(string name)
    {
        this.name = name;
    }
    public Exp(int tokenID)
    {
        this.tokenType = tokenID;
    }

    public override string ToString()
    {
        return string.Format("Exp({0})", name);
    }
}
public class Prop
{
    public string propertyName = string.Empty;
    public Exp exp;
    public Prop(string name)
    {
        propertyName = name;
    }
}
public class Or : Exp
{
    static int count = 10000;

    public Or()
    {
        name = "_or_" + (++count).ToString();
    }

    public static Exp One(params object[] para)
    {
        Or o = new Or();
        foreach (var p in para)
        {
            Exp exp = Exp.Create(p);
            o.sublings.Add(exp);
        }
        return o;
    }
}