using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            user u1 = new user("stravos", "stravos17@yahoo.com", "password!");
            Validator v = new Validator(u1);
            v.IsValid();

            info x = new info("bla");
            new infotest().ex3(x, "LALALALALAL!!!!");

            userm i1 = new userm("i1");
            userm i2 = new userm("i2");
            userm i3 = new userm("i3");
            userm i4 = new userm("i4");

            magazin mz = new magazin();
            mz.addCarte("c1");
            mz.addCarte("c2");

            mz.attach(i1, 1);
            mz.attach(i2, 2);
            i1.afis();
            i2.afis();

            mz.addElectro("e1");
            i1.afis();
            i2.afis();

            mz.remCarte("c1");
            i1.afis();
            i2.afis();

            Console.Read();
        }
    }
}
