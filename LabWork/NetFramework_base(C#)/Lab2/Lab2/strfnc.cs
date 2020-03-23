using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class strfnc
    {
        public enum TipCaracter
        {
            Litera = 0x0001,
            Numar = 0x0002,
            Punctuatie = 0x0004
        }


        public String f1(String[] a, char c)
        {
            String s="";
            if (a[0] != null)
            {
                s = a[0];
                for (int i = 1; i < a.Length; i++)
                    s = s + c + a[i];
            }
                return s;
            
        }

        public String[] f2(String a,char c)
        {

            String[] s= a.Split(c);
            return s;
        }

        public void f4(char c)
        {
            if (Char.IsControl(c))
                Console.WriteLine("Este Control!");
            if (Char.IsDigit(c))
                Console.WriteLine("Este Cifra!");
            if (Char.IsLetter(c))
                Console.WriteLine("Este Litera!");
            if (Char.IsNumber(c))
                Console.WriteLine("Este Numar!");
            if (Char.IsPunctuation(c))
                Console.WriteLine("Este Semn de punctuatie!");
            if (Char.IsSeparator(c))
                Console.WriteLine("Este Separator!");
            if (Char.IsSurrogate(c))
                Console.WriteLine("Este Inlocuitor!");
            if (Char.IsSymbol(c))
                Console.WriteLine("Este Simbol!");
            if (Char.IsWhiteSpace(c))
                Console.WriteLine("Este Spatziu!");
        }
        public void f4(String s, int x)
        {
            char c = s[x];
            f4(c);
        }
        public bool f4a1(char s, TipCaracter t)
        {
                if((int)(t)==1)
                    if(!Char.IsLetter(s))
                        return false;
                if((int)(t)==2)
                    if(!Char.IsNumber(s))
                        return false;
                if((int)(t)==4)
                    if(!Char.IsPunctuation(s))
                        return false;
            return true;
        }
        public bool f4a2(char s, TipCaracter[] t)
        {
            bool b=false;
            for(int i=0;i<t.Length;i++)
                if(f4a1(s,t[i]))
                {
                    b=true;
                    break;
                }
            return b;
        }

        public bool f4(String s, TipCaracter[] t)
        {
            for (int i = 0; i < s.Length; i++)
                if (!f4a2(s[i], t))
                    return false;
            return true;
        }


        public int[] f3(String s, char c, out int nap, bool sens)       //true= ambele
        {
            int[] v=new int[20];
            nap=0;
            int i=0;

            if (sens == true)
            {
                while ((i = s.IndexOf(c, i)) != -1)
                {
                    v[nap] = i;
                    nap++;
                    i++;
                }
                if (Char.IsLower(c) == true)
                    c=Char.ToUpper(c);
                else
                    c=Char.ToLower(c);
                i = 0;
                while ((i = s.IndexOf(c,i)) != -1)
                {
                    v[nap] = i;
                    nap++;
                    i++;
                }
            }
            else
            {
                while ((i = s.IndexOf(c, i)) != -1)
                {
                    v[nap] = i;
                    nap++;
                    i++;
                }
            }
            return v;
        }

        public void f5()
        {
            int e1=0,e2=0;

            Console.WriteLine("CurrentDir:  "+Environment.CurrentDirectory);
            String[] s1 = Environment.GetLogicalDrives();
            foreach (String s in s1)
            {
                e1++;
                Console.WriteLine("Logical Drive n" + e1 + ":  " + s);
            }
            String[] s2 = Environment.GetCommandLineArgs();
            foreach (String s in s2)
            {
                e2++;
                if(e2!=1)
                  Console.WriteLine("Commandline Arg n" + e2 + ":  " + s);
            }
            Console.WriteLine("Machinename:  "+Environment.MachineName);
            Console.WriteLine("OSVersion:  "+Environment.OSVersion);
            Console.WriteLine("Username:  "+Environment.UserName);
        }
    }
}
