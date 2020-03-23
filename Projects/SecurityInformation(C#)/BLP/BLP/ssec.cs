using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class ssec
    {
        public mat mat = new mat();

        public ssec()
        {
            mat = new mat();
        }

        #region tests
        public void testWrite(subcap sb, objc ob)
        {
            if ((new accesstate(sb, ob, mat.retperm(sb, ob)).canWrite(mat)) == false)
                Console.WriteLine("NO, SUBJECT {0} CANNOT PERFORM WRITE ON {1}", sb.retn(), ob.retn());
            else
                Console.WriteLine("YES, SUBJECT {0} CAN PERFORM WRITE ON {1}", sb.retn(), ob.retn());
        }
        public void testAppend(subcap sb, objc ob)
        {
            if ((new accesstate(sb, ob, mat.retperm(sb, ob)).canAppend(mat)) == false)
                Console.WriteLine("NO, SUBJECT {0} CANNOT PERFORM APPEND ON {1}", sb.retn(), ob.retn());
            else
                Console.WriteLine("YES, SUBJECT {0} CAN PERFORM APPEND ON {1}", sb.retn(), ob.retn());
        }
        public void testRead(subcap sb, objc ob)
        {
            if ((new accesstate(sb, ob, mat.retperm(sb, ob)).canRead(mat)) == false)
                Console.WriteLine("NO, SUBJECT {0} CANNOT PERFORM READ ON {1}", sb.retn(), ob.retn());
            else
                Console.WriteLine("YES, SUBJECT {0} CAN PERFORM READ ON {1}", sb.retn(), ob.retn());
        }
        public void testExec(subcap sb, objc ob)
        {
            if ((new accesstate(sb, ob, mat.retperm(sb, ob)).canExec(mat)) == false)
                Console.WriteLine("NO, SUBJECT {0} CANNOT PERFORM EXECUTE ON {1}", sb.retn(), ob.retn());
            else
                Console.WriteLine("YES, SUBJECT {0} CAN PERFORM EXECUTE ON {1}", sb.retn(), ob.retn());
        }
        #endregion

        #region tests by name
        public void testWrite(string sb, string ob)
        {
            testWrite(mat.sbj.Find(its => its.retn() == sb), mat.obj.Find(ito => ito.retn() == ob));
        }
        public void testAppend(string sb, string ob)
        {
            testAppend(mat.sbj.Find(its => its.retn() == sb), mat.obj.Find(ito => ito.retn() == ob));
        }
        public void testRead(string sb, string ob)
        {
            testRead(mat.sbj.Find(its => its.retn() == sb), mat.obj.Find(ito => ito.retn() == ob));
        }
        public void testExec(string sb, string ob)
        {
            testExec(mat.sbj.Find(its => its.retn() == sb), mat.obj.Find(ito => ito.retn() == ob));
        }
        #endregion

        public void setTrust(string sb)
        {
            mat.sbj.Find(its => its.retn() == sb).setTrust();
        }
        public void unsetTrust(string sb)
        {
            mat.sbj.Find(its => its.retn() == sb).unsetTrust();
        }
        public void afis()
        {
            Console.WriteLine("\n\t\t\tMATRICE:\n");
            mat.afis();
            Console.WriteLine("\n\t\t\tSUBIECTI:\n");
            foreach(var its in mat.sbj)
                its.afis();
            Console.WriteLine("\n\t\t\tOBIECTE:\n");
            foreach(var ito in mat.obj)
                ito.afis();
        }
    }
}
