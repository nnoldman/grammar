using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class PVM
{
    public List<PChunk> globalChunk;
    public List<object> globalVars;
    public Dictionary<string, PClass> classes;

    public void DoString(string s,string chunkname="")
    {

    }

    public void DoBytes(byte[] s, string chunkname = "")
    {

    }
}

public class PArgs
{
    public List<object> args;
}
public class PChunk
{
    object r;
    object a;
    object b;

    PFunction addr;

    public object[] Invoke(PClass instance)
    {
        a = instance;
        addr = instance.getF("CalCount");
        return addr.Call();
        throw new Exception();
    }
    public static object Add(object a, object b)
    {
        throw new Exception();
    }
    public static object Sub(object a, object b)
    {
        throw new Exception();
    }
    public static object Mul(object a, object b)
    {
        throw new Exception();
    }
    public static object Div(object a, object b)
    {
        throw new Exception();
    }
    public static object Mov(object a, object b)
    {
        throw new Exception();
    }
    public static void Jmp(IntPtr addr)
    {
        throw new Exception();
    }
}

public class PFunction
{
    public string name;
    public object result;
    public object[] args;
    public PChunk chunk;

    public object[] Call(PClass instance = null)
    {
        return chunk.Invoke(instance);
    }
}

public class PMember
{
    public Type type;
    public string name;
    public int index;
}

public class PClass
{
    public string name;

    public Dictionary<string, PMember> members;
    public Dictionary<string, PFunction> functions;

    public PMember getM(string mem)
    {
        return members[mem];
    }

    public PFunction getF(string mem)
    {
        return functions[mem];
    }
}

class PScript
{
    LuaScriptObject owner;

    public int count;

    public string name;

    public void Awake()
    {
        owner.Show();
    }
}
