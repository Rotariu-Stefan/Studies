using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using T1;

namespace T1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double[,] m1 ={{Convert.ToDouble(textBox5.Text),Convert.ToDouble(textBox6.Text),Convert.ToDouble(textBox8.Text),Convert.ToDouble(textBox7.Text),Convert.ToDouble(textBox12.Text),Convert.ToDouble(textBox11.Text),Convert.ToDouble(textBox10.Text),Convert.ToDouble(textBox9.Text)},
                              {Convert.ToDouble(textBox20.Text),Convert.ToDouble(textBox19.Text),Convert.ToDouble(textBox18.Text),Convert.ToDouble(textBox17.Text),Convert.ToDouble(textBox16.Text),Convert.ToDouble(textBox15.Text),Convert.ToDouble(textBox14.Text),Convert.ToDouble(textBox13.Text)},
                              {Convert.ToDouble(textBox36.Text),Convert.ToDouble(textBox35.Text),Convert.ToDouble(textBox34.Text),Convert.ToDouble(textBox33.Text),Convert.ToDouble(textBox32.Text),Convert.ToDouble(textBox31.Text),Convert.ToDouble(textBox30.Text),Convert.ToDouble(textBox29.Text)},
                              {Convert.ToDouble(textBox28.Text),Convert.ToDouble(textBox27.Text),Convert.ToDouble(textBox26.Text),Convert.ToDouble(textBox25.Text),Convert.ToDouble(textBox24.Text),Convert.ToDouble(textBox23.Text),Convert.ToDouble(textBox22.Text),Convert.ToDouble(textBox21.Text)},
                              {Convert.ToDouble(textBox68.Text),Convert.ToDouble(textBox67.Text),Convert.ToDouble(textBox66.Text),Convert.ToDouble(textBox65.Text),Convert.ToDouble(textBox64.Text),Convert.ToDouble(textBox63.Text),Convert.ToDouble(textBox62.Text),Convert.ToDouble(textBox61.Text)},
                              {Convert.ToDouble(textBox60.Text),Convert.ToDouble(textBox59.Text),Convert.ToDouble(textBox58.Text),Convert.ToDouble(textBox57.Text),Convert.ToDouble(textBox56.Text),Convert.ToDouble(textBox55.Text),Convert.ToDouble(textBox54.Text),Convert.ToDouble(textBox53.Text)},
                              {Convert.ToDouble(textBox52.Text),Convert.ToDouble(textBox51.Text),Convert.ToDouble(textBox50.Text),Convert.ToDouble(textBox49.Text),Convert.ToDouble(textBox48.Text),Convert.ToDouble(textBox47.Text),Convert.ToDouble(textBox46.Text),Convert.ToDouble(textBox45.Text)},
                              {Convert.ToDouble(textBox44.Text),Convert.ToDouble(textBox43.Text),Convert.ToDouble(textBox42.Text),Convert.ToDouble(textBox41.Text),Convert.ToDouble(textBox40.Text),Convert.ToDouble(textBox39.Text),Convert.ToDouble(textBox38.Text),Convert.ToDouble(textBox37.Text)}};

            double[,] m2 ={{Convert.ToDouble(textBox132.Text),Convert.ToDouble(textBox131.Text),Convert.ToDouble(textBox130.Text),Convert.ToDouble(textBox129.Text),Convert.ToDouble(textBox128.Text),Convert.ToDouble(textBox127.Text),Convert.ToDouble(textBox126.Text),Convert.ToDouble(textBox125.Text)},
                              {Convert.ToDouble(textBox124.Text),Convert.ToDouble(textBox123.Text),Convert.ToDouble(textBox122.Text),Convert.ToDouble(textBox121.Text),Convert.ToDouble(textBox120.Text),Convert.ToDouble(textBox119.Text),Convert.ToDouble(textBox118.Text),Convert.ToDouble(textBox117.Text)},
                              {Convert.ToDouble(textBox116.Text),Convert.ToDouble(textBox115.Text),Convert.ToDouble(textBox114.Text),Convert.ToDouble(textBox113.Text),Convert.ToDouble(textBox112.Text),Convert.ToDouble(textBox111.Text),Convert.ToDouble(textBox110.Text),Convert.ToDouble(textBox109.Text)},
                              {Convert.ToDouble(textBox108.Text),Convert.ToDouble(textBox107.Text),Convert.ToDouble(textBox106.Text),Convert.ToDouble(textBox105.Text),Convert.ToDouble(textBox104.Text),Convert.ToDouble(textBox103.Text),Convert.ToDouble(textBox102.Text),Convert.ToDouble(textBox101.Text)},
                              {Convert.ToDouble(textBox100.Text),Convert.ToDouble(textBox99.Text),Convert.ToDouble(textBox98.Text),Convert.ToDouble(textBox97.Text),Convert.ToDouble(textBox96.Text),Convert.ToDouble(textBox95.Text),Convert.ToDouble(textBox94.Text),Convert.ToDouble(textBox93.Text)},
                              {Convert.ToDouble(textBox92.Text),Convert.ToDouble(textBox91.Text),Convert.ToDouble(textBox90.Text),Convert.ToDouble(textBox89.Text),Convert.ToDouble(textBox88.Text),Convert.ToDouble(textBox87.Text),Convert.ToDouble(textBox86.Text),Convert.ToDouble(textBox85.Text)},
                              {Convert.ToDouble(textBox84.Text),Convert.ToDouble(textBox83.Text),Convert.ToDouble(textBox82.Text),Convert.ToDouble(textBox81.Text),Convert.ToDouble(textBox80.Text),Convert.ToDouble(textBox79.Text),Convert.ToDouble(textBox78.Text),Convert.ToDouble(textBox77.Text)},
                              {Convert.ToDouble(textBox76.Text),Convert.ToDouble(textBox75.Text),Convert.ToDouble(textBox74.Text),Convert.ToDouble(textBox73.Text),Convert.ToDouble(textBox72.Text),Convert.ToDouble(textBox71.Text),Convert.ToDouble(textBox70.Text),Convert.ToDouble(textBox69.Text)}};
            matp M1 = new matp(m1);
            matp M2 = new matp(m2);

            mul.Clear();
            mulStrass.Clear();

            mul.AppendText(opr.mul(M1, M2).ToString());
            mulStrass.AppendText(P3.mulStrass(M1, M2, 4).ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            P1 p1=new P1();
            p1.calcprecizie();

            textBox1.Clear();
            textBox1.AppendText(p1.u.ToString());
            textBox2.Clear();
            textBox2.AppendText(p1.m.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            P1 p1 = new P1();
            p1.calcprecizie();

            P2 p2 = new P2();
            textBox3.Clear();
            textBox3.AppendText(p2.retasoc(p1.u).ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            P1 p1 = new P1();
            p1.calcprecizie();

            P2 p2 = new P2();
            textBox4.Clear();
            textBox4.AppendText(p2.retasoc(p1.u*10).ToString());
        }
    }
}
