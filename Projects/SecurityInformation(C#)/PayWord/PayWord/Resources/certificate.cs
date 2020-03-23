using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PayWord.Parts;

namespace PayWord.Resources
{
    public class bcertificate
    {
        broker brker;
        string user;
        string address;
        rsakey ukey;
        DateTime exp;
        string info;
        BigInteger sigB;

        public bcertificate(broker b, string u, string a, rsakey uk, DateTime dt, string i, BigInteger sB)
        {
            brker = b;
            user = u;
            address = a;
            ukey = uk;
            exp = dt;
            info = i;
            sigB = sB;
        }

        public broker getbroker()
        {
            return brker;
        }
        public string getuser()
        {
            return user;
        }
        public string getaddress()
        {
            return address;
        }
        public rsakey getukey()
        {
            return ukey;
        }
        public DateTime getexp()
        {
            return exp;
        }
        public string getinfo()
        {
            return info;
        }
        public BigInteger getsigB()
        {
            return sigB;
        }


        public override string ToString()
        {
            return info;
        }
    }
}
