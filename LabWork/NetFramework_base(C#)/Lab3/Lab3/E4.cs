using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace Lab3
{
    class E4: IComparable
    {
        private string s;
        private char p;

        public E4()
        {
        }
        public E4(string ss, char pp)
        {
            s = ss;
            p = pp;
        }

        public int calc()
        {
            return s.IndexOf(p);
        }
        public void afis()
        {
            Console.WriteLine("Sir s: " + s + "\t Char p :" + p);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                return 1;
            }

            E4 other = obj as E4;
            if (other != null)
            {
                return this.calc().CompareTo(other.calc());
            }
            else
                throw new ArgumentException("Object invalid !!");
        }

    }

    class myComparer : IComparer
    {
        int IComparer.Compare(Object x, Object y)
        {
            return ((IComparable)x).CompareTo(y);
        }
    }

}
