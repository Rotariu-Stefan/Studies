using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class P2
    {
        public P2(double u)
        {
            Console.WriteLine("ASOC: " + retasoc(u));
        }

        public bool retasoc(double u)
        {
            double stg = 1 + u;
            stg += u;

            double dr = u + u;
            dr += 1;

            if (stg == dr)
                return true;
            else
                return false;
        }
    }
}
