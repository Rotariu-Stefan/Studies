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
        int kmax;
        double xtest;
        double eps;

        double[] valori;
        int[] indici;
        double[] b;

        double[] xc;
        double[] r;
        double[] d;
        double ro0;

        double[] tvalori;
        int[] tindici;

        double[] mvalori;
        int[] mindici;

        #region set/get/afis vectori

        void smv(int i, double val)
        {
            mvalori[i - 1] = val;
        }
        double gmv(int i)
        {
            return mvalori[i - 1];
        }
        void afismv()
        {
            string s = "mvalori = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s += gmv(i) + " ";
            s += " )";
            wtext(s);
        }

        void smi(int i, int val)
        {
            mindici[i - 1] = val;
        }
        int gmi(int i)
        {
            return mindici[i - 1];
        }
        void afismi()
        {
            string s = "mindici = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s += gmi(i) + " ";
            s += " )";
            wtext(s);
        }

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

        void sr(int i, double val)
        {
            r[i - 1] = val;
        }
        double gr(int i)
        {
            return r[i - 1];
        }
        void afisr()
        {
            string s = "R = ( ";
            for (int i = 1; i <= n; i++)
                s += gr(i) + " ";
            s += " )";
            wtext(s);
        }

        void sd(int i, double val)
        {
            d[i - 1] = val;
        }
        double gd(int i)
        {
            return d[i - 1];
        }
        void afisd()
        {
            string s = "D = ( ";
            for (int i = 1; i <= n; i++)
                s += gd(i) + " ";
            s += " )";
            wtext(s);
        }

        void stv(int i, double val)
        {
            tvalori[i-1]=val;
        }
        double gtv(int i)
        {
            return tvalori[i - 1];
        }
        void afistv()
        {
            string s = "Tvalori = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s +=  gtv(i)+ " ";
            s += " )";
            wtext(s);
        }

        void sti(int i, int val)
        {
            tindici[i - 1] = val;
        }
        int gti(int i)
        {
            return tindici[i - 1];
        }
        void afisti()
        {
            string s = "Tindici = ( ";
            for (int i = 1; i <= NN + 1; i++)
                s += gti(i) + " ";
            s += " )";
            wtext(s);
        }
        #endregion

        void wtext(string s)
        {
            tbr.AppendText(s + Environment.NewLine);
        }   //afis text

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            openFileDialog1.InitialDirectory = "U:\\stravos\\CalculN\\cnT5\\";
            eps = Math.Pow(10, -8);
        }

        private void button2_Click(object sender, EventArgs e)  //browse
        {
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
                fntb.Text = openFileDialog1.FileName;
        }

        private void button1_Click(object sender, EventArgs e)  //memorize
        {
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

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

            init();

            wtext("Memorize Successful !");
            //for (int i = 0; i < n; i++)
                //wtext(valori[i].ToString()+"  "+(i+1).ToString());

            button3.Enabled = true;
            button5.Enabled = true;
        }

        private void button3_Click(object sender, EventArgs e)  //o iteratie
        {
            calcxc();

            afisxc();

            button4.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)  //verify curent
        {
            if (verify() == true)
                wtext("Corect ! -> " + xtest + " < " + eps);
            else
                wtext("Incorect ! -> " + xtest + " > " + eps);
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

        void init()
        {
            xc = new double[n];
            r = new double[n];
            d = new double[n];

            r = vecsub(matmulvec(xc), b);
            for (int i = 1; i <= n; i++)
                sd(i, -gr(i));
            ro0 = vecmulsc(r, r);
        }

        void calcxc()
        {
            double[] h = matmulvec(d);

            double sig = ro0 / vecmulsc(d, h);

            for (int i = 1; i <= n; i++)
                sxc(i, gxc(i) + sig * gd(i));

            for (int i = 1; i <= n; i++)
                sr(i, gr(i) + sig * h[i-1]);

            double ro1 = vecmulsc(r, r);

            double bb = ro1 / ro0;

            for (int i = 1; i <= n; i++)
                sd(i, bb * gd(i) - gr(i));

            ro0 = ro1;
        }

        bool verify()
        {
            /*double[] xt = new double[n];
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
                wtext("Incorect ! -> " + x + " > " + eps);*/

            double x = 0;
            for (int i = 0; i < n; i++)
                x += r[i] * r[i];
            x = Math.Sqrt(x);

            xtest = x;
            if (x < eps)
                return true;
            else
                return false;
        }

        double[] matmulvec(double[] x)
        {
            if (x.Length != n)
            {
                wtext("Mulvec index invalid !");
                return null;
            }

            double[] rez=new double[n];
            for (int i = 1; i <= n; i++)
            {
                for (int j = gi(i); j < gi(i + 1); j++)
                    rez[i - 1] += gv(j) * x[gi(j) - 1];
                rez[i - 1] += gv(i) * x[i - 1];
            }

            return rez;
        }

        void matmulmat()
        {
            mvalori = new double[NN + 1];
            mindici = new int[NN + 1];

            /*
            List<double>[] l = new List<double>[n];
            for (int i = 0; i < n; i++)
                l[i] = new List<double>();
            List<double> ld = new List<double>();

            double x = 0;
            for (int i1 = 1; i1 <= n; i1++)
                for (int i2 = 1; i2 <= n; i2++)
                {
                    for (int j1 = gi(i1); j1 < gi(i1 + 1); j1++)
                    //{
                        for (int j2 = gti(i2); j2 < gti(i2 + 1); j2++)
                        {
                            if (i1 == gti(j2))
                                x += gv(i1) * gtv(j2);
                            if (i2 == gi(j1))
                                x += gtv(i2) * gv(j1);
                            if (gi(j1) == gti(j2))
                                x += gv(j1) * gtv(j2);
                        }
                        if (i1 == i2)
                            ld.Add(x);
                        else
                            l[i1 - 1].Add(x);
                        x = 0;
                    }

            for (int i = 1; i <= n; i++)
                smv(i, ld[i-1]);*/
            /*
            List<cmv
            for(int i=1;i<=n;i++)
                for(int j=1;j<=n;j++)
                {*/



        }

        double vecmulsc(double[] x1, double[] x2)
        {
            double rez = 0;
            for (int i = 0; i < x1.Length; i++)
                rez += x1[i] * x2[i];

            return rez;
        }

        double[] vecsub(double[] x1, double[] x2)
        {
            double[] rez = new double[x1.Length];
            for (int i = 0; i < x1.Length; i++)
                rez[i] = x1[i] - x2[i];

            return rez;
        }

        void transpose()
        {
            tindici = new int[NN + 1];
            tvalori = new double[NN + 1];
            int[] cn = new int[n];

            for (int i = n + 2; i <= NN + 1; i++)
                cn[gi(i)-1]++;

            int ax=n+2;
            for (int i = 1; i <= n; i++)
            {
                stv(i, gv(i));
                sti(i, ax);
                ax += cn[i - 1];
            }

            for (int i = 1; i <= n; i++)
            {
                for (int j = gi(i); j < gi(i + 1); j++)
                {
                    stv(gti(gi(j)) + cn[gi(j) - 1] - 1, gv(j));
                    sti(gti(gi(j)) + cn[gi(j) - 1] - 1, i);
                    cn[gi(j) - 1]--;
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            kmax = Convert.ToInt32(tbkmax.Text);

            int k = 0;
            while (verify() == false && k < kmax)
            {
                calcxc();
                k++;
            }

            afisxc();
            wtext("Rezultat :" + Environment.NewLine + Environment.NewLine);

            if(verify()==true)
                wtext("Corect ! -> " + xtest + " < " + eps);
            else
                wtext("Incorect ! -> " + xtest + " > " + eps);

            button4.Enabled = true;
        }   //loop k
    }
}
