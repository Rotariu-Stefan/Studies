package agenda;
import java.io.*;

public class agenda
{
    static intrare[] agenda=new intrare[100];
    static int nint=1;

    static void addInt(intrare a)   //adauga o intrare in agenda
    {
        if(a.id!=0)
            System.out.println("ERR:Persoana "+a.toString()+" se afla deja in agenda");
        agenda[nint]=a;
        a.id=nint;
        nint++;
    }
    static void delInt(intrare a)   //sterge o intrare din agenda
    {
        if(a.id==0)
            System.out.println("ERR:Persoana "+a.toString()+" nu se afla in agenda");
        else
        {
            for(int i=a.id;i<nint;i++)
            {
                agenda[i].id--;
                agenda[i]=agenda[i+1];
            }
            a.id=0;
            nint--;
        }
    }

    static void afis()      //afiseaza intrarile
    {
        for(int i=1;i<nint;i++)
            agenda[i].afis();
    }
    static void afisTot()   //afiseaza intrarile(cu tot cu contancte)
    {
        for(int i=0;i<nint;i++)
            agenda[i].afisTot();
    }

    static void dupaZodie() throws IOException      //parcurgerea intrarilor si gruparea dupa zodii (in fisierul ZodieSort.txt)
    {

   //     intrare[] dz=new intrare[nint];

        boolean check=true;
        zodie z=zodie.berbec;
   //     int count=0;
      
        BufferedWriter outs=new BufferedWriter(new FileWriter("ZodieSort.txt"));

        outs.write("Sortare dupa zodii: \n\n");
        while(check==true)
        {
            check=false;
            for(int i=1;i<nint;i++)
                if(agenda[i].mark==false)
                {
                    z=agenda[i].zod;
                    outs.write(z.toString()+":\n");
                    outs.write('\t'+agenda[i].nume+'\n');
           //         dz[count]=agenda[i];
           //         count++;
                    agenda[i].mark=true;
                    check=true;
                    break;
                }
            for(int i=1;i<nint;i++)
                if(agenda[i].mark==false && agenda[i].zod==z)
                {
                    outs.write("\t"+agenda[i].nume+'\n');
           //         dz[count]=agenda[i];
           //         count++;
                    agenda[i].mark=true;
                }
            outs.write('\n');

        }
        outs.close();
        for(int i=1;i<nint;i++)
            agenda[i].mark=false;



    }










}
