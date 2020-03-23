using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class magazin
    {
        private List<string> carti;
        private List<string> electro;

        private Dictionary<Iuser,int> useri;        // 1=doar carti, 2=doar electro, 3=ambele

        public magazin()
        {
            carti = new List<string>();
            electro = new List<string>();
            useri=new Dictionary<Iuser,int>();
        }
        public List<string> retC()
        {
            return carti;
        }
        public List<string> retE()
        {
            return electro;
        }

        public void addCarte(string s)
        {
            carti.Add(s);
        /*    foreach(var it in useri)
                if(it.Value!=2)
                    it.Key.update();*/
        }
        public void remCarte(string s)
        {
            carti.Remove(s);
         /*   foreach(var it in useri)
                if (it.Value != 2)
                    it.Key.update();*/
        }
        public void addElectro(string s)
        {
            electro.Add(s);
         /*   foreach (var it in useri)
                if (it.Value != 1)
                    it.Key.update();*/
        }
        public void remElectro(string s)
        {
            electro.Remove(s);
         /*   foreach(var it in useri)
                if (it.Value != 1)
                    it.Key.update();*/
        }

        public void attach(userm u,int d)
        {
            u.setmag(this);
            useri.Add(u,d);
            u.update();
        }
        public void detach(userm u)
        {
            u.setmag(new magazin());
            useri.Remove(u);
        }

        public void afis()
        {
            Console.WriteLine("\nCartile din magazin:");
            foreach (var it in carti)
                Console.Write("  "+it);
            Console.WriteLine("\nElectronicele din magazin:");
            foreach (var it in electro)
                Console.Write("  "+it);
        }

    }

    interface Iuser
    {
        void update();
    }

    class userm : Iuser
    {
        private string nume;
        private List<string> carti;
        private List<string> electro;
        private magazin mag;

        public userm(string s)
        {
            nume = s;
            carti = new List<string>();
            electro = new List<string>();
            mag = new magazin();
        }

        public void setmag(magazin m)
        {
            mag = new magazin();
            mag = m;
        }
        public void update()
        {
            this.carti = mag.retC();
            this.electro = mag.retE();
        }

        public void afis()
        {
            Console.WriteLine("\nCartile lui {0}:", nume);
            foreach (var it in carti)
                Console.Write("  " + it);
            Console.WriteLine("\nElectronicele lui {0}:", nume);
            foreach (var it in electro)
                Console.Write("  " + it);
        }
    }
}
