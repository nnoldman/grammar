using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            Tester.TestCSGrammar();
            Tester.TestPBGrammar();

            int a = 5;
            int b = 6;
            int c = 7;
            int d = 8;
            int e = 9;
            int f = 10;
            int m = (((((((((((((((a + b) + 6) + c) + a) * b) * d) * e) * f) * a) * b) * c) * d) * e) * f) * a);
        }

        static void Run()
        {
            for (int i = 0; i < 20000; ++i)
                Debug.WriteLine(i.ToString());
        }
    }
}
