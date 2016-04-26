using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BGrammar
{
    public static BGrammar Instance
    {
        get
        {
            if (mInstance == null)
            {
                mInstance = new BGrammar();
            }
            return mInstance;
        }
    }

    static BGrammar mInstance;

    public Stack<GrammarTable> Parse(List<Exp> exps, string content, Func<bool> loader)
    {
        throw new Exception();
    }
}
