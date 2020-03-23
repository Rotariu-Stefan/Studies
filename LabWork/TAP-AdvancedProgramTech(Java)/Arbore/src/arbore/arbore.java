package arbore;



public class arbore
{
    static nod[] arb=new nod[100];
    static int nnod=0;

    public static void addNod(nod n)
    {
        arb[nnod]=n;
        nnod++;
    }
    public static void addNod(int x)
    {
        arb[nnod]=new nod(x);
        if(nnod!=0)
            if(nnod%2==0)
            {
                arb[nnod].par=arb[(nnod-2)/2];
                arb[(nnod-2)/2].fd=arb[nnod];
            }
            else
            {
                arb[nnod].par=arb[(nnod-1)/2];
                arb[(nnod-1)/2].fs=arb[nnod];
            }
        nnod++;
    }

    private static String spatiu(double len)
    {
        String s="";
        for(int i=0;i<len;i++)
            s+=' ';
        return s;
        }
    private static int nrCifre(int nr)
    {
        int n=0;
        if(nr==0)
            return 1;
        while(nr!=0)
        {
            nr=nr/10;
            n++;
        }
        return n;
    }
    private static void swap(nod x, nod y)
    {
        int a=x.val;
        x.val=y.val;
        y.val=a;

    }

    public static void afis()
    {


        int y=40;

        System.out.print(spatiu(y)+arb[0].val);
        int x=2,j=1;
        while(j<=nnod)
        {
            System.out.print("\n"+spatiu(y/2));

            for(int i=0;i<x;i++)
            {
                if(arb[j]!=null)
                    System.out.print(arb[j].val+spatiu(y-nrCifre(arb[i].val)));
                j++;
            }
            x*=2;
            y=y/2;
        }
        System.out.println();

    }
    public static void raport()
    {
        for(int i=0;i<nnod;i++)
        {
            System.out.print("Nodul"+i+":\t");
            arb[i].afis();
        }
    }
    
    public static void preoAfis(nod r)       //RSD
    {
        if(r!=null)
        {
            System.out.print(r.val+"  ");
            preoAfis(r.fs);
            preoAfis(r.fd);
        }
    }
    public static void inoAfis(nod r)        //SRD
    {
        if(r!=null)
        {
            inoAfis(r.fs);
            System.out.print(r.val+"  ");
            inoAfis(r.fd);
        }
    }
    public static void postoAfis(nod r)      //SDR
    {
        if(r!=null)
        {
            postoAfis(r.fs);
            postoAfis(r.fd);
            System.out.print(r.val+"  ");
        }
    }

    public static void explorare(parcurs parc, functie f)
    {
        parc.cale(arb[0],f);
    }
    public static void actualizare()
    {
        boolean x=false;
        
        while(x==false)
        {
            x=true;
            int i=0;
            
            while(arb[i]!=null)
            {
                if(arb[i].fs!=null && arb[i].val<arb[2*i+1].val)
                {
                    x=false;
                    swap(arb[i],arb[2*i+1]);
                }
                if(arb[i].fd!=null && arb[i].val>arb[2*i+2].val)
                {
                    x=false;
                    swap(arb[i],arb[2*i+2]);
                }
                i++;
            }
        }
    }





}
