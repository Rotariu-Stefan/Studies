package social;

public final class Retea
{
    static Pers[] retea=new Pers[100];  //persoanele care au fost adaugate in Retea
    static int rpers=1;                 //nr persoane prezente in Retea

    static void afisare()       //afisarea tututor persoanelor din Retea cu datele lor
    {
        for(int i=1;i<rpers;i++)
            retea[i].stats();
    }

    static void addRel(Pers A,Pers B)   //adauga o relatie de la A la B
    {
        if(A==B)
            System.out.println("ERR:Nu se poate adauga o relatie unei persoane cu ea insasi");
        else
        if(A.pid==0||B.pid==0)
            System.out.println("ERR::Persoanele trebuie adaugate la Retea inainte de a putea forma relatii!");
        else
        {
        A.vRel[B.pid]=true;
        B.popular++;
        A.cunoaste++;
        }

    }
    static void addRelB(Pers A,Pers B)  //adauga relatie bivalenta (A la B si B la A)
    {
        addRel(A,B);
        addRel(B,A);
    }


    static void delRel(Pers A,Pers B)   //sterge relatia de la A la B dak exista
    {
        A.vRel[B.pid]=false;
        B.popular--;
        A.cunoaste--;

    }
    static void delRelB(Pers A,Pers B)  //sterge relatia de la A la B si/sau B la A dak exista
    {
        delRel(A,B);
        delRel(B,A);
    }

    static void addPers(Pers A)     //adauga persoana in Retea
    {
        if(A.pid!=0)
            System.out.println("ERR:Persoana "+A.toString()+"este deja in retea !");
        retea[rpers]=A;
        A.pid=rpers;
        rpers++;

    }
    static void delPers(Pers A)     //sterge persoana din Retea
    {
        if(A.pid==0)
            System.out.println("ERR:Persoana "+A.toString()+"nu este in retea !");
        else
        {
        for(int i=1;i<rpers;i++)
        {
            if(A.vRel[i]==true)
            {
                retea[i].popular--;
                A.vRel[i]=false;
            }
            if(retea[i].vRel[A.pid]==true)
                retea[i].cunoaste--;
        }
        for(int i=1;i<rpers;i++)
            for(int j=A.pid;j<rpers;j++)
                retea[i].vRel[j]=retea[i].vRel[j+1];
        
        
        for(int i=A.pid;i<rpers;i++)
        {
            retea[i]=retea[i+1];
            if(retea[i]!=null)
                retea[i].pid--;
        }

        rpers--;
        A.pid=0;
        }
    }


    @Override
    public String toString()        //listarea persoanelor
    {
        String s="";
        for(int i=1;i<rpers-1;i++)
            s+=retea[i].toString()+' ';
        s=s+retea[rpers-1];
        return s;
    }

    private static int aprop(Pers a,Pers b)     //apropierea dintre A la B (-2dak nu exista ambele in Retea, -1dak nu se pot lega, altfel nr. persoane dintre ele, 0=relatie directa)
    {
        if(a.pid==0 || b.pid==0)
        {
            System.out.println("ERR:Persoanele trebuie sa fie in Retea !!");
            return -2;
        }
        if(a==b)
        {
            System.out.println("ERR:Nu se poate calcula apropierea unei persoane cu ea insasi !!");
            return -2;
        }
        int A=0;
        Pers aux=a;
        Coada C=new Coada();



        if(a.vRel[b.pid]==true)
            return A;
        A++;
        a.mark=true;
        for(int i=1;i<rpers;i++)
              if(aux.vRel[retea[i].pid]==true && retea[i].mark==false)
              {
                C.push(retea[i]);
                retea[i].mark=true;
              }

        C.push(b);
        aux=C.pop();
        while(A>0)
        {
           
            if(aux==b)
                if(C.poptest()!=null)
                {
                    A++;
                    aux=C.pop();
                    C.push(b);
                }
                else
                    return -1;

            if(aux.vRel[b.pid]==true)
                return A;
            for(int i=1;i<rpers;i++)
              if(aux.vRel[retea[i].pid]==true && retea[i].mark==false)
              {
                C.push(retea[i]);
                retea[i].mark=true;
              }
            if(C.poptest()!=null)
                aux=C.pop();
            else break;
        }

        return -1;
    }

    public static int apropiere(Pers a,Pers b)  //calculeaza apropierea: A->B
    {
        int x=aprop(a,b);
        for(int i=1;i<rpers;i++)
            retea[i].mark=false;
        return x;

    }
    public static int apropiereB(Pers a,Pers b) //calculeaza apropierea ambivalenta: A<->B (minimul dintre A->B si B->A)
    {
        int v1=apropiere(a,b);
        int v2=apropiere(b,a);
        if(v1==-1)
            if(v2==-1)
                return -1;
            else
                return v2;
        if(v2==-1)
            return v1;
        return (v1>v2)?v2:v1;
    }

    public static int nPers()   //nr persoane
    {
        return Pers.npers;
    }
    public static int nPersR()  //nr persoane in Retea
    {
        return Retea.rpers;
    }
    public static void nrPersAfis()    //afiseaza nr persoane
    {
        System.out.println("Exista "+(Retea.nPers()-1)+" persoane declarare\n"+"       "+(Retea.nPersR()-1)+" persoane in Retea");
        System.out.println(Retea.nPers()-Retea.nPersR()+" persoane sunt in afara retelei");
    }

    public static void conectivPers()   //sorteaza si afiseaza conectivitatea in retea
    {

        int max=0;    int jmax=0;
        for(int i=1;i<rpers;i++)
        {
            for(int j=1;j<rpers;j++)
                if(max<retea[j].grad() && retea[j].mark==false)
                {
                    max=retea[j].grad();
                    jmax=j;
                }
            System.out.println(retea[jmax].toString()+" - "+max);
            retea[jmax].mark=true;
            max=0;
            jmax=0;
        }
        for(int i=1;i<rpers;i++)
            retea[i].mark=false;
    }
    public static void stat1()  //afiseaza in procente persoanele dupa sex din retea
    {
        int nM=0,nF=0;
        for(int i=1;i<rpers;i++)
        {
            if(retea[i].sex=='F')
                nF++;
            if(retea[i].sex=='M')
                nM++;
        }
        nM=(nM*100)/(nM+nF);
        nF=100-nM;
        System.out.println("Barbati: "+nM+"%\n"+"Femei: "+nF+'%');
    }

    public static void isConex()        //verifica daca graful format de relatiile din retea este convex
    {
        boolean c=true;
        for(int i=1;i<rpers;i++)
            for(int j=i+1;j<rpers;j++)
                if(i!=j)
                    if((aprop(retea[i],retea[j])<0)&&(aprop(retea[j],retea[i])<0))
                        c=false;
        if(c==true)
            System.out.println("Graful Retelei este Conex!");
        else
            System.out.println("Graful Retelei NU este Conex!");
    }

    public static void isComplet()      //verifica daca graful format de relatiile din retea este convex
    {
        boolean c=true;
        for(int i=1;i<rpers;i++)
            for(int j=i+1;j<rpers;j++)
                if(i!=j)
                    if((aprop(retea[i],retea[j])!=0)&&(aprop(retea[j],retea[i])!=0))
                        c=false;
        if(c==true)
            System.out.println("Graful Retelei este Complet!");
        else
            System.out.println("Graful Retelei NU este Complet!");
    }




}
