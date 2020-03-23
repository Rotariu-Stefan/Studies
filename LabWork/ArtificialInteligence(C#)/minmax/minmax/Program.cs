using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace minmax
{
    class Program
    {
        static void Main(string[] args)
        {

            stare s = new stare();
            for (int i = 0; i < 9; i++)
            {
                s.setnext(i);
                s.afis();
            }










            Console.Read();
        }
    }
}
