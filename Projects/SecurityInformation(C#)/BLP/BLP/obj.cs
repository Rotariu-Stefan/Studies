using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class objc
    {
        string name;
        List<latice.catg> comp;
        latice.clas claso;

        #region constructors
        public objc()
        {
            name = "odef";
            comp = new List<latice.catg>();
        }
        public objc(string n)
        {
            name = n;
            comp = new List<latice.catg>();
            claso=latice.clas.pblic;
        }
        public objc(string n, List<latice.catg> co,latice.clas cl)
        {
            name = n;
            comp = co;
            claso = cl;
        }
        public objc(string n, latice.catg co, latice.clas cl)
        {
            name = n;
            comp = new List<latice.catg>();
            comp.Add(co);
            claso = cl;
        }
        public objc(string n, latice.clas cl)
        {
            name = n;
            comp = new List<latice.catg>();         
            claso = cl;
        }
        #endregion

        #region returns
        public string retn()
        {
            return name;
        }
        public List<latice.catg> retcomp()
        {
            return comp;
        }
        public latice.clas retclas()
        {
            return claso;
        }
        #endregion

        public void afis()
        {
            Console.Write(name);
            Console.Write("\nCompartiment : ");
            foreach (var it in comp)
                Console.Write(it + "  ");
            Console.Write("\n Clasificare : {0}\n\n", claso);
        }

    }
}
