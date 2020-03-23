using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Aes;
using Jablon.Resurse;

namespace Jablon
{
    class Program
    {
        static void Main(string[] args)
        {
            cserver[] sl = new cserver[3];
            for (int i = 0; i < sl.Length; i++)
                sl[i] = new cserver();
            
            client c1 = new client("StravoS");  c1.genParam(sl);
            client c2 = new client("Ana");      c2.genParam(sl);
            client c3 = new client("Gigel");    c3.genParam(sl);

            c1.enrollment("sv11");
            c2.enrollment("asd");
            c3.enrollment("a22");

            c1.retreive("sv11",true);
            c2.retreive("asd",true);
            c2.retreive("a645",false);
            c3.retreive("a22",false);
            c3.retreive("a22",false);
            c3.retreive("a22",false);
            c3.retreive("a22", true);

            for (int i = 0; i < sl.Length; i++)
                Console.WriteLine(sl[i]);
            
            Console.WriteLine("\nSession completed !");
            Console.Read();
        }
    }
}