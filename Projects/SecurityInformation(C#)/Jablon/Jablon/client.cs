using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Aes;
using Jablon.Resurse;

namespace Jablon
{
    class client
    {
        private SHA1 sha = new SHA1CryptoServiceProvider();

        private String name;

        private cserver[] slist;
        private bool enrolled;

        private BigInteger p, q, r;

        BigInteger nrsa;        //n=pq din rsa

        public client()
        {
            name = "default";
            enrolled = false;
        }
        public client(String nm)
        {
            name = nm;
            enrolled = false;
        }

        /*        private bool isPrime(BigInteger x)
                {
                    if (x % 2 == 0)
                        return false;
                    x = BigInteger.Abs(x);
                    BigInteger sq=new BigInteger(Math.Exp(BigInteger.Log(x)/2));
                    for (BigInteger i = 3; i < sq; i = i + 2)
                    {
                        if (x % i == 0)
                            return false;
                    }
                    return true;
                }
                private BigInteger genPrime(int nb)
                {
            
                    BigInteger pp = new BigInteger();

                    do
                    {
                        var rng = new RNGCryptoServiceProvider();
                        byte[] bytes = new byte[nb / 8];
                        rng.GetBytes(bytes);
                        pp = new BigInteger(bytes);
                    }
                    while (!isPrime(pp));

                    return pp;
                }*/

        public void genParam(cserver[] cs)
        {
            gennr(false);
            setservers(cs);
        }

        public void enrollment(String pass)
        {
            BigInteger e, d, n;
            genRSAKeys(out e, out d, out n);

            BigInteger gp = gengp(pass);

            int cnt = slist.Length;
            BigInteger[] yrand = genyi(cnt, gp);
            BigInteger[] shares = genshares(gp, yrand);
            BigInteger Km = genmaster(shares);

            BigInteger proofPKm = genproof(gp, Km);
            byte[] Uk = cryptU(d, Km);

            for (int i = 0; i < cnt; i++)
            {
                messC1 m = new messC1(name, yrand[i], e, Uk, proofPKm);
                sendenroll(slist[i], m);
            }
            enrolled = true;
            Console.WriteLine("\n{0}: Enrollemnt Succesful !", name);
        }

        public void retreive(String pass, bool legal)
        {
            BigInteger gp = gengp(pass);

            BigInteger x= genx();
            BigInteger Q= genQ(x,gp);

            List<messS1> rec = new List<messS1>();
            int n = slist.Length;
            for (int i = 0; i < n; i++)
            {
                messC2 m = new messC2(name, Q);
                messS1 reply=sendrequest(slist[i], m);
                if (reply == null)
                {
                    Console.WriteLine("\n{0}: Abort !\n", name);
                    return;
                }
                else
                    rec.Add(reply);
            }

            BigInteger Kmrec= genKrec(x,rec);

            BigInteger proof = rec[0].getproof();
            byte[] Ukrec= rec[0].getUk();
            if (verify(gp,Kmrec,proof) == true)
            {
                Console.WriteLine("\n{0}: Km retreived successfuly !\n",name);

                BigInteger U= decryptU(Kmrec,Ukrec);
                BigInteger Qu=new BigInteger(rsa.createSig(Q, U, nrsa));

                messC3 mc = new messC3(Q, Qu);
                for (int i = 0; i < slist.Length; i++)
                    if (legal == true)
                        sendconfirm(slist[i], mc, nrsa);
                    else
                        sendconfirmFake(slist[i], mc, nrsa);
            }
            else
                Console.WriteLine("\n{0}: Abort !\n",name);
        }

        #region genParam

        private void gennr(bool cb)
        {
            Random rn = new Random();
            if (cb == true)
            {
                do
                {
                    q = BigInteger.genPseudoPrime(160, 100, rn);
                    r = BigInteger.genPseudoPrime(864, 100, rn);
                    p = new BigInteger(2 * q * r + 1);
                }
                while (!p.isProbablePrime(100));
            }
            else
            {
                q = new BigInteger("1357240200991138844597040462790625645217868549141", 10);
                r = new BigInteger("113598119916368178543726680204430381937875824948480914597565387121457881140775963786806669392598432983134451798231342845963966490631191394432315401337959287970864814764728934773343090591391155652090238298766028332871998156437678877229180157399463412668170350113", 10);
                p = new BigInteger("308359870215014078485257030866450886542952487603077718832462434453183859939031089613588944732186396626095766979593995910212976752989132511989881464667656451202440745056276306885148035449349526765128968559736391792793918308470057210553727052388063491635390548699144068380621588468735471047786563470306630805867", 10);
            }
        }
        private void setservers(cserver[] cs)
        {
            slist = cs;
        }

        #endregion

        #region enrollment

        private void genRSAKeys(out BigInteger e, out BigInteger d, out BigInteger n)
        {
            rsa Rsa = new rsa();

            e = new BigInteger(Rsa.gete());
            //Console.WriteLine("\nprivate KEY(V): " + e);
            d = new BigInteger(Rsa.getd());
            //Console.WriteLine("\nPRIVATE KEY(U): " + d);
            n = new BigInteger(Rsa.getn());
            nrsa = n;

        }
        private BigInteger gengp(String pass)
        {
            BigInteger gp = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(pass)));
            //Console.WriteLine("\nHASHED PASS: " + gp);
            gp = gp.modPow(2 * r, p);
            //Console.WriteLine("\nGP: " + gp);
            return gp;
        }
        private BigInteger[] genyi(int ns, BigInteger gp)
        {
            Random rn = new Random();

            BigInteger[] yrand = new BigInteger[ns];
            BigInteger yi = new BigInteger();
            int nb;

            for (int i = 0; i < ns; i++)
            {
                do
                {
                    nb = rn.Next(1, 160);
                    yi = BigInteger.genPseudoPrime(nb, 100, rn);
                    //Console.WriteLine("\nY-{0}: {1}", i, yi);
                }
                while (yi > q);
                yrand[i] = yi;

            }
            return yrand;
        }
        private BigInteger[] genshares(BigInteger gp, BigInteger[] yrand)
        {
            int cnt = yrand.Length;
            BigInteger[] shares = new BigInteger[cnt];

            for (int i = 0; i < cnt; i++)
            {
                shares[i] = gp.modPow(yrand[i], p);
                //Console.WriteLine("\nS-{0}: {1}", i, shares[i]);
            }
            return shares;
        }

        private BigInteger genmaster(BigInteger[] shares)
        {
            StringBuilder sb = new StringBuilder();
            BigInteger Km;

            for (int i = 0; i < shares.Length; i++)
                sb.Append(shares[i]);

            Km = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString())));
            Km = Km.modPow(1, new BigInteger("1208925819614629174706176", 10));

            //Console.WriteLine("\nKm: " + Km);

            return Km;
        }
        private BigInteger genproof(BigInteger gp, BigInteger Km)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Km);
            sb.Append(gp);

            BigInteger proofPKm = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString())));
            //Console.WriteLine("\nProofPKm: " + proofPKm);

            return proofPKm;

        }
        private byte[] cryptU(BigInteger d, BigInteger Km)
        {
            String s = d.ToString();

            blmat aes = new blmat(s);
            aes.acceptKeyStr(Km.ToString());
            aes.generateRKeyList();
            aes.Crypt();

            byte[] Uk = aes.retblmatbyte();
            //Console.WriteLine("\nUk: " + Uk);

            return Uk;
        }

        #endregion

        #region retreival

        private BigInteger genx()
        {
            Random rn = new Random();

            BigInteger x;
            int xb = rn.Next(1, 160);
            do
            {
                x = BigInteger.genPseudoPrime(xb, 100, rn);
            }
            while (x > q);
            //Console.WriteLine("\nX: " + x);

            return x;
        }
        private BigInteger genQ(BigInteger x, BigInteger gp)
        {
            BigInteger Q = new BigInteger(gp.modPow(x, p));
            //Console.WriteLine("\nQ: " + Q);

            return Q;

        }
        private BigInteger genrecshare(BigInteger Ri, BigInteger x)
        {

            BigInteger inverseX = x.modInverse(q);
            BigInteger Si = new BigInteger(Ri.modPow(inverseX, p)); //to be reviewed
            //Console.WriteLine("\nSi: " + Si);

            return Si;
        }
        private BigInteger genKrec(BigInteger x, List<messS1> rec)
        {
            StringBuilder sb = new StringBuilder();
            BigInteger Krec;

            for (int i = 0; i < rec.Count; i++)      
                sb.Append(genrecshare(rec[i].getRi(),x));
            Krec = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString())));
            Krec = Krec.modPow(1, new BigInteger("1208925819614629174706176", 10));

            //Console.WriteLine("\nK' : " + Krec);

            return Krec;
        }

        private bool verify(BigInteger gp, BigInteger Krec, BigInteger proofPKm)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Krec);
            sb.Append(gp);
            BigInteger testH = new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(sb.ToString())));

            //Console.WriteLine("\ntestH: " + testH);
            //Console.WriteLine("\nproofPKm: " + proofPKm);
          
            if (testH == proofPKm)
                return true;
            else
                return false;
        }
        private BigInteger decryptU(BigInteger Krec, byte[] Ukrec)
        {
            blmat aes = new blmat(Ukrec);
            aes.acceptKeyStr(Krec.ToString());
            aes.generateRKeyList();
            aes.deCrypt();

            BigInteger U = new BigInteger(aes.retblmatstr(), 10);
            //Console.WriteLine("\nU': " + U);

            return U;
        }

        #endregion

        #region messages

        private void sendenroll(cserver cs, messC1 m)
        {
            cs.enroll(m);
        }
        private messS1 sendrequest(cserver cs, messC2 m)
        {
            cs.accept(m,p);
            return cs.reply();
        }
        private void sendconfirm(cserver cs, messC3 m,BigInteger n)
        {
            cs.sigcheck(m,n);
        }
        private void sendconfirmFake(cserver cs, messC3 m, BigInteger n)
        {
            m = new messC3(m.getQ(), m.getQu() + 1);
            cs.sigcheck(m, n);
        }        

        #endregion

        public override String ToString()
        {
            String str = "";
            str += ("\nName: " + name);
            str += ("\nNr. Servers: " + slist.Length);
            if (enrolled == false)
                str += "\nNOT Enrolled";
            else
                str += "\nEnrolled";
            //str += ("\nQ: " + q.ToString());
            //str += ("\nR: " + r.ToString());
            //str += ("\nP: " + p.ToString()+"\n");

            return str;
        }
    }
}