using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Aes;
using Jablon.Resurse;

namespace Jablon
{
    class cserver
    {        
        private Dictionary<String, messC1> clist;   //Ci
        private List<messL> alist;     //Li
        private Dictionary<String, int> balist;

        private messS1 rep;

        public cserver()
        {
            clist = new Dictionary<string, messC1>();
            alist = new List<messL>();
            balist = new Dictionary<string, int>();
        }

        #region messages

        public void enroll(messC1 m)
        {
            clist.Add(m.getA(), m);
            balist.Add(m.getA(),0);
        }
        public void accept(messC2 m, BigInteger p)
        {
            String c = m.getA();
            if (balist[c] > 2)
            {
                Console.WriteLine("\nCLIENT {0} BLOCKED !", c);
                rep = null;
            }
            else
            {
                messC1 c1 = clist[c];
                messL l = new messL(c, m.getQ(), c1.getV(), DateTime.Now);
                alist.Add(l);

                BigInteger R = m.getQ().modPow(c1.getyi(), p);
                //Console.WriteLine("\nRi: " + R);
                rep = new messS1(R, c1.getUk(), c1.getproof());
            }
        }
        public messS1 reply()
        {
            return rep;
        }
        public void sigcheck(messC3 m, BigInteger n)
        {
            List<int> pozver = new List<int>();
            for(int i=0;i<alist.Count();i++)
                if (alist[i].getQ() == m.getQ())
                {
                    if (rsa.verifySig(m.getQ(), m.getQu(), alist[i].getV(), n) == true)
                    {
                        pozver.Add(i);
                        Console.WriteLine("VALID SIGNATURE FOR {0} !", alist[i].getA());
                    }
                    else
                    {
                        Console.WriteLine("BAD ACCESS ATTEMPT FOR {0} !", alist[i].getA());
                        balist[alist[i].getA()]++;
                        int x = balist[alist[i].getA()];
                        if (x > 2)
                            Console.WriteLine("TOO MANY BAD ATTEMPTS FOR {0} ! CLIENT BLOCKED !", alist[i].getA());
                    }

                }
            for (int i = 0; i < pozver.Count; i++)
                alist.RemoveAt(pozver[i]);
        }

        #endregion

        public override String ToString()
        {
            String str = "";

            str += "\nClient List(C): ";
            foreach (var it in clist)
                str += (it.Key + "\t");
            str += "\n";
            str += "Access List(L): ";
            str += ("\t" + alist[0].getA() + " at " + alist[0].gett() + "\n");
            for(int i=1;i<alist.Count;i++)
                str += ("\t\t\t"+alist[i].getA() +" at "+ alist[i].gett()+"\n");
            str += "\n";

            return str;
        }
    }
}
