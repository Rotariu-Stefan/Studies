using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayWord.Resources
{
    public class ucommit
    {
        string vendor;
        bcertificate cert;
        BigInteger c0;
        DateTime D;
        string info;
        BigInteger sigU;

        public ucommit(string v, bcertificate c, BigInteger cc0, DateTime d, string i, BigInteger sig)
        {
            vendor = v;
            cert = c;
            c0 = cc0;
            D = d;
            info = i;
            sigU = sig;
        }

        public string getvendor()
        {
            return vendor;
        }
        public bcertificate getcert()
        {
            return cert;
        }
        public BigInteger getc0()
        {
            return c0;
        }
        public DateTime getD()
        {
            return D;
        }
        public string getinfo()
        {
            return info;
        }
        public BigInteger getsigU()
        {
            return sigU;
        }

    }
}
