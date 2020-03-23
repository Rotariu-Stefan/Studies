using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            DblQueue<int> q = new DblQueue<int>();
            q.EnqueueHead(5);
            q.EnqueueHead(11);
            q.EnqueueTail(56);
            q.EnqueueTail(2);
            q.afis();

            Console.WriteLine("Count: {0}", q.Count);

            Console.WriteLine("Head: {0}\t Tail: {1}", q.PeekHead(), q.PeekTail());
            Console.WriteLine("Head: {0}\t Tail: {1}", q.DequeueHead(), q.DequeueTail());
            q.afis();

            int[] a = q.ToArray();


            Console.Read();
        }
    }
}
