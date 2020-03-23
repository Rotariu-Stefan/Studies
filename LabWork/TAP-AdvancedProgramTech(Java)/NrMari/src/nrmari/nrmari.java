package nrmari;


public class nrmari
{
    private boolean poz;        //semnul (poz=true <-pozitiv, poz=false <-negativ)
    private int lung;           //lungimea nr.
    private int vNr[];          //vector cu cifre

    //constante
    static final nrmari ZERO=new nrmari("0");
    static final nrmari ONE=new nrmari("1");
    static final nrmari MILLION=new nrmari("1000000");
    static final nrmari BILLION=new nrmari("1000000000");
    static final nrmari TRILLION=new nrmari("1000000000000");
   
    nrmari()        //constructor implicit (nr. 1)
    {
        poz=true;
        lung=1;
        vNr=new int[1];    vNr[1]=1;
    }
    nrmari(boolean p,int n, int l)      //constr. cu p=semn n=cifra care se repeta de l ori
    {
        poz=p;
        lung=l;
        vNr=new int[l];
        for(int i=0;i<l;i++)
            vNr[i]=n;
    }
    nrmari(int n, int l)            //la fel ca mai sus (semn automat pozitiv)
    {
        this(true,n,l);
    }
    nrmari(String str)        //constr. ce primeshte sir de caractere ca parametru
    {
        int i = 0;
        if (str.charAt(0) == '-')       //testam daca numarul e negativ
        {
            poz=false;
            i++;
        }   else poz=true;
        lung=str.length();
        vNr=new int[lung];
        for(;i<lung;i++)
        {
            switch(str.charAt(i))
            {
                case '1': vNr[i]=1; break;
                case '2': vNr[i]=2; break;
                case '3': vNr[i]=3; break;
                case '4': vNr[i]=4; break;
                case '5': vNr[i]=5; break;
                case '6': vNr[i]=6; break;
                case '7': vNr[i]=7; break;
                case '8': vNr[i]=8; break;
                case '9': vNr[i]=9; break;
                default: 
            }
        }
        
        
    }
    nrmari(boolean p,int[] vec)         //constr. cu semn shi vector
    {
        lung=vec.length;
        this.vNr=new int[lung];
        for(int i=0;i<lung;i++)
            this.vNr[i]=vec[i];
        poz=p;
    }
    nrmari(byte[] vec)                  //constr. pt. tablou de octeti
    {
        lung=vec.length;
        this.vNr=new int[lung];
        for(int i=0;i<lung;i++)
            this.vNr[i]=(int)vec[i];
        poz=true;
    }

    public void afisare()              //afisarea nr. pe ecran
    {
        if(poz==false)
            System.out.print("-");
        for(int i=0;i<lung;i++)
            System.out.print(vNr[i]);
    }

    private nrmari adunare(nrmari A)        //functie cu implementarea op. de adunare a 2 numere (independenta de semn)
    {
        nrmari R=new nrmari(true,0,20);         //rezultat partial
        int pas=0;      //verifica daca se trece un 1 mai departe sau nu

            if(this.lung>A.lung)
            {
                int dif=this.lung-A.lung;
                for(int i=A.lung-1;i>=0;i--)
                {
                    R.vNr[i+dif]=(this.vNr[dif+i]+A.vNr[i]+pas>9)?(this.vNr[dif+i]+A.vNr[i]+pas)%10:(this.vNr[dif+i]+A.vNr[i]+pas);
                    if(this.vNr[dif+i]+A.vNr[i]+pas>9) pas=1; else pas=0;
                }
                for(int i=dif-1;i>=0;i--)
                {
                    R.vNr[i]=(this.vNr[i]+pas>9)?(this.vNr[i]+pas)%10:(this.vNr[i]+pas);
                    if(this.vNr[i]+pas>9) pas=1; else pas=0;
                }
                R.lung=this.lung;
                R.poz=this.poz;
            }
            else
            {
                int dif=A.lung-this.lung;
                for(int i=this.lung-1;i>=0;i--)
                {
                    R.vNr[i+dif]=(A.vNr[dif+i]+this.vNr[i]+pas>9)?(A.vNr[dif+i]+this.vNr[i]+pas)%10:(A.vNr[dif+i]+this.vNr[i]+pas);
                    if(A.vNr[dif+i]+this.vNr[i]+pas>9) pas=1; else pas=0;
                }
                for(int i=dif-1;i>=0;i--)
                {
                    R.vNr[i]=(A.vNr[i]+pas>9)?(A.vNr[i]+pas)%10:(A.vNr[i]+pas);
                    if(A.vNr[i]+pas>9) pas=1; else pas=0;
                }
                R.lung=A.lung;
                R.poz=A.poz;
            }
        //adaugarea unui 1 in fatza nr. daca se depaseste lungimea maxima dintre cele 2 nr.
         if(pas==1)
         {
            nrmari RZ=new nrmari(R.poz,1,R.lung+1);
            for(int i=R.lung;i>0;i--)
                RZ.vNr[i]=R.vNr[i-1];
            return RZ;
         }
         else
             return R;


    }
    private nrmari scadere(nrmari A)        //functie cu implementarea op. de scadere a 2 numere (independenta de semn)
    {
        nrmari R=new nrmari(true,0,20);     //rezultat partial
        int pas=0;          //verifica daca se trece un 1 mai departe sau nu

            if(this.lung>A.lung)
            {
                int dif=this.lung-A.lung;
                for(int i=A.lung-1;i>=0;i--)
                {
                    R.vNr[i+dif]=(this.vNr[dif+i]-A.vNr[i]-pas<0)?(this.vNr[dif+i]-A.vNr[i]-pas+10):(this.vNr[dif+i]-A.vNr[i]-pas);
                    if(this.vNr[dif+i]-A.vNr[i]-pas<0) pas=1; else pas=0;
                }
                for(int i=dif-1;i>=0;i--)
                {
                    R.vNr[i]=(this.vNr[i]-pas<0)?(this.vNr[i]-pas+10):(this.vNr[i]-pas);
                    if(this.vNr[i]-pas<0) pas=1; else pas=0;
                }
                R.lung=this.lung;
                R.poz=this.poz;
            }
            else
            {
                int dif=A.lung-this.lung;
                for(int i=this.lung-1;i>=0;i--)
                {
                    R.vNr[i+dif]=(A.vNr[dif+i]-this.vNr[i]-pas<0)?(A.vNr[dif+i]-this.vNr[i]-pas+10):(A.vNr[dif+i]-this.vNr[i]-pas);
                    if(A.vNr[dif+i]-this.vNr[i]-pas>9) pas=1; else pas=0;
                }
                for(int i=dif-1;i>=0;i--)
                {
                    R.vNr[i]=(A.vNr[i]-pas<0)?(A.vNr[i]-pas+10):(A.vNr[i]-pas);
                    if(A.vNr[i]-pas<0) pas=1; else pas=0;
                }
                R.lung=A.lung;
                R.poz=A.poz;
            }
        //eliminarea zerourilor ce pot aparea
        int count=0;
        for(int i=0;i<R.lung;i++)
            if(R.vNr[i]==0)
                count++;
            else break;
        if(count==R.lung)
        {       count--;       R.poz=true; }
        nrmari RZ=new nrmari(R.poz,0,R.lung-count);
        int j=0;
        for(int i=count;i<R.lung;i++)
            RZ.vNr[j++]=R.vNr[i];

        return RZ;



    }
    public nrmari addZero(int x)            //adauga x zerouri la sfarsitul nr.
    {
        nrmari R=new nrmari(this.poz,0,this.lung+x);
        for(int i=0;i<this.lung;i++)
            R.vNr[i]=this.vNr[i];
        return R;

    }


    public nrmari add(nrmari T)         //adunarea (metoda folosita) a 2 nr.
    {
        if(this.poz==T.poz)
            return this.adunare(T);
        else
            return this.scadere(T);
    }
    public nrmari substract(nrmari T)       //scaderea (metoda folosita) a 2 nr.
    {
        if(this.poz==T.poz)
            return this.scadere(T);
        else
            return this.adunare(T);
    }


    public long longValue()             //convertirea nr. la tip long
    {
        long l,s=0;
        for(int i=0;i<this.lung;i++)
        {
            l=(long)(Math.pow(10,this.lung-i-1)*(this.vNr[i]));
            s=s+l;
        }
        return s;

    }
    @Override
    public boolean equals(Object b)         //egalitatea intre 2 nr. (suprascriere equals)
    {
        if (!(b instanceof nrmari))
        {
            return false;
        } else if (poz != ((nrmari) b).poz)
        {
            return false;
        } else if (lung != ((nrmari) b).lung)
        {
            return false;
        } else
        {
            for (int i = 0; i < lung; ++i)
            {
                if (vNr[i] != ((nrmari) b).vNr[i])
                {
                    return false;
                }
            }

            return true;
        }
    }
    @Override
    public String toString()            //convertirea la tip String a unui nr. (suprascriere toString)
    {
        StringBuilder str = new StringBuilder();
        if (poz==false)
        {
            str.append("-");
        }
        str.append(vNr[lung - 1]);
        for (int i=1; i<lung; i++)
            str.append(vNr[i]);
        
        return str.toString();
    }

    public nrmari inmCif(int x)     //inmultirea unui nrmari cu o cifra
    {
        nrmari R=new nrmari(this.poz,0,this.lung);
        int pas=0;
        for(int i=lung-1;i>=0;i--)
        {
            R.vNr[i]=(this.vNr[i]*x+pas)>10?(this.vNr[i]*x+pas)%10:(this.vNr[i]*x+pas);
            if((this.vNr[i]*x+pas)>10)
                pas=(this.vNr[i]*x+pas)/10;
            else pas=0;
        }

        if(pas!=0)
        {
            nrmari RZ=new nrmari(R.poz,pas,R.lung+1);
            for(int i=R.lung;i>0;i--)
                RZ.vNr[i]=R.vNr[i-1];
            return RZ;
        }
        else
            return R;

    }
    public nrmari inm(nrmari X)     //inmultirea a doua nrmari
    {
        nrmari R=new nrmari(true,0,this.lung+X.lung-1);
        int dif=(this.lung>X.lung)?(this.lung-X.lung):(X.lung-this.lung),j=0;
        
        for(int i=X.lung-1;i>=0;i--)
        {
            nrmari med=new nrmari(true,0,this.lung+X.lung);
            med=this.inmCif(X.vNr[i]);
            nrmari med1=med.addZero(j);

            R=R.add(med1);
            j++;

        }
        return R;

        
    }

}
