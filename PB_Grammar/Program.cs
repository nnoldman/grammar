using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PB_Grammar
{
    class Program
    {
        static void Main(string[] args)
        {
            PB_Grammar.ProtoBufferParser parser = new ProtoBufferParser();
            parser.Load();
        }
    }
}
