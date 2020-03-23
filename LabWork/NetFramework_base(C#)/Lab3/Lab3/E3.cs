using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class E3    // class Info from homework
    {
        public E3(int n)
        {
            this.n = n;
        }

        public int n = 0;
    }

    class E3func
    {
        private List<E3> numere = new List<E3>();
        
        public E3func()
        {
            Console.WriteLine("E3:\n________");
        }

        public void insert(int nr)
        {
            numere.Add(new E3(nr));
        }
        public void afis()
        {
            foreach (E3 p in this.numere)
            {
                Console.Write(p.n + " ");
            }
            Console.WriteLine();
        }

        public void add(int x)
        {
            foreach (E3 p in this.numere)
            {
                p.n = p.n + x;
            }
        }
        public void afisOdd()
        {
            foreach (E3 p in this.numere)
            {
                if (p.n % 2 == 1)
                {
                    Console.Write(p.n + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
