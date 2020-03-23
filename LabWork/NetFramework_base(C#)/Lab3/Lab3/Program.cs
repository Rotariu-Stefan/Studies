using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
     /*       E1 e1 = new E1();
            e1.execute();
            Console.WriteLine();

            E3func e3 = new E3func();
            e3.insert(11);
            e3.insert(66);
            e3.insert(13);
            e3.insert(127);
            e3.insert(24);
            e3.afis();
            e3.add(10);
            e3.afis();
            e3.afisOdd();
            Console.WriteLine();

            E2 e2 = new E2();
            e2.insert("mar");
            e2.insert("varza");
            e2.insert("polonic");
            e2.Serialize();
            e2.Deserialize();
            e2.insert("ciocan");
            e2.afis();      */

            ArrayList data = new ArrayList();
            data.Add(new E4("mar",'a'));
            data.Add(new E4("varza", 'z'));
            data.Add(new E4("polonic", 'l'));
            data.Add(new E4("ciocan", 'c'));
            data.Add(new E4("buldozer", 'd'));

            foreach (E4 item in data)
                item.afis();

            myComparer mc = new myComparer();
            data.Sort(mc);

            Console.WriteLine();
            foreach (E4 item in data)
                item.afis();

            E5 e5 = new E5();
            int[] x = { 22, 123, 6, 7, 1, 11, 5, 33, 0, 91 };
            e5.fill(x);
            e5.afis();
            e5.heapSort();
            e5.afis();



            Console.Read();
        }
    }
}
