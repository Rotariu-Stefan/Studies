using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using PayWord.Resources;

namespace PayWord.Parts
{
    public class broker
    {
        TextBox btb;

        string name;
        List<bcertificate> clist;

        private rsakey bpvk;
        public rsakey bpbk;

        public broker()
        {
            name = "Default Broker";
            clist = new List<bcertificate>();

            rsa brsa = new rsa();
            bpvk = brsa.getPrivate();
            bpbk = brsa.getPublic();
        }
        public void setUIfront(TextBox tb)
        {
            btb = tb;
        }
        public void loaded()
        {
            btb.AppendText("Broker initialized ! \n");
        }

        public void getrequest(urequest ureq, user u)
        {
            btb.AppendText("Received request from user: " + ureq.getname() + " !\n");

            DateTime exp = DateTime.Now;
            exp = exp.AddMonths(1);
            exp = exp.AddDays(1);
            //string info = "Certificate generated for user: " + ureq.getname() + " on " + DateTime.Now;
            string info = "Nr. of paywords:1100";
            string sigstr = this.ToString() + "," + ureq.getname() + "," + ureq.getmail() + "," + ureq.getkey().ToString() + "," + exp.ToString() + "," + info;
            BigInteger sigh = hashf.hash(sigstr);
            BigInteger sigB = rsa.createSig(sigh, bpvk);

            bcertificate cert = new bcertificate(this, ureq.getname(), ureq.getmail(), ureq.getkey(), exp, info, sigB);
            clist.Add(cert);

            sendcertificate(cert, u);
        }
        public void sendcertificate(bcertificate c, user u)
        {
            btb.AppendText("\tRequest aproved. Sent certificate to user: " + c.getuser() +" !\n");
            u.getcertificate(c);
        }
        public void getredemption(redmess rm, vendor v)
        {
            btb.AppendText("Redemption received from vendor: "+v.name+" !\n");
            if(verifyuser(rm.getucomm()))
            {
                BigInteger start = rm.getucomm().getc0();
                BigInteger temp = rm.getlpay().getci();
                int l=rm.getlpay().geti();
                for(int i=l;i>0;i--)
                    temp=hashf.hash(temp.ToString());
                if (start == temp)
                {
                    btb.AppendText("\tRedemption verified ( for " + l.ToString() + "$ ), transaction initiated for vendor: " + v.name + " !\n");
                    v.gettransinfo(l, this);
                }
                else
                {
                    btb.AppendText("\tRedemption failed (invalid payment), sent message to vendor: " + v.name + " !\n");
                    v.gettransinfo(0, this);
                }
            }
            else
            {
                btb.AppendText("\tRedemption failed (invalid user), sent message to vendor: " + v.name + " !\n");
                v.gettransinfo(0, this);
            }

        }
        private bool verifyuser(ucommit ucom)
        {
            foreach(var it in clist)
                if (it.getuser() == ucom.getcert().getuser())
                {
                    rsakey upbkey = ucom.getcert().getukey();
                    string mess = ucom.getvendor() + "," + ucom.getcert().ToString() + "," + ucom.getc0().ToString() + "," + ucom.getD().ToString() + "," + ucom.getinfo();
                    BigInteger messh = hashf.hash(mess);

                    return rsa.verifySig(messh, ucom.getsigU(), upbkey);
                }
            return false;
        }

        public override string ToString()
        {
            return name;
        }

    }
}
