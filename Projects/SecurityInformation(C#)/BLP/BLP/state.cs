using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class accesstate
    {
        subcap sb;                                                      //WAER
        objc ob;
        List<subcap.acces> ac;

        #region constructors
        public accesstate() { }
        public accesstate(subcap s, objc o, List<subcap.acces> a)
        {
            sb = s;
            ob = o;
            ac = a;
        }
        #endregion

        #region acces checks
        public bool canRead(mat m)
        {
            if (latice.dom(new slevel(ob), new slevel(sb, 'S')) ==false)
            {
                Console.WriteLine("ss-property breach !");
                return false;
            }
            List<subcap.acces> x = m.retperm(sb, ob);
            if (x.Contains(subcap.acces.read) == false)
            {
                Console.WriteLine("ds-property breach !");
                return false;
            }
            return true;
        }

        public bool canWrite(mat m)
        {
            if (latice.dom(new slevel(ob), new slevel(sb, 'S')) ==false)
            {
                Console.WriteLine("ss-property breach !");
                return false;
            }

            if (sb.getT() == false)
            {
                if (latice.dom(new slevel(sb, 'C'), new slevel(ob)) == false)
                {
                    Console.WriteLine("*-property breach !");
                    return false;
                }
                foreach (objc it in m.obj)
                    if (m.retperm(sb, it).Contains(subcap.acces.read) || m.retperm(sb, it).Contains(subcap.acces.write))
                        if (latice.dom(new slevel(it), new slevel(ob)) == false)
                        {
                            Console.WriteLine("*-property breach !");
                            return false;
                        }
            }

            List<subcap.acces> x = m.retperm(sb, ob);
            if (x.Contains(subcap.acces.write) == false)
            {
                Console.WriteLine("ds-property breach !");
                return false;
            }
            return true;
        }

        public bool canAppend(mat m)
        {
            if (sb.getT() == false)
            {
                if (latice.dom(new slevel(sb, 'C'), new slevel(ob)) == false)
                {
                    Console.WriteLine("*-property breach !");
                    return false;
                }
                foreach (objc it in m.obj)
                    if (m.retperm(sb, it).Contains(subcap.acces.read) || m.retperm(sb, it).Contains(subcap.acces.write))
                        if (latice.dom(new slevel(it), new slevel(ob)) == false)
                        {
                            Console.WriteLine("*-property breach !");
                            return false;
                        }
            }
            List<subcap.acces> x = m.retperm(sb, ob);
            if (x.Contains(subcap.acces.append) == false)
            {
                Console.WriteLine("ds-property breach !");
                return false;
            }
            return true;
        }

        public bool canExec(mat m)
        {
            List<subcap.acces> x = m.retperm(sb, ob);
            if (x.Contains(subcap.acces.exec) == false)
            {
                Console.WriteLine("ds-property breach !");
                return false;
            }
            return true;
        }
        #endregion
    }
}
