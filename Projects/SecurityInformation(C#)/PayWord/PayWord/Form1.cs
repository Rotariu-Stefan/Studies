using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PayWord.Parts;
using PayWord.Resources;

namespace PayWord
{
    public delegate void d_redemption();

    public partial class Payword : Form
    {
        broker Broker;
        user User;
        vendor Vendor;

        public d_redemption dredeem;

        public Payword()
        {
            InitializeComponent();
            dredeem = new d_redemption(redemption);

            Vendor = new vendor();
            Vendor.setUIfront(Vtextbox);
            Vendor.setfref(this);
            Broker = new broker();
            Broker.setUIfront(Btextbox);
        }

        private void Payword_Load(object sender, EventArgs e)
        {
            Vendor.loaded();
            Broker.loaded();

            Vlabel.Text = Vendor.name;
            Blabel.Text = Broker.ToString();
        }

        private void Usetbutton_Click(object sender, EventArgs e)
        {
            User = new user(Unametb.Text, Umaintb.Text, Ucctb.Text);
            User.setUIfront(UtextBox, Pleftlabel);
            User.loaded();

            Ulabel.Text = User.name;
            Ureqbutton.Enabled = true;
        }

        private void Ureqbutton_Click(object sender, EventArgs e)
        {
            User.sendrequest(Broker);

            Usetbutton.Enabled = false;
            Unametb.ReadOnly = true;
            Umaintb.ReadOnly = true;
            Ucctb.ReadOnly = true;
            Ureqbutton.Enabled = false;
            Ucomitbutton.Enabled = true;
        }

        private void Ucomitbutton_Click(object sender, EventArgs e)
        {
            User.sendcommit(Vendor);

            if (User.getcommited() == true)
            {
                Pleftlabel.Text = "Remaining paywords for the day: " + User.getpwnr().ToString();
                Ucomitbutton.Enabled = false;
                Upaybutton.Enabled = true;
                Pamount.ReadOnly = false;
                Pamount.Text = "1";
            }
            else
            { }
        }

        private void Upaybutton_Click(object sender, EventArgs e)
        {
            int nr;
            bool result = Int32.TryParse(Pamount.Text, out nr);
            if (result && nr > 0)
            {
                User.sendpayment(nr, Vendor);

            }
            else
                MessageBox.Show("Payment Invalid, write a pozitive number !");
        }

        private void redemption()
        {
            Vtextbox.AppendText("Cycle passed-->");
            Vendor.sendredemption(Broker);
            if (Vendor.gettrust())
            {
                User.renewcommit(Vendor);
                if (User.getcommited() == true)
                    Pleftlabel.Text = "Remaining paywords for the day: " + User.getpwnr().ToString();
            }
            else
            {
                MessageBox.Show("vendor commitment broken!");
            }
        }

        private void Payword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(1);
        }
    }

}
