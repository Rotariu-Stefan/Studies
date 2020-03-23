using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading;
using PayWord.Resources;

namespace PayWord.Parts
{
    public class vendor
    {
        TextBox vtb;
        Payword formref;

        public string name;
        Thread cycleth;
        int cycle;
        bool trust;

        ucommit ucomm;
        payment lastpay;

        public vendor()
        {
            name = "Default Vendor";
            cycleth=new Thread(new ThreadStart(clearcycle));
            cycle=10;
        }
        public void setUIfront(TextBox tb)
        {
            vtb = tb;
        }
        public void setfref(Payword f)
        {
            formref = f;
        }
        public void loaded()
        {
            vtb.AppendText("Vendor initialized ! \n");
        }

        public void getcommit(ucommit ucom, user u)
        {
            vtb.AppendText("Commitment request received from " + u.name + "! \n");

            if (ucom.getD() < ucom.getcert().getexp())
                if (verifyuser(ucom))
                    if (verifycert(ucom.getcert()))
                    {
                        vtb.AppendText("\tCommitment approved, message sent ! \n");
                        trust = true;
                        ucomm = ucom;
                        lastpay = new payment(ucom.getc0(),0);
                        u.comconfirm(true, this);
                        if (!(cycleth.IsAlive == true))
                            cycleth.Start();
                    }
                    else
                    {
                        vtb.AppendText("\tCertificate Signature Invalid, message sent ! \n");
                        u.comconfirm(false, this);
                    }
                else
                {
                    vtb.AppendText("\tUser Signature Invalid, message sent ! \n");
                    u.comconfirm(false, this);
                }
            else
            {
                vtb.AppendText("\tCertificate expired, message sent ! \n");
                u.comconfirm(false, this);
            }
        }
        private bool verifyuser(ucommit ucom)
        {
            rsakey upbkey = ucom.getcert().getukey();
            string mess = ucom.getvendor() + "," + ucom.getcert().ToString() + "," + ucom.getc0().ToString() + "," + ucom.getD().ToString() + "," + ucom.getinfo();
            BigInteger messh = hashf.hash(mess);

            return rsa.verifySig(messh, ucom.getsigU(), upbkey);
        }
        private bool verifycert(bcertificate cert)
        {
            rsakey bpbkey = cert.getbroker().bpbk;
            string mess = cert.getbroker().ToString() + "," + cert.getuser() + "," + cert.getaddress() + "," + cert.getukey().ToString() + "," + cert.getexp().ToString() + "," + cert.getinfo();
            BigInteger messh = hashf.hash(mess);

            return rsa.verifySig(messh, cert.getsigB(), bpbkey);
        }
        public void getpayment(payment pay, user u)
        {
            int i = pay.geti();
            BigInteger temp = pay.getci();

            vtb.AppendText("Received payment of " + i + "$ from user: " + u.name+" !\n");
            for (int j = i; j > 0; j--)
                temp = hashf.hash(temp.ToString());
            if (temp == lastpay.getci())
            {
                i += lastpay.geti();
                lastpay = new payment(pay.getci(), i);
                vtb.AppendText("\tPayment approved, message sent !\n");
                u.payconfirm(true, this,0);
            }
            else
            {
                vtb.AppendText("\tPayment failed, message sent !\n");
                u.payconfirm(false, this,i);
            }

        }
        public void sendredemption(broker b)
        {
            if (lastpay.geti() == 0)
                vtb.AppendText("No payments made !\n");
            else
            {
                vtb.AppendText("Redepmtion sent to broker: " + b.ToString() + " !\n");
                redmess rm = new redmess(lastpay, ucomm);
                b.getredemption(rm, this);
            }
        }
        public void gettransinfo(int np, broker b)
        {
            if (np == 0)
            {
                trust = false;
                vtb.AppendText("\tTransaction failed with :" + b.ToString() + " !\n");
            }
            else
            {
                trust = true;
                vtb.AppendText("\tTransaction completed with :" + b.ToString() + " for " + np + "$ !\n");
            }
        }
        public bool gettrust()
        {
            return trust;
        }

        public void clearcycle()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(new TimeSpan(0, 0, cycle));
                formref.Invoke(formref.dredeem, null);
            }
        }
    }
}
