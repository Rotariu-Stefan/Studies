using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chomsky
{
    public partial class Form1 : Form
    {
        int n;

        matp A;
        matp L;
        matp D;
        matp LT;

        double[] b;
        double eps;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private double[] getB(string s)
        {
            string[] ss = s.Split('\n');
            double[] b = new double[ss.Length];

            for (int i = 0; i < b.Length; i++)
                b[i] = Convert.ToDouble(ss[i]);
            return b;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            A = new matp(textA.Text);
            n = A.n;
            D = new matp(n);
            L = new matp(n);
            LT = new matp(n);

            b=getB(textb.Text);

            for(int i=0;i<n;i++)
            {
                L.m[i,i]=1;
                LT.m[i,i]=1;
            }
            for (int p = 0; p < n; p++)
                doPas(p);
            int t=Convert.ToInt32(texteps.Text);
            eps = Math.Pow(10, -t);

            textL.Clear();
            textL.AppendText(L.ToString());
            textD.Clear();
            textD.AppendText(D.ToString());
            textLT.Clear();
            textLT.AppendText(LT.ToString());

            textshow.Clear();
            textshow.AppendText("Det A=" + A.det()+Environment.NewLine);
            textshow.AppendText("Det L=" + L.det() + "\tDet D=" + D.det() + "\tDet LT=" + LT.det()+Environment.NewLine);

            double[] z = getSolTinf(L, b);
            double[] y = getSolDiag(D, z);
            double[] x = getSolTsup(LT, y);
            foreach (var it in x)
                textshow.AppendText(it + "  \t");

            MessageBox.Show("Solution is: "+testChol(x).ToString());
                

        }

        private void doPas(int p)
        {
            if (p > n)
                return;
            if (p == 0)
            {
                D.m[p, p] = A.m[p, p];

                for (int i = p; i < n; i++)
                {
                    L.m[i, p] = A.m[i, p] / D.m[p, p];
                    LT.m[p, i] = L.m[i, p];
                }
            }
            else
            {
                double sumd = 0;
                for (int k = 0; k < p; k++)
                    sumd += D.m[k, k] * L.m[p, k] * L.m[p, k];
                D.m[p, p] = A.m[p, p] - sumd;

                if (p != n - 1)
                {
                    double suml = 0;
                    for (int i = p; i < n; i++)
                    {
                        for (int k = 0; k < p; k++)
                            suml = D.m[k, k] * L.m[i, k] * L.m[p, k];
                        L.m[i, p] = (A.m[i, p] - suml) / D.m[p, p];
                        LT.m[p, i] = L.m[i, p];
                    }
                }
            }
        }

        private double[] getSolTinf(matp Tinf, double[] b)
        {
            if (Tinf.n != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Tinf.n];
            double sum = 0;

            x[0] = b[0];
            for (int i = 1; i < n; i++)
            {
                for (int j = 0; j < i; j++)
                    sum += (Tinf.m[i, j] * x[j]);
                x[i] = b[i] - sum;
                sum = 0;
            }
            return x;
        }
        private double[] getSolTsup(matp Tsup, double[] b)
        {
            if (Tsup.n != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Tsup.n];
            double sum = 0;

            x[n - 1] = b[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i; j < n; j++)
                    sum += (Tsup.m[i, j] * x[j]);
                x[i] = b[i] - sum;
                sum = 0;
            }
            return x;
        }
        private double[] getSolDiag(matp Diag, double[] b)
        {
            if (Diag.n != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Diag.n];

            for (int i = 0; i < n; i++)
                x[i] = b[i] / Diag.m[i, i];
            return x;
        }

        private double[] vsub(double[] v1, double[] v2)
        {
            if (v1.Length != v2.Length)
                MessageBox.Show("Invalid Length !");
            double[] rez = new double[v1.Length];
            for (int i = 0; i < v1.Length; i++)
                rez[i] = v1[i] - v2[i];

            return rez;
        }
        private double eunorm(double[] v)
        {
            double r = 0;
            for (int i = 0; i < v.Length; i++)
                r += Math.Pow(v[i], 2);

            return Math.Sqrt(r);
        }
        private bool testChol(double[] xchol)
        {
            double[] rez = opr.mulsc(A, xchol);
            rez = vsub(rez, b);
            double ex = eunorm(rez);
            if(ex<eps)
                return true;
            else
                return false;
        }
    }
}
