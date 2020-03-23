using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayWord.Resources
{
    public class rsa
    {
        private BigInteger p, q;
        private BigInteger n;
        private BigInteger e, d;

        public rsa()
        {
            Random rn = new Random();

            p = BigInteger.genPseudoPrime(521, 100, rn);
            q = BigInteger.genPseudoPrime(521, 100, rn);
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
        public rsakey getPublic()
        {
            return new rsakey(e, n);
        }
        public rsakey getPrivate()
        {
            return new rsakey(d, n);
        }

        #endregion

        public static BigInteger createSig(BigInteger m, BigInteger d, BigInteger n)
        {
            return m.modPow(d, n);
        }
        public static BigInteger createSig(BigInteger m, rsakey rk)
        {
            BigInteger d = rk.getk();
            BigInteger n = rk.getn();
            return m.modPow(d, n);
        }
        public static bool verifySig(BigInteger m, BigInteger s, BigInteger e, BigInteger n)
        {
            BigInteger mtest = new BigInteger(s.modPow(e, n));

            if (m == mtest)
                return true;
            else
                return false;
        }
        public static bool verifySig(BigInteger m, BigInteger s, rsakey rk)
        {
            BigInteger e = rk.getk();
            BigInteger n = rk.getn();
            BigInteger mtest = new BigInteger(s.modPow(e, n));

            if (m == mtest)
                return true;
            else
                return false;
        }

    }

    public class rsakey
    {
        BigInteger k;
        BigInteger n;

        public rsakey(BigInteger pk, BigInteger pn)
        {
            k = pk;
            n = pn;
        }

        public BigInteger getk()
        {
            return k;
        }
        public BigInteger getn()
        {
            return n;
        }

        public override string ToString()
        {
            return k.ToString() + "-" + n.ToString();
        }

    }
}
