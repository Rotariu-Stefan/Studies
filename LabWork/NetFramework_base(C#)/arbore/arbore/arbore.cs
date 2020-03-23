using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace arbore
{

    [Serializable]
    public class arbore<T>
    {

        nod<T>[] arb = new nod<T>[100];
        int nnod = 0;

        #region constructors
        public arbore()
        { }
        public arbore(T[] ar)
        {
            for (int i = 0; i < ar.Count(); i++)
                addnod(ar[i]);
        }
        #endregion

        #region add/erase
        public void addnod(nod<T> n)
        {
            Type X = n.GetType();
            arb[nnod] = new nod<T>(n);
            nnod++;
        }
        public void addnod(T x)
        {
            arb[nnod] = new nod<T>(x);
            if (nnod != 0)
                if (nnod % 2 == 0)
                {
                    arb[nnod].par = arb[(nnod - 2) / 2];
                    arb[(nnod - 2) / 2].fd = arb[nnod];
                }
                else
                {
                    arb[nnod].par = arb[(nnod - 1) / 2];
                    arb[(nnod - 1) / 2].fs = arb[nnod];
                }
            nnod++;
        }
        public void erasenod(T x)
        {
            int i;
            bool found = false;
            for (i = 0; i < nnod; i++)
                if (Convert.ToInt32(arb[i].val) == Convert.ToInt32(x))
                {
                    found = true;
                    break;
                }
            if (found == true)
            {
                swap(arb[i], arb[nnod - 1]);
                if (nnod % 2 == 0)
                    arb[nnod - 1].par.fs = null;
                else
                    arb[nnod - 1].par.fd = null;
                arb[nnod - 1] = null;
                nnod--;

            }
            else
                Console.WriteLine("Item {0} not found !!", x);
        }
        #endregion

        #region control
        private String spatiu(double len)
        {
            String s = "";
            for (int i = 0; i < len; i++)
                s += ' ';
            return s;
        }
        private int nrCifre(int nr)
        {
            int n = 0;
            if (nr == 0)
                return 1;
            while (nr != 0)
            {
                nr = nr / 10;
                n++;
            }
            return n;
        }
        private void swap(nod<T> x, nod<T> y)
        {
            T a = x.val;
            x.val = y.val;
            y.val = a;
        }
        #endregion

        #region traverses
        public void preoAfis(nod<T> r)       //RSD
        {
            if (r != null)
            {
                Console.Write(r.val + "  ");
                preoAfis(r.fs);
                preoAfis(r.fd);
            }
        }
        public void inoAfis(nod<T> r)        //SRD
        {
            if (r != null)
            {
                inoAfis(r.fs);
                Console.Write(r.val + "  ");
                inoAfis(r.fd);
            }
        }
        public void postoAfis(nod<T> r)      //SDR
        {
            if (r != null)
            {
                postoAfis(r.fs);
                postoAfis(r.fd);
                Console.Write(r.val + "  ");
            }
        }
        #endregion

        #region order/array
        public void order()
        {
            bool x = false;

            while (x == false)
            {
                x = true;
                int i = 0;

                while (arb[i] != null)
                {
                    if (arb[i].fs != null && Convert.ToInt32(arb[i].val) < Convert.ToInt32(arb[2 * i + 1].val))
                    {
                        x = false;
                        swap(arb[i], arb[2 * i + 1]);
                    }
                    if (arb[i].fd != null && Convert.ToInt32(arb[i].val) > Convert.ToInt32(arb[2 * i + 2].val))
                    {
                        x = false;
                        swap(arb[i], arb[2 * i + 2]);
                    }
                    i++;

                }
            }
        }
        public T[] toArray()
        {
            T[] x = new T[nnod];
            for (int i = 0; i < nnod; i++)
                x[i] = arb[i].val;
            return x;
        }
        #endregion

        public void afis()
        {

            int y = 40;

            Console.Write(spatiu(y) + arb[0].val);
            int x = 2, j = 1;
            while ((2 ^ (j)) <= nnod)
            {
                Console.Write("\n" + spatiu(y / 2));

                for (int i = 0; i < x; i++)
                {
                    if (arb[j] != null)
                        Console.Write(arb[j].val + spatiu(y - nrCifre(arb[i].val.ToString().Count())));
                    j++;
                }
                x *= 2;
                y = y / 2;
            }
            Console.WriteLine();

        }
        public void raport()
        {
            for (int i = 0; i < nnod; i++)
            {
                Console.Write("nod<T>ul" + i + ":\t");
                arb[i].afis();
            }
        }

    }
}
