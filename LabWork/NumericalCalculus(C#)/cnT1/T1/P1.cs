using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T1
{
    class P1
    {
        public double m;
        public double u;

        public P1()
        {}

        public void calcprecizie()
        {
            m = 0;
            while (Math.Pow(10, m) + 1 != 1)
            {
                m--;
            }
            u = Math.Pow(10, m + 1);
        }


    }
}
