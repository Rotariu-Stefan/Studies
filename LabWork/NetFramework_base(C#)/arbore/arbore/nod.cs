using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace arbore
{

    [Serializable]
    public class nod<T>
    {
        public T val;
        public nod<T> par;
        public nod<T> fs;
        public nod<T> fd;

        #region constructors
        public nod()
        {
            val = default(T);
        }
        public nod(T v)
        {
            val = v;
        }
        public nod(nod<T> n)
        {
            val = n.val;
            par = n.par;
            fs = n.fs;
            fd = n.fd;
        }
        public nod(T v, nod<T> p)
        {
            val = v;
            par = p;
            if (p.fs != null && p.fd != null)
                Console.WriteLine("ERR:ta deja are 2 copchii !!");
            else
                if (p.fs == null)
                    p.fs = this;
                else
                    p.fd = this;
        }
        #endregion

        public void afis()
        {

            Console.WriteLine("Nod: " + val);
            if (fs != null)
                Console.WriteLine("Fiu stang: " + fs.val);
            if (fd != null)
                Console.WriteLine("Fiu drept: " + fd.val);
        }

    }
}
