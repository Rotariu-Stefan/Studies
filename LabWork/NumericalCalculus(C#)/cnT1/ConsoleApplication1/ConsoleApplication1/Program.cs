using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            P1 p1 = new P1();
            P2 p2 = new P2(p1.u);

            double[,] m1 = { { 1, 4, 2, 3 }, { 2, 4, 0, 1 }, { 2, 5, 8, 2 }, { 2, 1, 1, 11 } };
            double[,] m2 = { { 1, 0, 0, 0 }, { 0, 1, 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            matp M1 = new matp(m1);
            M1.afis();
            matp M2 = new matp(m2);
            M2.afis();


            opr.mul(M1, M2).afis();
            P3.mulStrass(M1, M2, 2).afis();



            Console.WriteLine("\n\nDONE !");
            Console.Read();
        }
    }
}
