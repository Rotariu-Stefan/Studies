using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace queens8
{
    class slot
    {
        public int l;
        public int c;

        public slot()
        {
            l = -1;
            c = -1;
        }
        public slot(int li, int co)
        {
            l = li;
            c = co;
        }
        public slot(slot s)
        {
            l=s.l;
            c=s.c;
        }

        public void set(int li, int co)
        {
            l = li;
            c = co;
        }
        public void unset()
        {
            l = -1;
            c = -1;
        }
        public bool isset()
        {
            if (l < 0 || c < 0)
                return false;
            else
                return true;
        }

        public void afis()
        {
            Console.WriteLine(" L: " + l + "   C: " + c);
        }

    }
}
