using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class subcap
    {
        public enum acces { write, read, append, exec };

        string name;
        List<List<acces>> cap;
        List<latice.catg> comp;
        latice.clas clasc;
        latice.clas clasm;

        bool trusted;

        #region constructors
        public subcap()
        {
            name = "sdef";
            cap = new List<List<acces>>();
            comp = new List<latice.catg>();
            clasc = latice.clas.pblic;
            clasm = latice.clas.pblic;
            trusted = false;
        }
        public subcap(string n)
        {
            name = n;
            cap = new List<List<acces>>();
            comp = new List<latice.catg>();
            clasc = latice.clas.pblic;
            clasm = latice.clas.pblic;
            trusted = false;
        }
        public subcap(string n, List<latice.catg> co, latice.clas clc, latice.clas clm)
        {
            name = n;
            cap = new List<List<acces>>();
            comp = co;
            if (clc <= clm)
            {
                clasc = clc;
                clasm = clm;
            }
            else
            {
                Console.WriteLine("Maximal security level < current security level !\n default to 1,1");
                clasc = latice.clas.pblic;
                clasm = latice.clas.pblic;
            }
            trusted = false;
        }
        public subcap(string n, latice.catg co, latice.clas clc, latice.clas clm)
        {
            name = n;
            cap = new List<List<acces>>();
            comp = new List<latice.catg>();
            comp.Add(co);
            if (clc <= clm)
            {
                clasc = clc;
                clasm = clm;
            }
            else
            {
                Console.WriteLine("Maximal security level < current security level !\n default to 1,1");
                clasc = latice.clas.pblic;
                clasm = latice.clas.pblic;
            }
            trusted = false;
        }
        public subcap(string n, latice.clas clc, latice.clas clm)
        {
            name = n;
            cap = new List<List<acces>>();
            comp = new List<latice.catg>();
            if (clc <= clm)
            {
                clasc = clc;
                clasm = clm;
            }
            else
            {
                Console.WriteLine("Maximal security level < current security level !\n default to 1,1");
                clasc = latice.clas.pblic;
                clasm = latice.clas.pblic;
            }
            trusted = false;
        }
        #endregion 

        #region object rights
        public void setcap(List<List<acces>> obR)
        {
            cap = obR;
        }
        public void setcapI(int x, List<acces> ac)
        {
            cap[x] = ac;
        }
        public void addcapI(int x, acces r)
        {
            cap[x].Add(r);
        }
        #endregion

        #region returns
        public string retn()
        {
            return name;
        }
        public List<List<acces>> retcap()
        {
            return cap;
        }
        public List<acces> retcapI(int i)
        {
            return cap[i];
        }
        public List<latice.catg> retcomp()
        {
            return comp;
        }
        public latice.clas retclasc()
        {
            return clasc;
        }
        public latice.clas retclasm()
        {
            return clasm;
        }
        #endregion

        #region object stats
        private void initobj()
        {
            cap.Add(new List<acces>());
        }
        public void initobj(int nO)
        {
            for (int i = 0; i < nO; i++)
                initobj();
        }
        public void remob(int pozO)
        {
            cap.RemoveAt(pozO);
        }
        #endregion

        #region trusted
        public void setTrust()
        {
            trusted = true;
        }
        public void unsetTrust()
        {
            trusted = false;
        }
        public bool getT()
        {
            return trusted;
        }
        #endregion

        public void afis()
        {
            Console.Write(name);
            Console.Write("\nCompartiment : ");
            foreach (var it in comp)
                Console.Write(it + "  ");
            Console.Write("\nClasificareC : {0}\n", clasc);
            Console.Write("ClasificareM : {0}\n\n", clasc);
        }

    }
}
