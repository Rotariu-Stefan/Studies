using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class P1
    {
        public double u;

        public P1()
        {
            retprecizie();
        }
        public void retprecizie()
        {
            double m = 0;
            while (Math.Pow(10, m) + 1 != 1)
            {
                m--;
            }
            u = Math.Pow(10, m + 1);

            Console.WriteLine("U este:" + u);

        }


    }
}
