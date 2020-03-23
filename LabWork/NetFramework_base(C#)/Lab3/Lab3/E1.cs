using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class E1
    {
        public E1()
        {
            Console.WriteLine("E1:\n________");
        }

        public void execute()
        {
            var di = new Dictionary<int, string>();

            di.Add(4, "patru");
            di.Add(1, "unu");
            di.Add(3, "trei");
            di.Add(2, "doi");

            var q = di.OrderBy(k => k.Value);
            foreach (var item in q)
            {
                Console.WriteLine(item.Key + " , " + item.Value);
            }
        }
    }
}
