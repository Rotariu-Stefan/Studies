using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class E5
    {
        private List<int> a = new List<int>();

        public E5()
        {
            Console.WriteLine("E5:\n________");
        }

        public void fill(int[] x)
        {
            foreach (int it in x)
                a.Add(it);
        }
        public void afis()
        {
            foreach (int i in a)
            {
                Console.Write(i + " ");
            }
            Console.WriteLine();
        }
        public void sw(int i1, int i2)
        {
            int x = a[i1];
            a[i1] = a[i2];
            a[i2] = x;
        }

        public void siftDown(int st, int end)
        {
            int r = st;

            while (r * 2 + 1 <= end)
            {
                int swap = r;
                int fs = r * 2 + 1;

                if (a[swap] < a[fs])
                    swap = fs;
                if ((fs + 1 <= end) && (a[swap] < a[fs + 1]))
                    swap = fs + 1;
                if (swap != r)
                {
                    sw(r, swap);
                    r = swap;
                }
                else
                    break;
            }
        }
        public void heapify()
        {
            int st = a.Count / 2 - 1;

            while (st >= 0)
            {
                siftDown(st, a.Count - 1);
                st--;
            }
        }
        public void heapSort()
        {
            heapify();

            int end = a.Count - 1;
            while (end > 0)
            {
                sw(end, 0);
                end--;
                siftDown(0, end);
            }
        }

    }
}
