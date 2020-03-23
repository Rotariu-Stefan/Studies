using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Clside
{
    public delegate void d_sigclose();

    public partial class Form1 : Form
    {
        public d_sigclose dsc;
        public String uname;
        public String upass;

        public Form1()
        {
            InitializeComponent();

            dsc = new d_sigclose(sigclose);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            uname = textBox1.Text;
            upass = textBox2.Text;
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            uname = "";
            upass = "";
        }

        public void sigclose()
        {
            this.Close();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Environment.Exit(0);
        }
    }
}
