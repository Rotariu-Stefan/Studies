using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Clside
{
    public partial class Privatechat : Form
    {
        String name;
        d_sendmess senddel;
        d_changeon dclosed;

        public bool logged;

        public Privatechat()
        {
            InitializeComponent();
        }
        public Privatechat(string nm, d_sendmess dsmdel,d_changeon dcl)
        {
            name = nm;
            InitializeComponent();
            label2.Text = name;

            senddel = dsmdel;
            dclosed = dcl;

            logged = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (logged == true)
            {
                String s = textBox2.Text;
                textBox1.AppendText("You: " + s + "\n");
                textBox2.Clear();

                String mess = "mess~" + name + "~" + s;
                senddel(mess);
            }
            else
                MessageBox.Show("User disconected!");
        }

        private void Privatechat_FormClosed(object sender, FormClosedEventArgs e)
        {
            dclosed(name);
        }

        [STAThread]
        private void button2_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult res = openFileDialog1.ShowDialog();
            if (res == DialogResult.OK)
            {
                string fname = openFileDialog1.FileName;
                string text="";
                try
                {
                    text = File.ReadAllText(fname);
                    size = text.Length;
                }
                catch (IOException)
                {
                    MessageBox.Show("ERR: file not read!");
                }
          /*      if (size > 4080)
                {
                    //hmmm
                }
                else
                {*/
                    string[] fv=fname.Split('\\');
                    string fn = fv.Last();
                    String mess = "file~" + name + "~" + fn + "~" + text;
                    textBox1.AppendText("System: sent file " + fn + "  ...\n");
                    senddel(mess);
                //}
            }
        }

        public void showmess(string mess)
        {
            if (mess == "#DC#")
                textBox1.AppendText("System: " + name + " has disconnected!\n");
            else if (mess == "#RC#")
                textBox1.AppendText("System: " + name + " has reconnected!\n");
            else if(mess.StartsWith("#file#"))
                textBox1.AppendText("System: received file " + mess.Substring(6) + " ...\n");
            else
                textBox1.AppendText(name + ": " + mess + "\n");
        }
    }
}
