using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T1
{
    class matp
    {
        public int n;
        public double[,] m;

        public matp()
        {
            n = 1;
            m = new double[n, n];
            m[0, 0] = 0;
        }
        public matp(int pn)
        {
            n = pn;
            m = new double[n, n];
            populate(0);
        }
        public matp(double[,] pm)
        {
            n = (int)Math.Sqrt(pm.Length);
            m = new double[n, n];
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    m[i, j] = pm[i, j];
        }
        public void populate(double x)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    m[i, j] = x;
        }

        public matp[] section()
        {
            matp[] blocs = new matp[4];
            for (int i = 0; i < 4; i++)
                blocs[i] = new matp(this.n / 2);

            for (int i = 0; i < n / 2; i++)
                for (int j = 0; j < n / 2; j++)
                {
                    blocs[0].m[i, j] = this.m[i, j];
                    blocs[1].m[i, j] = this.m[i, j + n / 2];
                    blocs[2].m[i, j] = this.m[i + n / 2, j];
                    blocs[3].m[i, j] = this.m[i + n / 2, j + n / 2];
                }

            return blocs;
        }
        public override string ToString()
        {
            string s = "";
            s += Environment.NewLine + Environment.NewLine + "n=" + n;
            for (int i = 0; i < n; i++)
            {
                s += Environment.NewLine + "         | ";
                for (int j = 0; j < n; j++)
                    s += m[i, j] + " | ";
            }
            return s;
        }
    }
    class opr
    {
        static public matp mul(matp m1, matp m2)
        {
            if (m1.n != m2.n)
            {
                Console.WriteLine("Wrong mul!");
                return new matp(1);
            }

            int n = m1.n;
            matp rez = new matp(n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    for (int k = 0; k < n; k++)
                        rez.m[i, j] += (m1.m[i, k] * m2.m[k, j]);
            return rez;
        }

        static public matp add(matp m1, matp m2)
        {
            if (m1.n != m2.n)
            {
                Console.WriteLine("Wrong add!");
                return new matp(1);
            }
            int n = m1.n;
            matp rez = new matp(n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rez.m[i, j] = m1.m[i, j] + m2.m[i, j];
            return rez;
        }

        static public matp sub(matp m1, matp m2)
        {
            if (m1.n != m2.n)
            {
                Console.WriteLine("Wrong sub!");
                return new matp(1);
            }
            int n = m1.n;
            matp rez = new matp(n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    rez.m[i, j] = m1.m[i, j] - m2.m[i, j];
            return rez;
        }

        static public matp remake(matp[] ms)
        {
            int n = ms[0].n * 2;
            matp mr = new matp(n);

            for (int i = 0; i < n / 2; i++)
                for (int j = 0; j < n / 2; j++)
                {
                    mr.m[i, j] = ms[0].m[i, j];
                    mr.m[i, j + n / 2] = ms[1].m[i, j];
                    mr.m[i + n / 2, j] = ms[2].m[i, j];
                    mr.m[i + n / 2, j + n / 2] = ms[3].m[i, j];
                }
            return mr;
        }
    }
}
