using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace PayWord.Resources
{
    class hashf
    {
        static private SHA1 sha = new SHA1CryptoServiceProvider();

        static public BigInteger hash(string s)
        {
            return new BigInteger(sha.ComputeHash(Encoding.ASCII.GetBytes(s)));
        }
        static public bool verify(BigInteger b, string s)
        {
            if (hash(s) == b)
                return true;
            else
                return false;
        }

    }
}
