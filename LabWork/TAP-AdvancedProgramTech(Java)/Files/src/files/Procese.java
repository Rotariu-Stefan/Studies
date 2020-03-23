package files;
import java.io.*;
import java.util.regex.*;


public class Procese
{


   /*static void raportParcurs(File F, String Expr) throws IOException  <---De Intrebat!
   {
       FileWriter outs=new FileWriter("Raport");
       parcurs(F,Expr,outs);
       outs.close();

   }*/


   static void parcurs(File F, String Expr) throws IOException  //parcurgerea recursiva a directorului(F) si verificarea fisierelor (cu expresia Expr)
   {
        File[] list=F.listFiles();
        File f1;

        for(int i=0;i<list.length;i++)
        {
            f1=list[i];
            if(f1.isFile())
            {
                if(potriv(f1.getName(),Expr))
                {
                    try
                    {
                        FileReader d1=new FileReader(f1);
                        System.out.println("Fisier: "+f1.getName()+"\t"+rezVerif(d1));
                        d1.close();
                    }
                    catch(Exception err)
                    {
                        System.out.println("Fisier: "+f1.getName()+"\t"+"Eroare!");
                    }
                }
            }

            if(f1.isDirectory())
            {
                 parcurs(f1,Expr);
            }
        }
    }

   static String rezVerif(FileReader d) throws IOException,Exception    //trimite un mesaj/eroare in urma verificarii
   {
       if(verif(d)==true)
           return "OK!";
       else
           throw new Exception("Eroare !");

   }

   static boolean verif(FileReader d) throws IOException    //verificarea perechilor de acolade/paranteze
   {
       int c;
       int aDesc=0; int aInc=0;
       int pDesc=0; int pInc=0;
       while((c=d.read())!=-1)
       {
           if(c=='{')
               aDesc++;
           if(c=='}')
               aInc++;
           if(c=='(')
               pDesc++;
           if(c==')')
               pInc++;
           if(aDesc<aInc)
               return false;
           if(pDesc<pInc)
               return false;


       }
       if(aDesc==aInc && pDesc==pInc)
           return true;
       else
           return false;
   }
   static boolean potriv(String str, String pat)    //cautarea expresiei regulate in str (numele fisierului)
   {
        Pattern p=Pattern.compile(pat);
        Matcher m=p.matcher(str);
        return m.matches();
    }























}
