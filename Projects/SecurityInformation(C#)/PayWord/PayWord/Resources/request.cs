using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PayWord.Resources
{
    public class urequest
    {
        string name;
        string mail;
        string card;
        rsakey upbk;

        public urequest(string n, string m, string c, rsakey rk)
        {
            name = n;
            mail = m;
            card = c;
            upbk = rk;
        }

        public string getname()
        {
            return name;
        }
        public string getmail()
        {
            return mail;
        }
        public string getcard()
        {
            return card;
        }
        public rsakey getkey()
        {
            return upbk;
        }

    }
}
