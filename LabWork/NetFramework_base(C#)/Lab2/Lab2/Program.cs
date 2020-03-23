using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            strfnc x=new strfnc();

            String[] s1 = { "asd", "gfgfd", "Dfrderg" };
            String s1f = x.f1(s1,',');
            Console.WriteLine(s1f);

            String[] s2f=x.f2(s1f,',');
            foreach (String cuv in s2f)
                Console.WriteLine(cuv + " ");

            int na;
            char c = 'd';
            int[] s3f = x.f3(s1f, c,out na, true);
            Console.Write("Aparitii:");
            for(int i=0;i<na;i++)
                Console.Write(" "+s3f[i]);
            Console.WriteLine(" Nr aparitii:"+na);

            x.f5();

            strfnc.TipCaracter[] t = { strfnc.TipCaracter.Litera, strfnc.TipCaracter.Numar, strfnc.TipCaracter.Punctuatie };
            Console.WriteLine("ENUM TEST: "+x.f4("sda9as3,da4TT1sd", t)+" !!!!");

            
            Console.Read();

        }
    }
}
