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
    public delegate void d_changeon(string f);
    public delegate void d_takemess(string f, string m);

    public partial class Mainchat : Form
    {
        public d_changeon dchon;
        public d_changeon dchoff;
        public d_changeon dchcreate;//dsfgdfhg
        public d_takemess dtkm;
        public d_changeon dclosed;
        public d_changeon daddf;

        d_sendmess senddel;

        Dictionary<string, Privatechat> oclist;

        public Mainchat()
        {
            InitializeComponent();

            oclist = new Dictionary<string, Privatechat>();
        }
        public Mainchat(string user, List<string> friends, d_sendmess dsmdel)
        {
            InitializeComponent();

            label2.Text = user;
            listBox1.Items.AddRange(friends.ToArray());

            dchon = new d_changeon(changeon);
            dchoff = new d_changeon(changeoff);
            dchcreate = new d_changeon(openprchat);//fswgdd
            dtkm = new d_takemess(takemess);
            dclosed = new d_changeon(chatclosed);
            daddf = new d_changeon(addfriend);

            senddel = dsmdel;

            oclist = new Dictionary<string, Privatechat>();
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            openprchat(listBox1.SelectedItem.ToString());
        }


        private void button2_Click(object sender, EventArgs e)      //add
        {
            String s = textBox1.Text;
            String mess = "add~" + s;
            textBox1.Text = "";
            senddel(mess);
        }

        private void button1_Click(object sender, EventArgs e)      //logout
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)      //remove
        {
            object o = listBox1.SelectedItem;
            if (o != null)
            {
                String s = o.ToString();
                listBox1.Items.Remove(s);
                if(s.StartsWith("[O]"))
                    s=s.Substring(3);
                String mess="remove~"+s;
                senddel(mess);
            }
            else
                MessageBox.Show("Nimic selectat!");
        }

        public void changeon(string friend)
        {
            int i = listBox1.Items.IndexOf(friend);
            string newf = "[O]" + friend;
            listBox1.Items.RemoveAt(i);
            listBox1.Items.Insert(i, newf);
            if (oclist.ContainsKey(friend))
            {
                oclist[friend].logged = true;
                oclist[friend].showmess("#RC#");
            }
        }
        public void changeoff(string friend)
        {
            string fo = "[O]" + friend;
            int i = listBox1.Items.IndexOf(fo);
            listBox1.Items.RemoveAt(i);
            listBox1.Items.Insert(i, friend);
            if (oclist.ContainsKey(friend))
            {
                oclist[friend].logged = false;
                oclist[friend].showmess("#DC#");
            }        
        }
        public void openprchat(string friend)
        {
            String ss = friend;
            if (ss.StartsWith("[O]"))
            {
                String s = ss.Substring(3);
                if (oclist.ContainsKey(s))
                { }
                else
                {
                    Privatechat pc = new Privatechat(s, senddel,dclosed);
                    oclist.Add(s, pc);
                    pc.Show();
                }
            }
            else
                MessageBox.Show(ss + " not online!");
        }
        public void takemess(string friend, string mess)
        {
            oclist[friend].showmess(mess);
        }
        public void chatclosed(string friend)
        {
            oclist.Remove(friend);
        }
        public void addfriend(string friend)
        {
            listBox1.Items.Add(friend);
        }
    }
}
