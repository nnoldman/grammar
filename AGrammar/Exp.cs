using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Exp
{
    public static Exp Empty = new Exp("empty");
    public static Exp Add(string name)
    {
        throw new Exception();
    }
    public static Exp Get(string name)
    {
        throw new Exception();
    }
    public Exp Or(params object[] para)
    {
        throw new Exception();
    }
    public Exp OrArray()
    {
        throw new Exception();
    }
    public Exp Node(string conent)
    {
        throw new Exception();

    }
    public Exp Push(string content)
    {
        throw new Exception();
    }
    public Exp Pop()
    {
        throw new Exception();
    }

    public Exp Is(params object[] para)
    {
        throw new Exception();
    }
    public Exp(string content)
    {

    }

}
public class Or
{
    public static Exp One(params object[] para)
    {
        throw new Exception();
    }
}