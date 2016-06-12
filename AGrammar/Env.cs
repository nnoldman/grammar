using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGrammar
{
    internal class Env : BoolObject, IEnumerable<Env>
    {
        public Env prev;

        Dictionary<Token, ID> set = new Dictionary<Token, ID>();

        public void Put(Token t,ID id)
        {
            set.Add(t, id);
        }
        public ID Get(Token t)
        {
            foreach (var env in this)
            {
                ID id = null;
                if (env.set.TryGetValue(t, out id))
                    return id;
            }
            return null;
        }

        public IEnumerator<Env> GetEnumerator()
        {
            yield return this;

            Env p = prev;

            while (p)
            {
                yield return p;
                p = p.prev;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
