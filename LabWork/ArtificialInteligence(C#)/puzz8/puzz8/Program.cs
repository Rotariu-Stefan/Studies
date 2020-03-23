using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace puzz8
{
    class Program
    {
        static void Main(string[] args)
        {
            Random r = new Random();
            int[,] s =
            {
                {7,4,2},
                {1,6,0},
                {5,3,8}
            };
            int[,] f =
            {
                {7,6,4},
                {0,3,2},
                {1,5,8}
            };
            int[,] f2 =
            {
                {8,6,4},
                {7,0,2},
                {1,5,3}
            };
            state xs = new state(s);
            state xf = new state(f2);
            
            node n = new node();
            node.initArray();
            n.setFinal(xf);
            n.setStart(xs);

            n.comparare(r);


            Console.Read();
        }
    }
}
