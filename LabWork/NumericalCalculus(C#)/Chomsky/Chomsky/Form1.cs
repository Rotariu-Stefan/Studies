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
    public partial class Form1 : Form           //  -86.625       52.25         -11
    {
        int n;

        matp A;
        //matp L;
        //matp D;
        double[] D;
        //matp LT;

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
            //D = new matp(n);
            D = new double[n];
            //L = new matp(n);
            //LT = new matp(n);

            b=getB(textb.Text);

            for(int i=0;i<n;i++)
            {
                //L.m[i,i]=1;
                //LT.m[i,i]=1;
            }
            for (int p = 0; p < n; p++)
                doPas(p);
            int t=Convert.ToInt32(texteps.Text);
            eps = Math.Pow(10, -t);

            afisDesc();

            double detd=1;
            foreach (var it in D)
                detd *= it;

            textshow.Clear();
            textshow.AppendText("Det A=" + A.detSimSup() + Environment.NewLine);
            textshow.AppendText("Det L=" + 1 + "\tDet D=" + detd + "\tDet LT=" + 1 + Environment.NewLine);
            //textshow.AppendText("Det L=" + L.det() + "\tDet D=" + D.det() + "\tDet LT=" + LT.det()+Environment.NewLine);

            double[] z = getSolTinf(A, b);
            double[] y = getSolDiag2(D, z);
            double[] x = getSolTsup2(A, y);
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
                D[p] = A.m[p, p];

                for (int i = p; i < n; i++)
                {
                    if (Math.Abs(D[p]) > eps)
                    {
                        A.m[i, p] = A.m[p, i] / D[p];
                        //LT.m[p, i] = A.m[i, p];
                    }
                    else
                        MessageBox.Show("Divide by 0 !");
                }
            }
            else
            {
                double sumd = 0;
                for (int k = 0; k < p; k++)
                    sumd += D[k] * A.m[p, k] * A.m[p, k];
                D[p] = A.m[p, p] - sumd;

                double suml = 0;
                for (int i = p + 1; i < n; i++)
                {
                    for (int k = 0; k < p; k++)
                        suml = D[k] * A.m[i, k] * A.m[p, k];
                    if (Math.Abs(D[p]) > eps)
                    {
                        A.m[i, p] = (A.m[p, i] - suml) / D[p];
                        //LT.m[p, i] = A.m[i, p];
                    }
                    else
                        MessageBox.Show("Divide by 0 !");
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
        private double[] getSolTsup2(matp Tsup, double[] b)
        {
            if (Tsup.n != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Tsup.n];
            double sum = 0;

            x[n - 1] = b[n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i; j < n; j++)
                    sum += (Tsup.m[j, i] * x[j]);
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
                if(Math.Abs(Diag.m[i,i])>eps)
                    x[i] = b[i] / Diag.m[i, i];
                else
                    MessageBox.Show("Divide by 0 !");
            return x;
        }
        private double[] getSolDiag2(double[] Diag, double[] b)
        {
            if (Diag.Length != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Diag.Length];

            for (int i = 0; i < n; i++)
                if(Math.Abs(Diag[i])>eps)
                    x[i] = b[i] / Diag[i];
                else
                    MessageBox.Show("Divide by 0 !");
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
            double[] rez = opr.mulscSimSup(A, xchol);
            rez = vsub(rez, b);
            double ex = eunorm(rez);
            MessageBox.Show("EX:" + ex.ToString());
            if(ex<eps)
                return true;
            else
                return false;
        }

        private void afisDesc()
        {
            textL.Clear();
            //textL.AppendText(L.ToString());
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (i > j)
                        textL.AppendText(" | " + A.m[i, j].ToString());
                    else
                        if (i < j)
                            textL.AppendText(" | " + "0");
                        else
                            textL.AppendText(" | " + "1");
                textL.AppendText(" |\n");
            }
            textD.Clear();
            //textD.AppendText(D.ToString());
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (i == j)
                        textD.AppendText(" | " + D[i].ToString());
                    else
                        textD.AppendText(" | " + "0");
                textD.AppendText(" |\n");
            }
            textLT.Clear();
            //textLT.AppendText(LT.ToString());
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                    if (i < j)
                        textLT.AppendText(" | " + A.m[j, i].ToString());
                    else
                        if (i > j)
                            textLT.AppendText(" | " + "0");
                        else
                            textLT.AppendText(" | " + "1");
                textLT.AppendText(" |\n");
            }
        }
    }
}
