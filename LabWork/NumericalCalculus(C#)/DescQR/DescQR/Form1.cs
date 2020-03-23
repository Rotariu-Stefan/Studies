using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using dnAnalytics;
using dnAnalytics.LinearAlgebra;
using dnAnalytics.LinearAlgebra.Decomposition;
using dnAnalytics.LinearAlgebra.Solvers.Direct;

namespace DescQR
{
    public partial class Form1 : Form
    {
        double eps;
        TimeSpan tgiv, tqr;

        matp A, R, Qt, Ainv;
        double[] Binit, B, Xgivens;
        int n;

        Matrix lA, lB, Xqr, lAinv;
        Householder h;

        public Form1()
        {
            InitializeComponent();
        }

        #region gen utils
        private void show(string s)
        {
            tbshow.AppendText(s + Environment.NewLine);
        }

        private matp matRot(int n,int p, int q, double c, double s)
        {
            matp r = new matp(n);

            r.initmat();
            r.sm(p, p, c);
            r.sm(q, q, c);
            r.sm(q, p, -s);
            r.sm(p, q, s);

            return r;
        }

        private double[] calcB(matp a)
        {
            double[] b = new double[a.n];

            for (int i = 1; i <= a.n; i++)
                for (int j = 1; j <= a.n; j++)
                    b[i - 1] += j * a.gm(i, j);

            return b;
        }

        private double[] getSolTsup(matp Tsup, double[] b)
        {
            if (Tsup.n != b.Length)
                MessageBox.Show("Invalid Length !");
            double[] x = new double[Tsup.n];
            double sum = 0;

            x[n - 1] = b[n - 1] / Tsup.m[n - 1, n - 1];
            for (int i = n - 2; i >= 0; i--)
            {
                for (int j = i; j < n; j++)
                    sum += (Tsup.m[i, j] * x[j]);
                x[i] = (b[i] - sum) / Tsup.m[i, i];
                sum = 0;
            }
            return x;
        }

        private double[,] tomat(string s)
        {
            string[] mline = s.Split('\n');

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

            return mat;
        }

        private double eunorm(double[] v)
        {
            double r = 0;
            for (int i = 0; i < v.Length; i++)
                r += Math.Pow(v[i], 2);

            return Math.Sqrt(r);
        }
        #endregion

        #region Givens
        private void button1_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            eps = Math.Pow(10, -Convert.ToInt32(tbeps.Text));
            opr.eps = eps;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;

            A = new matp(tbA.Text);
            n=A.n;
            show("A was processed !");

            B = calcB(A);
            Binit = B;
            tbB.Clear();
            for (int i = 0; i < n; i++)
                tbB.AppendText(B[i].ToString() + Environment.NewLine);
            show("B calculated (init) !");

            Qt = new matp(n);
            Qt.initmat();
            show("Q initiated !");

            double r0 = 0;
            double c,s;

            R = A;
            for (int r = 1; r < n; r++)
                for (int i = r + 1; i <= n; i++)
                {
                    r0 = Math.Sqrt(R.gm(r, r) * R.gm(r, r) + R.gm(i, r) * R.gm(i, r));
                    if (Math.Abs(r0) < eps)
                    {
                        c = 1;
                        s = 0;
                    }
                    else
                    {
                        c = R.gm(r, r) / r0;
                        s = R.gm(i, r) / r0;
                    }

                    matp Ri = matRot(n, r, i, c, s);
                    R = opr.mul(Ri, R);
                    B = opr.mulsc(Ri, B);
                    Qt = opr.mul(Ri, Qt);
                }

            show("R,B,Q calculated !");

            if (R.checksing() == false)
                MessageBox.Show("Matrix singular !");
            else
            {
                button3.Enabled = true;
                button4.Enabled = true;
            }

            tbR.Clear();
            tbR.AppendText(R.ToString());

            tbQ.Clear();
            tbQ.AppendText(Qt.transpose().ToString());

            tbBb.Clear();
            for (int i = 0; i < n; i++)
                tbBb.AppendText(B[i].ToString() + Environment.NewLine);
            show("B calculated (re) !");

            tgiv = DateTime.Now - dt;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            Xgivens = getSolTsup(R, B);
            tbBb.Clear();
            for(int i=0;i<n;i++)
                tbBb.AppendText(Xgivens[i].ToString()+Environment.NewLine);

            show("Rezult calculated !");
            button2.Enabled = true;

            tgiv += DateTime.Now - dt;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] a1 = opr.mulsc(A, Xgivens);
            for (int i = 0; i < n; i++)
                a1[i] -= Binit[i];
            double t1 = eunorm(a1);

            if (t1 > eps)
                show(t1.ToString() + " > " + eps + " -> Incorect !");
            else
                show(t1.ToString() + " < " + eps + " -> Corect !");
            show("Tested AXgivens-b !");

            for (int i = 0; i < n; i++)
                a1[i] = Xgivens[i] - (i + 1);
            t1 = eunorm(a1);

            if (t1 > eps)
                show(t1.ToString() + " > " + eps + " -> Incorect !");
            else
                show(t1.ToString() + " < " + eps + " -> Corect !");
            show("Tested Xgivens-s !");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Ainv = new matp(n);
            double[] baux;

            for (int j = 1; j <= n; j++)
            {
                baux = getbcol(j);
                setAinvcol(j, getSolTsup(R, baux));
            }

            show("Ainv givens calculated !");
            tbAinv.Clear();
            tbAinv.AppendText(Ainv.ToString());
        }
        #endregion

        private double[] getbcol(int j)
        {
            double[] b=new double[n];
            for(int i=1;i<=n;i++)
                b[i-1]=Qt.gm(i,j);

            return b;
        }
        private void setAinvcol(int j,double[] x)
        {
            for(int i=1;i<=n;i++)
                Ainv.sm(i,j,x[i-1]);
        }

        private void button5_Click(object sender, EventArgs e)      //generate
        {
            n=Convert.ToInt32(tbrng.Text);
            int min=1,max=12;
            Random rn = new Random();

            string s="";

            for (int i = 0; i < n-1; i++)
            {
                s += rn.Next(min, max).ToString();
                for (int j = 1; j < n; j++)
                    s += " " + rn.Next(min, max).ToString();
                s += Environment.NewLine;
            }
            s += rn.Next(min, max).ToString();
            for (int j = 1; j < n; j++)
            s += " " + rn.Next(min, max).ToString();

            tbA.Clear();
            tbA.AppendText(s);
        }

        #region lib(QR)
        private void button6_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            eps = Math.Pow(10, -Convert.ToInt32(tbeps.Text));

            button7.Enabled = false;
            button8.Enabled = false;
            button9.Enabled = false;

            double[,] mat = tomat(tbA.Text);
            lA = new SparseMatrix(mat);
            n=lA.Rows;
            show("Lib A initiated !");

            lB = new SparseMatrix(n, 1);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    lB[i, 0] += mat[i, j] * (j + 1);
            tbB.Clear();
            tbB.AppendText(lB.ToString());

            h = new Householder(lA);
            tbR.Clear();
            tbR.AppendText(h.R().ToString());
            tbQ.Clear();
            tbQ.AppendText(h.Q().ToString());

            Matrix lQp = new SparseMatrix(n, n);
            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                    lQp[j, i] = h.Q()[i, j];

            Matrix lBp = lQp.Multiply(lB);
            tbBb.Clear();
            tbBb.AppendText(lBp.ToString());

            show("Lib Q,R calculated !");
            button7.Enabled = true;
            button9.Enabled = true;

            tqr = DateTime.Now - dt;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;

            Xqr=h.Solve(lB);
            tbBb.Clear();
            tbBb.AppendText(Xqr.ToString());

            show("Lib Xqr calculated !");
            button8.Enabled = true;

            tqr += DateTime.Now - dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Matrix ax = lA.Multiply(Xqr);
            ax.Subtract(lB);
            double t1=ax.L2Norm();

            if (t1 > eps)
                show(t1.ToString() + " > " + eps + " -> Incorect !");
            else
                show(t1.ToString() + " < " + eps + " -> Corect !");
            show("Lib Tested AXqr-b !");

            for (int i = 0; i < n; i++)
                ax[i,0] = Xqr[i,0] - (i + 1);
            t1 = ax.L2Norm();

            if (t1 > eps)
                show(t1.ToString() + " > " + eps + " -> Incorect !");
            else
                show(t1.ToString() + " < " + eps + " -> Corect !");
            show("Lib Tested Xqr-s !");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            lAinv = lA.Inverse();
            show("Lib Ainv calculated !");

            tbAinv.Clear();
            tbAinv.AppendText(lAinv.ToString());
        }
        #endregion

        #region compare/tests
        private void button10_Click(object sender, EventArgs e)
        {
            if (Ainv != null && lAinv != null)
            {
                Matrix Aigiv = new SparseMatrix(Ainv.m);
                Aigiv.Subtract(lAinv);

                double t1 = Aigiv.L1Norm();
                if (t1 > eps)
                    show(t1.ToString() + " > " + eps + " -> Incorect !");
                else
                    show(t1.ToString() + " < " + eps + " -> Corect !");
                show("Inverse Norm Tested !");
            }
            else
                MessageBox.Show("Calculate both inverses 1st !");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (tgiv.Milliseconds == 0 || tqr.Milliseconds == 0)
                MessageBox.Show("Do both calculations !");
            else
            {
                show("Givens time :" + tgiv.Seconds + " , " + tgiv.Milliseconds);
                show("QR time :" + tqr.Seconds + " , " + tqr.Milliseconds);
            }
        }
        #endregion
    }
}
