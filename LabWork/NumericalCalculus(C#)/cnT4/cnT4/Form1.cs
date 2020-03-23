using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace cnT4
{
    public partial class Form1 : Form
    {
        int n;
        int NN;
        double w;
        int kmax;
        double eps;

        double[] valori;
        int[] indici;
        double[] b;

        double[] xc;
        double[] xp;

        #region set/get/afis vectori

        void sv(int i, double val)
        {
            valori[i-1]=val;
        }
        double gv(int i)
        {
            return valori[i - 1];
        }
        void afisv()
        {
            string s = "valori = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s +=  gv(i)+ " ";
            s += " )";
            wtext(s);
        }

        void si(int i, int val)
        {
            indici[i - 1] = val;
        }
        int gi(int i)
        {
            return indici[i - 1];
        }
        void afisi()
        {
            string s = "indici = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s += gi(i) + " ";
            s += " )";
            wtext(s);
        }

        void sb(int i, double val)
        {
            b[i-1]=val;
        }
        double gb(int i)
        {
            return b[i - 1];
        }
        void afisb()
        {
            string s = "B = ( ";
            for (int i = 1; i <= n; i++)
                s += gb(i) + " ";
            s += " )";
            wtext(s);
        }

        void sxc(int i, double val)
        {
            xc[i - 1] = val;
        }
        double gxc(int i)
        {
            return xc[i - 1];
        }
        void afisxc()
        {
            string s = "Xc = ( ";
            for (int i = 1; i <= n; i++)
                s += gxc(i) + " ";
            s += " )";
            wtext(s);
        }

        void sxp(int i, double val)
        {
            xp[i - 1] = val;
        }
        double gxp(int i)
        {
            return xp[i - 1];
        }
        void afisxp()
        {
            string s = "Xp = ( ";
            for (int i = 1; i <= n; i++)
                s += gxp(i) + " ";
            s += " )";
            wtext(s);
        }

        #endregion

        void wtext(string s)
        {
            tbr.AppendText(s + Environment.NewLine);
        }

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "U:\\stravos\\CalculN\\matr";
            w = 1;
            kmax = 10;
            eps = Math.Pow(10, -8);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                fntb.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ll;
            StreamReader fs = new StreamReader(fntb.Text);

            ll=fs.ReadLine();
            n = Convert.ToInt32(ll);
            
            int lcount = File.ReadAllLines(fntb.Text).Length;
            NN = lcount - n - 3;
            wtext("n: "+n);
            wtext("NN: " + NN);

            valori = new double[NN + 1];
            indici = new int[NN + 1];
            b = new double[n];

            memorize(fs);

            xc = new double[n];
            xp = new double[n];

            double[] x0 = { 1, 2, 3, 4, 5 };
            initxp(x0);

            wtext("Memorize Successful !");
            //for (int i = 0; i < n; i++)
                //wtext(valori[i].ToString()+"  "+(i+1).ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            calcxp();
            for (int i = 1; i <= n; i++)
                sxp(i, gxc(i));

            afisxc();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            verify();
        }

        void memorize(StreamReader fs)
        {
            string test = "";
            string tline;

            fs.ReadLine();
            for (int i = 1; i <= n; i++)
            {
                tline = fs.ReadLine();
                sb(i, Convert.ToDouble(tline));
            }
            wtext("B memorized !");     //afisb();

            int cline = 1;
            int cel = n + 2;
            double[] vt = new double[3];

            fs.ReadLine();
            for (int i = 1; i <= NN; i++)
            {
                tline = fs.ReadLine();
                for (int ii = 0; ii < 3; ii++)
                {
                    vt[ii] = Convert.ToDouble(tline.Split(',')[ii].Trim());
                }

                if (vt[1] != cline)
                {
                    si(cline + 1, cel);
                    cline++;
                }
                if (vt[1] == vt[2])
                {
                    if (vt[0] == 0)
                        MessageBox.Show("0 Diagonal !");
                    sv(cline, vt[0]);
                }
                else
                {
                    if (vt[0] == 0)
                        MessageBox.Show("0 Val ?? ");
                    sv(cel, vt[0]);
                    si(cel, (int)vt[2]);
                    cel++;
                }
            }
            sv(n + 1, 0);
            si(1, n + 2);
            si(n + 1, NN + 2);

            wtext("Valori memorized !");    //afisv();
            wtext("Indici memorized !");    //afisi();
        }

        void initxp(double[] x)
        {
            if (x.Length != n)
            {
                wtext("Init invalid !");
                return;
            }
            for (int i = 1; i <= n; i++)
                sxp(i, x[i - 1]);
        }

        void calcxp()
        {
            double s1=0;
            double s2=0;
            double r=0;

            for (int i = 1; i <= n; i++)
            {
                s1 = 0;
                s2 = 0;
                r = 0;

                for (int j = gi(i); j < gi(i + 1); j++)
                {
                    if (gi(j) < i)
                        s1 += (gv(j) * gxc(gi(j)));
                    if (gi(j) > i && gi(j) <= n)
                        s2 += (gv(j) * gxp(gi(j)));
                }

                if (gv(i) != 0)
                    r = (1 - w) * gxp(i) + (w / gv(i)) * (gb(i) - s1 - s2);
                else
                    MessageBox.Show("0 !!"+i.ToString());
                sxc(i, r);
            }
        }

        void verify()
        {
            double[] xt = new double[n];
            for (int i = 1; i <= n; i++)
            {
                for (int j = gi(i); j < gi(i + 1); j++)
                    xt[i - 1] += gv(j) * gxc(gi(j));
                xt[i - 1] += gv(i) * gxc(i);
                xt[i - 1] -= gb(i);
            }

            double x = 0;
            for (int i = 0; i < n; i++)
                x += xt[i] * xt[i];
            x = Math.Sqrt(x);
            if (x < eps)
                wtext("Corect ! -> " + x + " < " + eps);
            else
                wtext("Incorect ! -> " + x + " > " + eps);
        }


    }
}
