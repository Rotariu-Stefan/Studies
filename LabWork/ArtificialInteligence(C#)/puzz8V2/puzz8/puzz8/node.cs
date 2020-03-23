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
        public static Dictionary<state,int> devisA;
        public static Queue<state> devisC;
        public static Stack<state> devis;
        public static int cnt;
        public static TimeSpan ts;
        public static String trace;

        public static void initArray()
        {
            vis = new System.Collections.ArrayList();
            devis = new Stack<state>();
            devisC = new Queue<state>();
            devisA = new Dictionary<state, int>();
            cnt = 0;
            trace = "";
        }

        private static state fin;
        private static state start;

        public node msus;
        public node mjos;
        public node mst;
        public node mdr;

        public state val;

 
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
        public int calcA(state x)
        {
            return calcDist(x) + x.rettr().Length;
        }

        #endregion

        public void executeBKT()
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

        }       //backtracking

        public bool executeBF()
        {

            DateTime dt = DateTime.Now;
            state temp;
            devis.Push(start);

            var distCalc = new Dictionary<state,int>();

            while (devis.Count != 0 && cnt<3000)
            {
                val = devis.Pop();
                distCalc = new Dictionary<state,int>();
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    Console.WriteLine(trace);
                //    afisTrace(trace);
                //    Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp,calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var q = distCalc.OrderBy(k => -k.Value);
                foreach (var item in q)
                    devis.Push(item.Key);
                
            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;
        }       //dfs dist
        public bool executeBF2()
        {
            DateTime dt = DateTime.Now;
            state temp;
            devis.Push(start);

            var distCalc = new Dictionary<state, int>();

            while (devis.Count != 0 && cnt<3000)
            {

                val = devis.Pop();
                distCalc = new Dictionary<state, int>();
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    Console.WriteLine(trace);
               //     afisTrace(trace);
               //     Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var q = distCalc.OrderBy(k => -k.Value);
                foreach (var item in q)
                    devis.Push(item.Key);

            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;
        }       //dfs diff

        public bool executeBF3()
        {

            DateTime dt = DateTime.Now;
            state temp;
            devisC.Enqueue(start);

            var distCalc = new Dictionary<state, int>();

            while (devisC.Count != 0 && cnt < 3000)
            {
                val = devisC.Dequeue();
                distCalc = new Dictionary<state, int>();
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    Console.WriteLine(trace);
                    //    afisTrace(trace);
                    //    Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var q = distCalc.OrderBy(k => k.Value);
                foreach (var item in q)
                    devisC.Enqueue(item.Key);

            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;
        }       //bfs dist
        public bool executeBF4()
        {
            DateTime dt = DateTime.Now;
            state temp;
            devisC.Enqueue(start);

            var distCalc = new Dictionary<state, int>();

            while (devisC.Count != 0 && cnt < 3000)
            {

                val = devisC.Dequeue();
                distCalc = new Dictionary<state, int>();
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    Console.WriteLine(trace);
                    //     afisTrace(trace);
                    //     Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDiff(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var q = distCalc.OrderBy(k => k.Value);
                foreach (var item in q)
                    devisC.Enqueue(item.Key);

            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;
        }       //bfs diff

        public bool executeA()
        {
            DateTime dt = DateTime.Now;
            state temp;
            devisA.Add(start,calcDist(start));

            while (devisA.Count != 0 && cnt < 3000)
            {

                val = devisA.First().Key;
                devisA.Remove(val);
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    //Console.WriteLine(trace);
                         afisTrace(trace);
                    //     Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        if (!devisA.ContainsKey(temp))
                            devisA.Add(temp, calcA(temp));
                        else
                            if(calcA(temp)<devisA[temp])
                            {
                                devisA.Remove(temp);
                                devisA.Add(temp, calcA(temp));                             
                            }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        if (!devisA.ContainsKey(temp))
                            devisA.Add(temp, calcA(temp));
                        else
                            if(calcA(temp)<devisA[temp])
                            {
                                devisA.Remove(temp);
                                devisA.Add(temp,calcA(temp));
                            }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        if (!devisA.ContainsKey(temp))
                            devisA.Add(temp, calcA(temp));
                        else
                            if(calcA(temp)<devisA[temp])
                            {
                                devisA.Remove(temp);
                                devisA.Add(temp,calcA(temp));
                            }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        if (!devisA.ContainsKey(temp))
                            devisA.Add(temp, calcA(temp));
                        else
                            if(calcA(temp)<devisA[temp])
                            {
                                devisA.Remove(temp);
                                devisA.Add(temp,calcA(temp));
                            }
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var v = devisA.OrderBy(k => k.Value);
                devisA = new Dictionary<state, int>();
                foreach (var item in v)
                    devisA.Add(item.Key, item.Value);
            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;

        }       //A*(dist)
        public bool executeHC()
        {

            DateTime dt = DateTime.Now;
            state temp;

            var distCalc = new Dictionary<state, int>();

            val = start;
            while ((distCalc.Count != 0 ||cnt==0) && (cnt < 3000))
            {
                distCalc = new Dictionary<state, int>();
                Console.WriteLine("PAS:" + (cnt++));
                val.afis();

                if (val == fin)
                {
                    ts = DateTime.Now - dt;
                    Console.WriteLine("OMG SUCCESS!!!");
                    trace = val.rettr();
                    Console.WriteLine(trace);
                    afisTrace(trace);
                    //    Thread.Sleep(5000);
                    return true;
                }

                vis.Add(val);
                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutasus())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //       Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutajos())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //      Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutast())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                //    Thread.Sleep(500);
                temp = new state(val);
                if (temp.mutadr())
                    if (isVisited(temp) == false)
                        distCalc.Add(temp, calcDist(temp));
                    else
                        Console.WriteLine("Stare deja vizitata!");

                var q = distCalc.OrderBy(k => k.Value);
                foreach (var item in q)
                {
                    val = item.Key;
                    break;
                }
            }

            ts = DateTime.Now - dt;
            Console.WriteLine("NU S-A GASIT SOLUTIE!!!");
            return false;
        }       //hill climbing


        public void comparare(Random r)
        {
            int n1Suc = 0, n1Fal = 0;
            int n2Suc = 0, n2Fal = 0;
            int n3Suc = 0, n3Fal = 0;
            int n4Suc = 0, n4Fal = 0;
            int n5Suc = 0, n5Fal = 0;

            int m1Ap = 0;
            int m2Ap = 0;
            int m3Ap = 0;
            int m4Ap = 0;
            int m5Ap = 0;

            int m1Ts = 0;
            int m2Ts = 0;
            int m3Ts = 0;
            int m4Ts = 0;
            int m5Ts = 0;

            int m1Dr = 0;
            int m2Dr = 0;
            int m3Dr = 0;
            int m4Dr = 0;
            int m5Dr = 0;

            for (int i = 0; i < 100; i++)
            {
                start = new state();
                start.fillRand(r);

                if (executeBF() == true)
                {
                    m1Ap += cnt;
                    m1Ts += ts.Milliseconds;
                    m1Dr += trace.Length;
                    n1Suc++;
                    initArray();
                }
                else
                {
                    n1Fal++;
                    initArray();
                }

                if (executeBF2() == true)
                {
                    m2Ap += cnt;
                    m2Ts += ts.Milliseconds;
                    m2Dr += trace.Length;
                    n2Suc++;
                    initArray();
                }
                else
                {
                    n2Fal++;
                    initArray();
                }

                if (executeBF3() == true)
                {
                    m3Ap += cnt;
                    m3Ts += ts.Milliseconds;
                    m3Dr += trace.Length;
                    n3Suc++;
                    initArray();
                }
                else
                {
                    n3Fal++;
                    initArray();
                }

                if (executeBF4() == true)
                {
                    m4Ap += cnt;
                    m4Ts += ts.Milliseconds;
                    m4Dr += trace.Length;
                    n4Suc++;
                    initArray();
                }
                else
                {
                    n4Fal++;
                    initArray();
                }

                if (executeA() == true)
                {
                    m5Ap += cnt;
                    m5Ts += ts.Milliseconds;
                    m5Dr += trace.Length;
                    n5Suc++;
                    initArray();
                }
                else
                {
                    n5Fal++;
                    initArray();
                }
            }

            if (n1Suc > 0)
            {
                m1Ap = m1Ap / n1Suc;
                m1Ts = m1Ts / n1Suc;
                m1Dr = m1Dr / n1Suc;
            }

            if (n2Suc > 0)
            {
                m2Ap = m2Ap / n2Suc;
                m2Ts = m2Ts / n2Suc;
                m2Dr = m2Dr / n2Suc;
            }

            if (n3Suc > 0)
            {
                m3Ap = m3Ap / n3Suc;
                m3Ts = m3Ts / n3Suc;
                m3Dr = m3Dr / n3Suc;
            }

            if (n4Suc > 0)
            {
                m4Ap = m4Ap / n4Suc;
                m4Ts = m4Ts / n4Suc;
                m4Dr = m4Dr / n4Suc;
            }

            if (n5Suc > 0)
            {
                m5Ap = m5Ap / n5Suc;
                m5Ts = m5Ts / n5Suc;
                m5Dr = m5Dr / n5Suc;
            }

            Console.WriteLine("\nRezultate DFS 1(prin Distanta):\n Nr cazuri cu success: " + (n1Suc-1) + "\n Nr cazuri fara success: " + n1Fal + "\n Nr mediu de iteratii: " + m1Ap + "\n Timp mediu: " + m1Ts + "\n Drum mediu: "+m1Dr);
            Console.WriteLine("\nRezultate DFS 2(prin Diferenta):\n Nr cazuri cu success: " + (n2Suc-1) + "\n Nr cazuri fara success: " + n2Fal + "\n Nr mediu de iteratii: " + m2Ap + "\n Timp mediu: " + m2Ts + "\n Drum mediu: " + m2Dr);
            Console.WriteLine("\nRezultate BFS 1(prin Diferenta):\n Nr cazuri cu success: " + (n3Suc-1) + "\n Nr cazuri fara success: " + n3Fal + "\n Nr mediu de iteratii: " + m3Ap + "\n Timp mediu: " + m3Ts + "\n Drum mediu: " + m3Dr);
            Console.WriteLine("\nRezultate BFS 2(prin Diferenta):\n Nr cazuri cu success: " + (n4Suc-1) + "\n Nr cazuri fara success: " + n4Fal + "\n Nr mediu de iteratii: " + m4Ap + "\n Timp mediu: " + m4Ts + "\n Drum mediu: " + m4Dr);
            Console.WriteLine("\nRezultate  A* (prin Diferenta):\n Nr cazuri cu success: " + (n5Suc-1) + "\n Nr cazuri fara success: " + n5Fal + "\n Nr mediu de iteratii: " + m5Ap + "\n Timp mediu: " + m5Ts + "\n Drum mediu: " + m5Dr);
        }

                
    }
}
