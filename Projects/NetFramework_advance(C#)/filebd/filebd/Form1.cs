using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Sql;
using System.Data.SqlClient;

namespace filebd
{
    public partial class Form1 : Form
    {
        string rootpath;
        List<line> table;
        int idcnt;
        bool tview;

        SqlConnection conn;

        public Form1()
        {
            InitializeComponent();

            table = new List<line>();
            idcnt=0;
            tview = false;
        }

        private void loadbutton_Click(object sender, EventArgs e)
        {
            rootpath = pathtb.Text;
            mainlist.Items.Clear();

            cleartable();
            populatetable(rootpath, -1);
            mainlist.Items.Add((string)"Database Loaded !");
        }

        private void browsebutton_Click(object sender, EventArgs e)
        {
            DialogResult res = folderBrowserDialog1.ShowDialog();
            if (res == DialogResult.OK)
                pathtb.Text = folderBrowserDialog1.SelectedPath;
        }

        private void showdbbutton_Click(object sender, EventArgs e)
        {
            mainlist.Items.Clear();
            setview(-1, 0);
            tview = true;
        }

        private void showtb_Click(object sender, EventArgs e)
        {
            mainlist.Items.Clear();            
            showtable();
            tview = false;
        }

        private void mainlist_SelectedValueChanged(object sender, EventArgs e)
        {
            if (tview == true)
            {
                string sel=mainlist.SelectedItem.ToString();
                sel = sel.Substring(sel.IndexOf(']') + 2);
                var par = from s in table where s.name == sel select s.id;
                var items = from s in table where s.parentid == par.First() select s;

                listView1.Clear();
                foreach (var it in items)
                    if (it.type == "dir")
                        listView1.Items.Add(it.name);
                    else
                        listView1.Items.Add(it.name);
            }
        }

        private string[] getdirchilds(string path)
        {
            List<string> chl = new List<string>();

            string[] dl = Directory.GetDirectories(rootpath);
            foreach (string it in dl)
                chl.Add("[D]"+getname(it));

            string[] fl = Directory.GetFiles(rootpath);
            foreach (string it in fl)
                chl.Add("[F]"+getname(it));

            return chl.ToArray();
        }

        private string getname(string path)
        {
            string[] pl = path.Split('\\');
            return pl[pl.Count()-1];
        }

        private void populatetable(string path, int par)
        {
            table.Add(new line(idcnt, getname(path), "dir", par));
            int x = idcnt;
            idcnt++;
            foreach (var it in Directory.GetDirectories(path))
                populatetable(it, x);
            foreach (var it in Directory.GetFiles(path))
            {
                table.Add(new line(idcnt, getname(it), "file", x));
                idcnt++;
            }
        }
        private void cleartable()
        {
            table = new List<line>();
        }

        private void showtable()
        {
            mainlist.Items.Clear();
            foreach (line it in table)
                mainlist.Items.Add(it.ToString());
        }

        private void setview(int par, int tabs)
        {
                var items = from s in table where s.parentid == par select s;
                foreach (var it in items)
                {
                    string t = "";
                    for (int i = 0; i < tabs; i++)
                        t += "---";
                    if (it.type == "dir")
                        mainlist.Items.Add(t + "[D] " + it.name);
                    if (it.type == "file")
                        mainlist.Items.Add(t + "[F] " + it.name);
                    setview(it.id, tabs + 1);
                }
        }

        private void loadbutton2_Click(object sender, EventArgs e)
        {
            cleartable();           //->mysql ado.net conn

            conn = new SqlConnection("user id=stefan.rotariu;password=Arrakis7;server=172.17.17.15:8080;Trusted_Connection=yes;database=filedata;connection timeout=2");
            conn.Open();

            /*
            SqlDataAdapter read = new SqlDataAdapter("SELECT id,name,type,parentid FROM fdata", conn);
            DataTable dt = new DataTable();
            read.Fill(dt);
            object[] fields;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                fields = dt.Rows[i].ItemArray;
                table.Add(new line(fields));
            }*/

            mainlist.Items.Add((string)"Database Loaded !");
        }

        private void savebutton_Click(object sender, EventArgs e)
        {
            string sqldel = "DELETE * FROM fdata";
            SqlCommand comm = new SqlCommand(sqldel);
            comm.Connection = conn;
            comm.ExecuteNonQuery();
            foreach (var it in table)
            {
                comm = new SqlCommand(getinsquery(it));
                comm.Connection = conn;
                comm.ExecuteNonQuery();
            }

            MessageBox.Show("Database Updated");
        }
        private string getinsquery(line li)
        {
            return "INSERT INTO fdata(id,name,type,parentid) VALUES('" + li.id.ToString() + "','" + li.name + "','" + li.type + "','" + li.parentid.ToString() + "')";
        }

        private void renbutton_Click(object sender, EventArgs e)
        {
            string sel=mainlist.SelectedItem.ToString();
            sel = sel.Substring(sel.IndexOf(']') + 2);

            var entry = from s in table where s.name == sel select s;
            entry.First().name = rentb.Text;
            mainlist.SelectedItem = (string)rentb.Text;            
        }

        private void delbutton_Click(object sender, EventArgs e)
        {
            string sel = mainlist.SelectedItem.ToString();
            sel = sel.Substring(sel.IndexOf(']') + 2);
            var ffs = from s in table where s.name==sel select s.id;

            delfile(ffs.First());

            setview(-1, 0);
        }

        private void delfile(int id)
        {
            var items = from s in table where s.parentid == id select s;
            foreach (var it in items)
            {
                if (it.type == "dir")
                    delfile(it.id);
                if (it.type == "file")
                    table.Remove(it);
            }
            var fthis = from s in table where s.id == id select s;
            table.Remove(fthis.First());
        }
    }
}
