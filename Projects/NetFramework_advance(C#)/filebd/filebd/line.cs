using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace filebd
{
    class line
    {
        public int id;
        public string name;
        public string type;
        public int parentid;

        public line(int i, string n, string t, int p)
        {
            id = i;
            name = n;
            type = t;
            parentid = p;
        }
        public line(object[] li)
        {
            id = (int)li[0];
            name = (string)li[1];
            type = (string)li[2];
            parentid = (int)li[3];
        }

        public object[] tofield()
        {
            object[] f = { id, name, type, parentid };
            return f;
        }

        public override string ToString()
        {
            return id + "\t" + parentid + "\t" + type + "\t" + name;
        }

    }
}
