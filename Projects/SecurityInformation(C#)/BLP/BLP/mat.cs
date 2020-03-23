using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class mat
    {
        public List<objc> obj;
        public List<subcap> sbj;

        public mat()
        {
            obj = new List<objc>();
            sbj = new List<subcap>();
        }

        #region indexfind
        private int sbjindex(string s)
        {
            for (int i = 0; i < sbj.Count; i++)
                if (sbj[i].retn() == s)
                    return i;
            return -1;
        }
        private int objindex(string s)
        {
            for (int i = 0; i < obj.Count; i++)
                if (obj[i].retn() == s)
                    return i;
            return -1;
        }
        #endregion

        #region obj/sbj by name
        public void addobj(string s)
        {
            objc o = new objc(s);
            obj.Add(o);
            foreach (var it in sbj)
                it.initobj(1);
        }
        public void addsbj(string s)
        {
            sbj.Add(new subcap(s));
            sbj[sbj.Count - 1].initobj(obj.Count);
        }
        public void remobj(string s)
        {
            int x = objindex(s);
            obj.RemoveAt(x);
            foreach (var it in sbj)
                it.remob(x);
        }
        public void remsbj(string s)
        {
            sbj.RemoveAt(sbjindex(s));
        }
        #endregion

        #region rights by name
        public void setperm(string sb, string ob, List<subcap.acces> ac)
        {
            sbj[sbjindex(sb)].setcapI(objindex(ob), ac);
        }
        public void setperm(string sb, string ob, subcap.acces dr)
        {
            List<subcap.acces> ac = new List<subcap.acces>();
            ac.Add(dr);
            sbj[sbjindex(sb)].setcapI(objindex(ob), ac);
        }
        public void addperm(string sb, string ob, subcap.acces dr)
        {
            sbj[sbjindex(sb)].addcapI(objindex(ob), dr);
        }
        public void addperm(string sb, string ob, char c)
        {
            if (c == 'W')
                addperm(sb, ob, subcap.acces.write);
            if (c == 'A')
                addperm(sb, ob, subcap.acces.append);
            if (c == 'R')
                addperm(sb, ob, subcap.acces.read);
            if (c == 'E')
                addperm(sb, ob, subcap.acces.exec);
        }
        public List<subcap.acces> retperm(string sb, string ob)
        {
            return sbj[sbjindex(sb)].retcapI(objindex(ob));
        }
        #endregion

        #region obj/sbj
        public void addobj(objc o)
        {
            obj.Add(o);
            foreach (var it in sbj)
                it.initobj(1);
        }
        public void addsbj(subcap s)
        {
            sbj.Add(s);
            sbj[sbj.Count - 1].initobj(obj.Count);
        }
        public void remobj(objc o)
        {
            int x = obj.IndexOf(o);
            obj.Remove(o);
            foreach (var it in sbj)
                it.remob(x);
        }
        public void remsbj(subcap s)
        {
            sbj.Remove(s);
        }
        #endregion

        #region rights
        public void setperm(subcap sb, objc ob, List<subcap.acces> ac)
        {
            sbj.Find(it => it == sb).setcapI(obj.IndexOf(ob), ac);
        }
        public void setperm(subcap sb, objc ob, subcap.acces dr)
        {
            List<subcap.acces> ac = new List<subcap.acces>();
            ac.Add(dr);
            sbj.Find(it => it == sb).setcapI(obj.IndexOf(ob), ac);
        }
        public void addperm(subcap sb, objc ob, subcap.acces dr)
        {
            sbj.Find(it => it == sb).addcapI(obj.IndexOf(ob), dr);
        }
        public void addperm(subcap sb, objc ob, char c)
        {
            if (c == 'W')
                addperm(sb, ob, subcap.acces.write);
            if (c == 'A')
                addperm(sb, ob, subcap.acces.append);
            if (c == 'R')
                addperm(sb, ob, subcap.acces.read);
            if (c == 'E')
                addperm(sb, ob, subcap.acces.exec);
        }
        public List<subcap.acces> retperm(subcap sb, objc ob)
        {
            return sbj.Find(it => it == sb).retcapI(obj.IndexOf(ob));
        }
        #endregion

        public void clear()
        {
            obj = new List<objc>();
            sbj = new List<subcap>();
        }
        public void afis()
        {
            Console.Write("\n\t");
            foreach (var it in obj)
                Console.Write(it.retn() + "\t");
            foreach (var it in sbj)
            {
                Console.Write("\n" + it.retn() + " : ");
                for (int i = 0; i < it.retcap().Count; i++)
                {
                    if (it.retcapI(i).Count == 0)
                        Console.Write("----\t");
                    else
                    {
                        var x = it.retcapI(i);
                        if (x.Contains(subcap.acces.write))
                            Console.Write("W");
                        else
                            Console.Write("-");
                        if (x.Contains(subcap.acces.append))
                            Console.Write("A");
                        else
                            Console.Write("-");
                        if (x.Contains(subcap.acces.read))
                            Console.Write("R");
                        else
                            Console.Write("-");
                        if (x.Contains(subcap.acces.exec))
                            Console.Write("E");
                        else
                            Console.Write("-");

                        Console.Write("\t");
                    }
                }
            }
            Console.WriteLine();
        }


    }
}
