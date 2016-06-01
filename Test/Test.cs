using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tester
{
    static void HandleError(string msg)
    {
        Debug.WriteLine(msg);
    }
    public static void TestCSGrammar()
    {
        CSGrammar.Paser p = new CSGrammar.Paser();
        string content = File.ReadAllText("TestCSGrammar.cs");
        p.Load(HandleError, content);
        p.OutPut("TestCSGrammar.json");
    }
}
