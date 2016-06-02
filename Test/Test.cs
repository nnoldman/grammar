using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tester
{
    static void HandleMessage(string msg)
    {
        Console.WriteLine(msg);
    }
    public static void TestCSGrammar()
    {
        CSGrammar.Parser p = new CSGrammar.Parser();
        string content = File.ReadAllText("TestCSGrammar.cs");
        p.Load(HandleMessage, content);
        p.Dump("TestCSGrammar.json");
    }

    public static void TestPBGrammar()
    {
        PBGrammar.Parser p = new PBGrammar.Parser();
        string content = File.ReadAllText("commondData.proto");
        p.Load(HandleMessage, content);
        p.Dump("TestPBGrammar.json");
    }
}
