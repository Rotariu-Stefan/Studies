using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace queens8
{
    class Program
    {
        static void Main(string[] args)
        {
            slot[] test = { new slot(0, 4), new slot(1, 0), new slot(2, 7), new slot(3, 5), new slot(4, 2), new slot(5, 6), new slot(6, 1),new slot(7,3)};
            state s1 = new state();
            //s1.afis();
            s1.insertBK();


            Console.Read();
        }
    }
}
