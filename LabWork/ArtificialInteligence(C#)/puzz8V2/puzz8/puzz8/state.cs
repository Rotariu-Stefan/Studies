using System;
using System.Collections.Generic;

namespace puzz8
{


    class state
    {
        private int[,] a;
        private int pozl;
        private int pozc;
        private String ltrace;

        #region constructori
        public state()
        {
            a = new int[3, 3];
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                    a[l,c]=1;
            a[0, 0] = 0;
            pozl=0;
            pozc=0;
            ltrace = "";
            
        }
        public state(state x)
        {
            a = new int[3, 3];
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                    a[l, c] = x.a[l, c];
            pozl = x.pozl;
            pozc = x.pozc;
            ltrace = x.ltrace;
        }
        public state(int[,] x)
        {
            a = new int[3, 3];
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                {
                    if (x[l, c] == 0)
                    {
                        pozl = l;
                        pozc = c;
                    }
                    a[l, c] = x[l,c];
                }
            ltrace = "";
        }
        #endregion

        #region mutari
        public bool mutasus()
        {
            if (pozl == 0)
            {
                Console.WriteLine("Mutare sus imposibila!");
                return false;
            }
            else
            {
                a[pozl, pozc] = a[pozl - 1, pozc];
                a[pozl - 1, pozc] = 0;
                pozl--;
                ltrace = ltrace+'u';
            }
            return true;
        }
        public bool mutajos()
        {
            if (pozl == 2)
            {
                Console.WriteLine("Mutare jos imposibila!");
                return false;
            }
            else
            {
                a[pozl, pozc] = a[pozl + 1, pozc];
                a[pozl + 1, pozc] = 0;
                pozl++;
                ltrace = ltrace+'j';
            }
            return true;
        }
        public bool mutast()
        {
            if (pozc == 0)
            {
                Console.WriteLine("Mutare stanga imposibila!");
                return false;
            }
            else
            {
                a[pozl, pozc] = a[pozl, pozc - 1];
                a[pozl, pozc - 1] = 0;
                pozc--;
                ltrace = ltrace+'s';
            }
            return true;
        }
        public bool mutadr()
        {
            if (pozc == 2)
            {
                Console.WriteLine("Mutare dreapta imposibila!");
                return false;
            }
            else
            {
                a[pozl, pozc] = a[pozl, pozc + 1];
                a[pozl , pozc+1] = 0;
                pozc++;
                ltrace = ltrace+'d';
            }
            return true;
        }
        #endregion

        #region rets/fills
        public void fill(int[,] x)
        {
            for(int l=0;l<3;l++)
                for(int c=0;c<3;c++)
                {
                    if (x[l, c] == 0)
                    {
                        pozl = l;
                        pozc = c;
                    }
                    a[l,c]=x[l,c];
                }
        }
        public void fillRand(Random r)
        {
            GenRand rn = new GenRand(0, 8);
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                {
                    a[l, c] = rn.NewRandomNumber(r);
                    if (a[l, c] == 0)
                    {
                        pozl = l;
                        pozc = c;
                    }
                }
        }

        public int[,] reta()
        {
            return a;
        }
        public int retl()
        {
            return pozl;
        }
        public int retc()
        {
            return pozc;
        }
        public String rettr()
        {
            return ltrace;
        }
        public void afis()
        {
            for (int l = 0; l < 3; l++)
            {
                Console.Write("|");
                for (int c = 0; c < 3; c++)
                {
                    Console.Write(a[l, c] + "|");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
            //Console.WriteLine("L:" + pozl + " C:" + pozc);
        }
        #endregion

        #region operators
        public static bool operator ==(state x1, state x2)
        {

            for (int l = 0; l < 3; l++)
                for (int c=0; c < 3; c++)
                    if (x1.a[l, c] != x2.a[l, c])
                        return false;
            if ((x1.pozc != x2.pozc) || (x1.pozl != x2.pozl))
                return false;
            return true;
        }
        public static bool operator !=(state x1, state x2)
        {
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                    if (x1.a[l, c] != x2.a[l, c])
                        return true;
            if ((x1.pozc != x2.pozc) || (x1.pozl != x2.pozl))
                return true;
            return false;
        }
        #endregion
    }
}
