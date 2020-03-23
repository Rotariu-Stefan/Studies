using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace arbore
{
    class Program
    {
        static void Main(string[] args)
        {
            arbore<int> ar = new arbore<int>();
            int[] a = { 21, 15, 10, 127, 25, 30, 11 };
       /*     ar.addnod(21);
            ar.addnod(15);
            ar.addnod(10);
            ar.addnod(127);
            ar.addnod(25);
            ar.addnod(30);
            ar.addnod(11);*/

            ar = new arbore<int>(a);
            ar.afis();
            ar.order();
            ar.afis();

            ar.erasenod(10);
            ar.erasenod(127);
            ar.erasenod(15);
            ar.afis();
            ar.order();
            ar.afis();

            ar.addnod(1);
            ar.addnod(-4);
            ar.addnod(27);

            ar.afis();
            ar.order();
            ar.afis();

            int[] arr = ar.toArray();

            Console.WriteLine();
            for (int i = 0; i < arr.Count(); i++)
                Console.Write(arr[i] + " ");

            





            Console.Read();
        }
    }
}
