using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace puzz8
{
    class node
    {
        public static System.Collections.ArrayList vis;
        public static void initArray()
        {
            vis= new System.Collections.ArrayList();
            cnt = 0;
        }
        public static int cnt;
        public static TimeSpan ts;

        private static state fin;
        private static state start;

        public node msus;
        public node mjos;
        public node mst;
        public node mdr;

        public state val;
        public String trace;
 
        #region constructors
        public node()
        {
            val = new state();
        }
        public node(state x)
        {
            val = new state(x);
        }
        public node(int[,] x)
        {
            val = new state(x);
        }
        #endregion

        #region control functions
        public bool isVisited(state x)
        {
            state[] xtest = (state[])vis.ToArray(typeof(state));
            for (int c = 0; c < xtest.Length; c++)
                if (xtest[c] == x)
                    return true;
            return false;
              
        }
        public void setFinal(state x)
        {
            fin = new state(x);
        }
        public void setStart(state x)
        {
            val = new state(x);
            start = new state(x);
            vis.Add(x);
            trace = "";
        }
        public void setTrace(String s)
        {
            trace = s;
        }
        public void afisTrace(String s)
        {
            Console.WriteLine(s+"\n");
            state temp = new state(start);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == 'u')
                {
                    temp.mutasus();
                    temp.afis();
                    Console.WriteLine("------------");
                }
                if (s[i] == 'j')
                {
                    temp.mutajos();
                    temp.afis();
                    Console.WriteLine("------------");
                }
                if (s[i] == 's')
                {
                    temp.mutast();
                    temp.afis();
                    Console.WriteLine("------------");
                }
                if (s[i] == 'd')
                {
                    temp.mutadr();
                    temp.afis();
                    Console.WriteLine("------------");
                }
            }

        }
        #endregion

        #region euristics
        public int calcDist(state x)
        {
            if (x == new state())
                return 1337;
            int d = 0;
            for (int lc = 0; lc < 3; lc++)
                for (int cc = 0; cc < 3; cc++)
                    for (int lf = 0; lf < 3; lf++)
                        for (int fc = 0; fc < 3; fc++)
                            if (x.reta()[lc, cc] == fin.reta()[lf, fc])
                                d =d+ Math.Abs(lc-lf)+Math.Abs(cc-fc);
            return d;
        }
        public int calcDiff(state x)
        {
            if (x == new state())
                return 1337;
            int d = 0;
            for (int l = 0; l < 3; l++)
                for (int c = 0; c < 3; c++)
                    if (x.reta()[l, c] != fin.reta()[l, c])
                        d++;
            return d;
        }
   /*     public state bestFind(state[] x)
        {
            state min = x[0];
            for (int i = 1; i < x.Length; i++)
                    if (calcDist(x[i]) < calcDist(min))
                        min = new state(x[i]);
            return min;
        }*/

        #endregion

        public void executeBKT()        //DFS ?
        {
            cnt++;
            Console.WriteLine("APELARI:"+cnt);

            if(cnt>4600)
            {
                Console.WriteLine("NU EXISTA SOLUTIE!");
                Thread.Sleep(20000);
                Environment.Exit(0);
            }

            if (val == fin)
            {
                Console.WriteLine("OMG SUCCESS!:");
                Thread.Sleep(2000);
                Console.Clear();
                afisTrace(trace);
                Thread.Sleep(20000);
                Environment.Exit(1);
            }

            state temp = new state(val);

     //       Thread.Sleep(500);
               if (temp.mutasus())
               {
                   if (isVisited(temp) == false)
                   {
                       temp.afis();
                       vis.Add(temp);
                       msus = new node(temp);
                       msus.setTrace(trace + 'u');
                   }
                   else
                       Console.WriteLine("Stare deja vizitata!");
               }
     //        Thread.Sleep(500);
               temp = new state(val);
               if (temp.mutajos())
               {
                   if (isVisited(temp) == false)
                   {
                       temp.afis();
                       vis.Add(temp);
                       mjos = new node(temp);
                       mjos.setTrace(trace + 'j');
                   }
                   else
                       Console.WriteLine("Stare deja vizitata!");
               }
       //      Thread.Sleep(500);
               temp = new state(val);
               if (temp.mutast())
               {
                   if (isVisited(temp) == false)
                   {
                       temp.afis();
                       vis.Add(temp);
                       mst = new node(temp);
                       mst.setTrace(trace + 's');
                   }
                   else
                       Console.WriteLine("Stare deja vizitata!");
               }

        //     Thread.Sleep(500);
               temp = new state(val);
               if (temp.mutadr())
               {
                   if (isVisited(temp) == false)
                   {
                       temp.afis();
                       vis.Add(temp);
                       mdr = new node(temp);
                       mdr.setTrace(trace + 'd');
                   }
                   else
                       Console.WriteLine("Stare deja vizitata!");
               }
     //       Thread.Sleep(500);
     /*       state[] xtest = (state [])vis.ToArray(typeof(state));
            Console.WriteLine("---------------------------");
            for (int c = 0; c < xtest.Length; c++)
                xtest[c].afis();
            Console.WriteLine("---------------------------");   */

            if (msus != null)
                msus.executeBKT();
            if (mjos != null)
                mjos.executeBKT();
            if(mst!=null)
                mst.executeBKT();
            if(mdr!=null)
                mdr.executeBKT();

        }

        public bool executeBF()
        {
            DateTime dt=DateTime.Now;
            state temp;

            while (true)
            {
                cnt++;
                Console.WriteLine("APELARI:" + cnt);
                if (val == fin)
                {
                    ts=DateTime.Now-dt;
                    Console.WriteLine("OMG SUCCESS!:");
                //    Thread.Sleep(2000);
                    Console.Clear();
                    afisTrace(trace);
                 //   Thread.Sleep(20000);
                    return true;
                }
                state min=new state();

                 //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                 //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDist(temp) < calcDist(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDist(temp) < calcDist(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDist(temp) < calcDist(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                val = new state(min);
                setTrace(trace + val.rettr());

                if (val == new state())
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("NU EXISTA SOLUTIE!");
                //    Thread.Sleep(20000);
                    return false;
                }
                
            }


        }

        public bool executeBF2()
        {
            DateTime dt=DateTime.Now;
            state temp;

            while (true)
            {
                cnt++;
                Console.WriteLine("APELARI:" + cnt);
                if (val == fin)
                {
                    ts=DateTime.Now-dt;
                    Console.WriteLine("OMG SUCCESS!:");
                  //  Thread.Sleep(2000);
                    Console.Clear();
                    afisTrace(trace);
                  //  Thread.Sleep(20000);
                    return true;
                }
                state min = new state();

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDiff(temp) < calcDiff(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDiff(temp) < calcDiff(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                    {
                        temp.afis();
                        vis.Add(temp);
                        if (calcDiff(temp) < calcDiff(min))
                            min = new state(temp);
                    }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                val = new state(min);
                setTrace(trace + val.rettr());

                if (val == new state())
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("NU EXISTA SOLUTIE!");
                  //  Thread.Sleep(20000);
                    return false;
                }

            }


        }

        public void comparare(Random r)
        {
            int n1Suc = 0, n1Fal = 0;
            int n2Suc = 0, n2Fal = 0;

            int m1Ap = 0;
            int m2Ap = 0;

            int m1Ts = 0;
            int m2Ts = 0;

            for (int i = 0; i < 100; i++)
            {
                start = new state();
                start.fillRand(r);
                val = new state(start);

                if (executeBF() == true)
                {
                    m1Ap += cnt;
                    m1Ts += ts.Milliseconds;
                    n1Suc++;
                    initArray();
                }
                else
                {
                    n1Fal++;
                    initArray();
                }
            }

            m1Ap = m1Ap / 100;
            m1Ts = m1Ts / 100;

            for (int i = 0; i < 100; i++)
            {
                start = new state();
                start.fillRand(r);
                val = new state(start);

                if (executeBF2() == true)
                {
                    m2Ap += cnt;
                    m2Ts += ts.Milliseconds;
                    n2Suc++;
                    initArray();
                }
                else
                {
                    n2Fal++;
                    initArray();
                }
            }


            m2Ap = m2Ap / 100;
            m2Ts = m2Ts / 100;

            Console.WriteLine("\nRezultate BF(prin Distanta):\n Nr cazuri cu success: " + n1Suc + "\n Nr cazuri fara success: " + n1Fal + "\n Nr mediu de iteratii: " + m1Ap + "\n Timp mediu: " + m1Ts);
            Console.WriteLine("\nRezultate BF2(prin Diferenta):\n Nr cazuri cu success: " + n2Suc + "\n Nr cazuri fara success: " + n2Fal + "\n Nr mediu de iteratii: " + m2Ap + "\n Timp mediu: " + m2Ts);
        }

                
    }
}
