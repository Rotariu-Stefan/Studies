package puzz8;

public class puzzle 
{
    private static int a[][]=new int[2][2];
    private static int pozl;
    private static int pozc;
    private static int sf[][]=new int[2][2];
    
    public puzzle()
    {

    }
    public static void mutasus()
    {
        if(pozl==0)
            System.out.print("Mutare sus imposibila!");
        else
        {
            a[pozl][pozc]=a[pozl-1][pozc];
            a[pozl-1][pozc]=0;
            pozl--;
        }
    }
    public static void mutajos()
    {
        if(pozl==2)
            System.out.print("Mutare jos imposibila!");
        else
        {
            a[pozl][pozc]=a[pozl+1][pozc];
            a[pozl+1][pozc]=0;
            pozl++;
        }
    }
    public static void mutast()
    {
        if(pozc==0)
            System.out.print("Mutare sus imposibila!");
        else
        {
            a[pozl][pozc]=a[pozl][pozc-1];
            a[pozl][pozc-1]=0;
            pozc--;
        }
    }
    public static void mutadr()
    {
        if(pozc==2)
            System.out.print("Mutare sus imposibila!");
        else
        {
            a[pozl][pozc]=a[pozl][pozc+1];
            a[pozl+1][pozc]=0;
            pozc++;
        }        
    }
    public static void fillstart(int s[])
    {
        int i=0;
        for(int l=0;l<=2;l++)
            for(int c=0;c<=2;c++)
            {
                a[l][c]=s[i];
                i++;
            }
    }
    public static void fillfinish(int s[])
    {
        int i=0;
        for(int l=0;l<3;l++)
            for(int c=0;c<3;c++)
            {
                sf[l][c]=s[i];
                i++;
            }
    }
        
    public static void afiscr()
    {
        for(int l=0;l<3;l++)
        {
            System.out.print("|");
            for(int c=0;c<3;c++)
            {
                System.out.print(a[l][c]+"|");
            }
        }
    }
    public static void afisfin()
    {
        for(int l=0;l<3;l++)
        {
            System.out.print("|");
            for(int c=0;c<3;c++)
            {
                System.out.print(sf[l][c]+"|");
            }
        }
    }
}
