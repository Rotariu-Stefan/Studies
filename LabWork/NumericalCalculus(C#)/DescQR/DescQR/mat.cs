﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DescQR
{
    class matp
    {
        public int n;
        public double[,] m;

        #region constr

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
        public matp(string m)
        {
            string[] mline = m.Split('\n');

            int n = mline.Length;
            double[,] mat = new double[n, n];

            string[] mel;
            for (int i = 0; i < n; i++)
            {
                mel = mline[i].Split(' ');
                if (n != mel.Length)
                {
                    MessageBox.Show("Not square matrix !");
                    break;
                }
                for (int j = 0; j < n; j++)
                    mat[i, j] = Convert.ToDouble(mel[j]);
            }
            this.m = mat;
            this.n = n;
        }
        public void populate(double x)
        {
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    m[i, j] = x;
        }
        public void initmat()
        {
            for (int i = 0; i < n; i++)
                m[i, i] = 1;
        }

        #endregion

        #region regular functions

        public double gm(int i, int j)
        {
            if (i <= 0 || i > n || j <= 0 || j > n)
            {
                MessageBox.Show("Invalid index !");
                return 0;
            }
            else
                return m[i - 1, j - 1];
        }
        public void sm(int i, int j,double v)
        {
            if (i <= 0 || i > n || j <= 0 || j > n)
                MessageBox.Show("Invalid index !");
            else
                m[i - 1, j - 1] = v;
        }

        public double det()
        {
            if (n == 3)
            {
                double p1 = m[0, 0] * m[1, 1] * m[2, 2] + m[0, 2] * m[1, 0] * m[2, 1] + m[0, 1] * m[1, 2] * m[2, 0];
                double p2 = m[0, 2] * m[1, 1] * m[2, 0] + m[0, 1] * m[1, 0] * m[2, 2] + m[0, 0] * m[1, 2] * m[2, 1];
                return p1 - p2;
            }
            else
                return 0;
        }

        public matp transpose()
        {
            matp t = new matp(n);

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    t.m[j, i] = m[i, j];

            return t;
        }

        public bool checksing()
        {
            for (int i = 0; i < n; i++)
                if (m[i, i] == 0)
                    return false;
            
            return true;
        }

        #endregion

        #region addf

        public bool solTest(double[] x, double[] b)
        {
            if (x.Length != b.Length || x.Length != n)
                MessageBox.Show("Invalid length !");
            double sumt = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    sumt += (m[i, j] * x[j]);
                if (sumt != b[i])
                    return false;
                sumt = 0;
            }
            return true;
        }

        public override string ToString()
        {
            string s = "";
            s += Environment.NewLine + "n=" + n;
            for (int i = 0; i < n; i++)
            {
                s += Environment.NewLine + "| ";
                for (int j = 0; j < n; j++)
                    s += m[i, j] + " | ";
            }
            return s;
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

        #endregion
    }


    class opr
    {
        public static double eps = 0.00000001;

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
                {
                    for (int k = 0; k < n; k++)
                        rez.m[i, j] += (m1.m[i, k] * m2.m[k, j]);
                    if (Math.Abs(rez.m[i, j]) < eps)
                        rez.m[i, j] = 0;
                }
            return rez;
        }

        static public double[] mulsc(matp m, double[] x)
        {
            if (m.n != x.Length)
                MessageBox.Show("Invalid length !");

            double[] rez = new double[m.n];
            double sumt = 0;

            for (int i = 0; i < m.n; i++)
            {
                for (int j = 0; j < m.n; j++)
                    sumt += (m.m[i, j] * x[j]);
                rez[i] = sumt;
                sumt = 0;

                if (Math.Abs(rez[i]) < eps)
                    rez[i] = 0;
            }

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
