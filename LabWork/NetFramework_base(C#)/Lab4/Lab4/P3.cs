using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;

namespace Lab4
{
    public class info
    {
        public info(string nume)
        {
            this.nume = nume;
        }

        private readonly string nume;
        public string Nume
        {
            get
            {
                return nume;
            }
        }
    }

    public class infotest
    {
        public void ex3(info i, string s)
        {
            FieldInfo[] xf = i.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance);
            xf[0].SetValue(i, s);
            Console.WriteLine(i.Nume);
        }
    }




}