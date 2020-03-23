using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLP
{
    class Program
    {
        static void Main(string[] args)
        {
            /*          mat ma = new mat();
                        subcap io = new subcap("Iosssn");
                        subcap vs = new subcap("Vasile");
                        ma.addsbj(io);
                        ma.addsbj(vs);
                        objc f1 = new objc("f1");
                        objc f2 = new objc("f2");
                        ma.addobj(f1);
                        ma.addobj(f2);
                        ma.addperm(io, f1, 'A');
                        ma.addperm(vs, f2, 'W');
                        ma.afis();
                        ma.remsbj(vs);
                        ma.remobj(f1);
                        objc f11 = new objc("f11");
                        ma.addobj(f11);
                        subcap sv = new subcap("StravoS");
                        ma.addsbj(sv);
                        ma.addperm(sv, f11, 'R');
                        ma.afis();      */

            ssec S = new ssec();

            S.mat.addsbj(new subcap("StravoS", new List<latice.catg>() { latice.catg.personal, latice.catg.ingineri, latice.catg.clienti }, latice.clas.secret, latice.clas.secret));
            S.mat.addsbj(new subcap("ion", latice.catg.personal, latice.clas.pblic, latice.clas.privat));
            S.mat.addsbj(new subcap("ana", latice.catg.ingineri, latice.clas.pblic, latice.clas.pblic));
            S.mat.addsbj(new subcap("gica", latice.catg.clienti, latice.clas.privat, latice.clas.secret));
            S.mat.addsbj(new subcap("vlad", latice.clas.privat, latice.clas.privat));

            S.mat.addobj(new objc("ob1", new List<latice.catg>() { latice.catg.personal, latice.catg.clienti }, latice.clas.secret));
            S.mat.addobj(new objc("ob2", latice.catg.personal, latice.clas.privat));
            S.mat.addobj(new objc("ob3", latice.catg.ingineri, latice.clas.pblic));
            S.mat.addobj(new objc("ob4", latice.catg.clienti, latice.clas.secret));
            S.mat.addobj(new objc("ob5", latice.clas.secret));

            S.mat.addperm("StravoS", "ob1", 'R'); S.mat.addperm("StravoS", "ob2", 'R'); S.mat.addperm("StravoS", "ob3", 'R'); S.mat.addperm("StravoS", "ob4", 'R'); S.mat.addperm("StravoS", "ob5", 'R');
            S.mat.addperm("StravoS", "ob3", 'W');
            S.mat.addperm("ion", "ob2", 'R'); S.mat.addperm("ion", "ob2", 'A');



            S.afis();

            S.testWrite("StravoS", "ob3");
            S.setTrust("StravoS");
            S.testWrite("StravoS", "ob3");
            S.unsetTrust("StravoS");
            S.testWrite("StravoS", "ob3");

            S.testRead("ion", "ob2");
            S.testAppend("ion", "ob2");
            S.testWrite("ion", "ob2");
            S.testRead("ion", "ob5");
            S.testAppend("ion", "ob5");



            Console.Read();
        }
    }
}
