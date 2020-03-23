using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class P3
    {
        static public matp[] genP(matp m1, matp m2,int n_min)
        {
            matp[] m1bl = m1.section();
            matp[] m2bl = m2.section();

            matp[] P = new matp[7];

            P[0] = mulStrass(opr.add(m1bl[0], m1bl[3]), opr.add(m2bl[0], m2bl[3]),n_min);
            P[1] = mulStrass(opr.add(m1bl[2], m1bl[3]), m2bl[0], n_min);
            P[2] = mulStrass(m1bl[0], opr.sub(m2bl[1], m2bl[3]), n_min);
            P[3] = mulStrass(m1bl[3], opr.sub(m2bl[2], m2bl[0]), n_min);
            P[4] = mulStrass(opr.add(m1bl[0], m1bl[1]), m2bl[3], n_min);
            P[5] = mulStrass(opr.sub(m1bl[2], m1bl[0]), opr.add(m2bl[0], m2bl[1]), n_min);
            P[6] = mulStrass(opr.sub(m1bl[1], m1bl[3]), opr.add(m2bl[2], m2bl[3]), n_min);

            return P;
        }

        static public matp[] genC(matp[] P)
        {
            matp[] C = new matp[4];

            C[0] = opr.add(opr.sub(opr.add(P[0], P[3]), P[4]), P[6]);
            C[1] = opr.add(P[2], P[4]);
            C[2] = opr.add(P[1], P[3]);
            C[3] = opr.add(opr.sub(opr.add(P[0], P[2]), P[1]), P[5]);

            return C;
        }

        static public matp mulStrass(matp m1, matp m2,int n_min)
        {
            if (n_min > m1.n || n_min < 1)
            {
                Console.WriteLine("Wrong n-min !");
                return new matp(1);
            }
            if (m1.n <= n_min)
                return opr.mul(m1, m2);

            matp[] P = genP(m1, m2, n_min);
            matp[] C = genC(P);
            matp rez = opr.remake(C);

            return rez;
        }
    }
}
