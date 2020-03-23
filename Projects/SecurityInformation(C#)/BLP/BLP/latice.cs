using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class latice
    {
        public enum catg { ingineri, personal, clienti } ;
        public enum clas { pblic = 1, privat = 2, secret = 3 } ;

        public latice() { }

        #region domination
        private static bool dommax2(clas clas1, List<catg> comp1, clas clas2, List<catg> comp2)
        {
            if (clas1 > clas2)
                return false;
            foreach (var it1 in comp1)
            {
               // Console.Write("CO" + comp1[i]);
                if (comp2.Contains(it1) == false)
                {
                    Console.Write("comparment @->");
                    return false;
                }
            }
            return true;
        }
        public static bool dom(slevel s1, slevel s2)
        {
            return dommax2(s1.clas, s1.comp, s2.clas, s2.comp);
        }
        #endregion

    }
    class slevel
    {
        public latice.clas clas;
        public List<latice.catg> comp;

        public slevel(subcap s, char c)
        {
            if (c == 'C')
                clas = s.retclasc();
            if (c == 'M' || c == 'S')
                clas = s.retclasm();

            comp = s.retcomp();
        }
        public slevel(objc o)
        {
            clas = o.retclas();

            comp = o.retcomp();
        }

    }
}
