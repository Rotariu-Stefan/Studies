using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jablon.Resurse
{
    public class rsa
    {
        private BigInteger p, q;
        private BigInteger n;
        private BigInteger e, d;

        public rsa()
        {
            Random rn = new Random();

            p = BigInteger.genPseudoPrime(520, 100, rn);
            q = BigInteger.genPseudoPrime(520, 100, rn);
            n = new BigInteger(p * q);

            BigInteger o = new BigInteger((p - 1) * (q - 1));

            int eb=0;
            do
            {
                //eb = rn.Next(2, o.bitCount());
                //e = BigInteger.genPseudoPrime(eb, 100, rn);
                e = new BigInteger(65537);
            }
            while (e > o || e.gcd(o) != new BigInteger(1));

            d = new BigInteger(e.modInverse(o));
        }

        #region returns

        public BigInteger gete()
        {
            return e;
        }
        public BigInteger getd()
        {
            return d;
        }
        public BigInteger getn()
        {
            return n;
        }

        #endregion

        public static BigInteger createSig(BigInteger m, BigInteger d, BigInteger n)
        {
        //    Console.WriteLine("\nD : " + d);
            return m.modPow(d, n);
        }
        public static bool verifySig(BigInteger m, BigInteger s, BigInteger e, BigInteger n)
        {
        //    Console.WriteLine("\nE: " + e);
        //    Console.WriteLine("\nN: " + n);
            BigInteger mtest = new BigInteger(s.modPow(e, n));
        //    Console.WriteLine("\nM: " + m);
        //    Console.WriteLine("\nMtest: " + mtest);
            if (m == mtest)
                return true;
            else
                return false;
        }

        public void TEST(BigInteger m)
        {
            Console.WriteLine("\nN: " + n);

            Console.WriteLine("\nE: " + e);
            Console.WriteLine("\nD: " + d);

            BigInteger s = new BigInteger(m.modPow(e, n));
            Console.WriteLine("\nS: " + s);
            BigInteger mtest = new BigInteger(s.modPow(d, n));
            Console.WriteLine("\nM: " + mtest);
            Console.WriteLine("\nm: " + m);
        }
    }
}
