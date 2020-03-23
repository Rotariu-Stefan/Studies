using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayWord.Resources;

namespace PayWord.Parts
{
    public class user
    {
        TextBox utb;
        Label pleft;

        public string name;
        string mail;
        string card;

        private rsakey upvk;
        public rsakey upbk;

        bcertificate cert;
        ucommit ucomm;

        int pwnr = 1000;
        bool commited;
        BigInteger[] pwords;
        int currentpw;


        public user()
        {
            name = "defaultname";
            mail = "defaultmail";
            card = "defaultcard";
            commited = false;

            rsa ursa = new rsa();
            upvk = ursa.getPrivate();
            upbk = ursa.getPublic();

        }
        public user(string n, string m, string c)
        {
            name = n;
            mail = m;
            card = c;
            commited = false;

            rsa ursa = new rsa();
            upvk = ursa.getPrivate();
            upbk = ursa.getPublic();
        }
        public void setUIfront(TextBox tb, Label l)
        {
            utb = tb;
            pleft = l;
        }
        public void loaded()
        {
            utb.AppendText("User initialized ! \n\n");
        }

        private void genpaywords(int nr)
        {
            pwords = new BigInteger[nr];

            Random rn = new Random();
            BigInteger cn = BigInteger.genPseudoPrime(64, 0, rn);

            BigInteger ci=cn;
            for (int i = nr - 1; i >= 0; i--)
            {
                pwords[i] = hashf.hash(ci.ToString());
                ci = pwords[i];
            }

        }

        public void sendrequest(broker b)
        {
            urequest ureq = new urequest(name, mail, card, upbk);
            utb.AppendText("Sent certificate request to broker:" + b + " !\n");
            b.getrequest(ureq, this);
        }
        public void getcertificate(bcertificate c)
        {
            utb.AppendText("\tCertificate received from: " + c.getbroker() + " !\n");
            cert = c;
            //set nr or paywords from certificate
            string[] ax = cert.getinfo().Split(':');
            pwnr = Convert.ToInt32(ax[1]);
        }

        public void sendcommit(vendor v)
        {
            genpaywords(pwnr + 1);
            currentpw = 0;
            utb.AppendText("Commitment : Generated 1$ paywords(" + pwnr + ") !\n");

            string info = "adinfo";
            string sigstr = v.name + "," + cert.ToString() + "," + pwords[0].ToString() + "," + DateTime.Now.ToString() + "," + info;
            BigInteger sigh = hashf.hash(sigstr);
            BigInteger sigU = rsa.createSig(sigh, upvk);

            ucommit cmes = new ucommit(v.name, cert, pwords[0], DateTime.Now, info, sigU);
            ucomm = cmes;
            utb.AppendText("\tSent comitment to " + v.name + " !\n");
            v.getcommit(cmes, this);
        }
        public void renewcommit(vendor v)
        {
            utb.AppendText("Renewing comitment !\n");
            sendcommit(v);
        }
        public void comconfirm(bool c,vendor v)
        {
            if (c == true)
                utb.AppendText("\tComitment to vendor: " + v.name + " confirmed !\n");
            else
            {
                ucomm = null;
                utb.AppendText("\tComitment to vendor: " + v.name + " failed !\n");
            }
            commited = c;
        }
        private void changepw(int nr)
        {
            string nstr = pleft.Text.Substring(32);
            int n = Convert.ToInt32(nstr);
            n -= nr;
            pleft.Text = "Remaining paywords for the day: " + n.ToString();
        }
        public bool getcommited()
        {
            return commited;
        }
        public int getpwnr()
        {
            return pwnr;
        }
        public void sendpayment(int pnr, vendor v)
        {
            currentpw += pnr;
            if (currentpw > pwnr)
                MessageBox.Show("Pay word limit for Today reached !");
            else
            {
                payment pay = new payment(pwords[currentpw], pnr);

                utb.AppendText("Sent payment of " + pnr + "$ to vendor: " + v.name + " !\n");
                v.getpayment(pay, this);
                changepw(pnr);
            }
        }
        public void payconfirm(bool c, vendor v, int i)
        {
            if (c == true)
                utb.AppendText("\tPayment to vendor: " + v.name + " confirmed !\n");
            else
                utb.AppendText("\tPayment to vendor: " + v.name + " failed !\n");
            currentpw -= i;
        }
    }
}
