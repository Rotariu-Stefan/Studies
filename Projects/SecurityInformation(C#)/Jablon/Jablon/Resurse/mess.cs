using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jablon.Resurse
{
    public class messC1
    {
        private String A;
        private BigInteger yi;
        private BigInteger V;
        private byte[] Uk;
        private BigInteger proofPKm;

        public messC1(String a, BigInteger y, BigInteger v, byte[] uk, BigInteger proof)
        {
            A = a;
            yi = y;
            V = v;
            Uk = uk;
            proofPKm = proof;
        }

        public String getA()
        {
            return A;
        }
        public BigInteger getyi()
        {
            return yi;
        }
        public BigInteger getV()
        {
            return V;
        }
        public byte[] getUk()
        {
            return Uk;
        }
        public BigInteger getproof()
        {
            return proofPKm;
        }

    }

    public class messC2
    {
        private String A;
        private BigInteger Q;

        public messC2(String a, BigInteger q)
        {
            A = a;
            Q = q;
        }

        public String getA()
        {
            return A;
        }
        public BigInteger getQ()
        {
            return Q;
        }

    }

    public class messL
    {
        private String A;
        private BigInteger Q;
        private BigInteger V;
        private DateTime t;

        public messL(String a, BigInteger q, BigInteger v, DateTime tt)
        {
            A = a;
            Q = q;
            V = v;
            t = tt;
        }

        public String getA()
        {
            return A;
        }
        public BigInteger getQ()
        {
            return Q;
        }
        public BigInteger getV()
        {
            return V;
        }
        public DateTime gett()
        {
            return t;
        }


    }

    public class messS1
    {
        private BigInteger Ri;
        private byte[] Uk;
        private BigInteger proofPKm;

        public messS1(BigInteger ri, byte[] u, BigInteger proof)
        {
            Ri = ri;
            Uk = u;
            proofPKm = proof;
        }

        public BigInteger getRi()
        {
            return Ri;
        }
        public byte[] getUk()
        {
            return Uk;
        }
        public BigInteger getproof()
        {
            return proofPKm;
        }

    }

    public class messC3
    {
        private BigInteger Q;
        private BigInteger Qu;

        public messC3(BigInteger q, BigInteger qu)
        {
            Q = q;
            Qu = qu;
        }

        public BigInteger getQ()
        {
            return Q;
        }
        public BigInteger getQu()
        {
            return Qu;
        }
    }


}
