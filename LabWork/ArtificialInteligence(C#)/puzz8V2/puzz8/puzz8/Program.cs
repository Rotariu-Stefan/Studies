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
                {0,2,3},
                {7,1,6},
                {5,4,8}
            };
            int[,] fe =
            {
                {7,2,0},
                {5,6,3},
                {4,1,8}
            };
            int[,] f =
            {
                {7,6,4},
                {0,3,2},
                {1,5,8}
            };
            int[,] f2 =
            {
                {1,2,3},
                {4,0,5},
                {6,7,8}
            };
            state xs = new state(s);
            state xf = new state(fe);
            
            node n = new node();
            node.initArray();
            n.setFinal(xf);
            n.setStart(xs);
            //n.comparare(r);

            n.executeHC();
            //n.executeA();
            //n.executeBF4();

            Console.Read();
        }
    }
}
