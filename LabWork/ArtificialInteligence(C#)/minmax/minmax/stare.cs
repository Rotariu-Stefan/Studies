using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace minmax
{
    class stare
    {
        public int p;
        public List<int> f;

        int[,] tab;
        int lset;

        public stare()
        {
            tab = new int[3, 3];
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    tab[i, j] = -1;
            lset = 0;
        }

        public int runda()
        {
            int x = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (tab[i, j] != -1)
                        x++;
            return x;
        }
        public bool set0(int l, int c)
        {
            if (tab[l, c] != -1)
                return false;
            else
            {
                tab[l, c] = 0;
                return true;
            }
        }
        public bool setX(int l, int c)
        {
            if (tab[l, c] != -1)
                return false;
            else
            {
                tab[l, c] = 1;
                return true;
            }
        }
        public bool setnext0(int r)
        {
            if (isfinal() == true)
                return false;
            set0(r / 3, r % 3);
            return true;
        }
        public bool setnextX(int r)
        {
            if (isfinal() == true)
                return false;
            setX(r / 3, r % 3);
            return true;
        }
        public bool setnext(int r)
        {
            if (lset == 0)
            {
                lset = 1;
                return setnextX(r);
            }
            else
            {
                lset = 0;
                return setnext0(r);
            }
        }
        public bool isfinal()
        {
            for (int i = 0; i < 3; i++)
                if (tab[i, 0] == tab[i, 1] && tab[i, 0] == tab[i, 2] && tab[i, 0] != -1)
                    return true;
            for (int i = 0; i < 3; i++)
                if (tab[0, i] == tab[1, i] && tab[0, i] == tab[2, i] && tab[0, i] != -1)
                    return true;
            if (tab[0, 0] == tab[2, 2] && tab[0, 0] == tab[1, 1] && tab[0, 0] != -1)
                return true;
            if (tab[0, 2] == tab[1, 1] && tab[0, 2] == tab[2, 0] && tab[0, 2] != -1)
                return true;
            return false;
        }
        public void afis()
        {
            Console.WriteLine();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    Console.Write("|" + tab[i, j]);
                Console.WriteLine("|");
            }
            if (isfinal() == true)
                Console.WriteLine("Final!");
        }
    }
}