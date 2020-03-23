using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace queens8
{
    class state
    {
        public slot[] s;
        public int[,] dom;

        public state()
        {
            s = new slot[8];
            for (int i = 0; i < 8; i++)
                s[i] = new slot();

            dom = new int[8,8];
            for (int i = 0; i < 8; i++)
                for (int i2 = 0; i2 < 8; i2++)
                    dom[i, i2] = 1;
        }
        public state(slot[] ss)
        {
            s = new slot[8];
            for (int i = 0; i < 8; i++)
                s[i] = new slot();
            for (int i = 0; i < ss.Length; i++)
                s[i] = ss[i];
        }

        public void set(slot[] ss)
        {
            s=ss;
        }
        public bool isset()
        {
            for (int i = 0; i < 8; i++)
                if (s[i].isset() == false)
                    return false;
            return true;
        }
        public bool isdiag(slot a, slot b)
        {
            if(Math.Abs(a.c-b.c)==Math.Abs(a.l-b.l))
                return true;
            if((a.c+a.l)==(b.c+b.l))
                return true;
            return false;
        }
        public bool isvalid()
        {
            for(int i=0;i <8;i++)
                for(int j=i+1;j<8;j++)
                {
                    if(s[i].isset() && s[j].isset())
                    {
                        if(s[i].l==s[j].l)
                            return false;
                        if(s[i].c==s[j].c)
                            return false;
                        if(isdiag(s[i],s[j]))
                            return false;
                    }
                }
            return true;
        }

        public bool insert(int peL, int delaC)
        {
            if (s[peL].isset())
                return false;
            for (int c = delaC; c < 8; c++)
            {
                s[peL].set(peL, c);
                if (isvalid())
                    return true;
            }
            s[peL].unset();
            return false;
        }
        public bool clear(int peL)
        {
            if(s[peL].isset())
            {
                s[peL].unset();
                return true;
            }
            else
                return false;
        }

        public bool insertFC(int peL, int delaC)
        {
            int dC = 1;

            if (s[peL].isset())
                return false;
            for (int c = delaC; c < 8; c++)
            {
                if (dom[peL, c] == 1)
                {
                    s[peL].set(peL, c);
                    for (int d = peL + 1; d < 8; d++)
                    {
                        dom[d, c]--;
                        if (c - dC >= 0)
                            dom[d, c - dC]--;
                        if (c + dC <= 7)
                            dom[d, c + dC]--;
                        dC++;
                    }
                    return true;
                }
            }
            return false;
        }
        public bool clearFC(int peL)
        {
            if (s[peL].isset())
            {
                int dC = 1;
                for (int d = peL + 1; d < 8; d++)
                {
                    dom[d, s[peL].c]++;
                    if (s[peL].c - dC >= 0)
                        dom[d, s[peL].c - dC]++;
                    if (s[peL].c + dC <= 7)
                        dom[d, s[peL].c + dC]++;
                    dC++;
                }

                s[peL].unset();
                return true;
            }
            else
                return false;
        }

        public void insertBK()
        {
            DateTime dt = DateTime.Now;
            int mts = 0;
            bool x;
            int lc = 0, cc = 0;
            int count = 0;

            while (true)
            {
                //Console.Clear();
                //afis();
                //Thread.Sleep(1000);
                x = insert(lc, cc);

                if (isset() && isvalid())
                {
                    count = count + 1;
                    //Console.Clear();

                    //Console.WriteLine("SOLUTIA : " + count + "\n----------------");
                    //afis();
                    TimeSpan ts = DateTime.Now - dt;
                    //Console.WriteLine("Timp : " + ts.Milliseconds);
                    mts += ts.Milliseconds;
                    dt = DateTime.Now;
                    //Console.Read();
                    //Thread.Sleep(10);
                    clear(7);
                    x = false;
                }

                if (x == false)
                {
                    if (lc == 0)
                    {
                        Console.WriteLine("Timp : " + mts);
                        Console.WriteLine("S-au gasit toate solutiile ({0})!!",count);
                        Console.Read();
                        return;
                    }
                    lc--;
                    cc = s[lc].c + 1;
                    clear(lc);
                }
                else
                {
                    lc++;
                    cc = 0;
                }
            }
        }
        public void insertFC()
        {
            DateTime dt = DateTime.Now;
            int mts = 0;
            
            bool x;
            int lc = 0, cc = 0;
            int count = 0;

            while (true)
            {
                //Console.Clear();
                //afis();
                //Thread.Sleep(1000);
                x = insertFC(lc, cc);

                if (isset() && isvalid())
                {
                    count = count + 1;
                    //Console.Clear();

                    //Console.WriteLine("SOLUTIA : " + count + "\n----------------");
                    //afis();
                    TimeSpan ts = DateTime.Now - dt;
                    mts += ts.Milliseconds;
                    //Console.WriteLine("Timp : " + ts.Milliseconds);
                    dt = DateTime.Now;
                    //Console.Read();
                    //Thread.Sleep(10);
                    clear(7);
                    x = false;
                }

                if (x == false)
                {
                    if (lc == 0)
                    {
                        Console.WriteLine("S-au gasit toate solutiile ({0})!!",count);
                        Console.WriteLine("Timp : " + mts);
                        Console.Read();
                        return;
                    }
                    lc--;
                    cc = s[lc].c + 1;
                    clearFC(lc);
                }
                else
                {
                    lc++;
                    cc = 0;
                }
            }
        }

        public void afis()
        {
            string str = "\n";
            int x = 0;
            bool ex;
            while (x < 8)
            {
                ex = false;
                for (int i = 0; i < 8; i++)
                    if (s[i].l == x)
                    {
                        for (int j = 0; j < s[i].c; j++)
                            str += "* ";
                        str += "Q ";
                        for (int j = s[i].c + 1; j < 8; j++)
                            str += "* ";
                        str += "\n";
                        ex = true;
                    }
                if (ex == false)
                {
                    for (int j = 0; j < 8; j++)
                        str += "* ";
                    str += "\n";
                }
                x++;
            }

            str += "\n";
            Console.Write(str);
            Console.WriteLine("Setat: " + isset());
            Console.WriteLine("Valid: " + isvalid());

        }

    }
}




