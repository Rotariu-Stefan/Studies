using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayWord.Resources
{
    public class payment
    {
        BigInteger ci;
        int i;

        public payment(BigInteger pci, int pi)
        {
            ci = pci;
            i = pi;
        }

        public BigInteger getci()
        {
            return ci;
        }
        public int geti()
        {
            return i;
        }
    }
}
