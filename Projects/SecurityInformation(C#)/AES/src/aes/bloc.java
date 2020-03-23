package aes;

public class bloc 
{
    private short[][] bl;
    
    bloc()
    {
        bl=new short[4][4];
        for(int l=0;l<=3;l++)
            for(int c=0;c<=3;c++)
                bl[l][c]=0;
    }
    bloc(short[] b)
    {
        int i=0;
        bl=new short[4][4];
        for(int l=0;l<=3;l++)
            for(int c=0;c<=3;c++)
            {
                bl[l][c]=b[i];
                i++;
            }
    }
    bloc(String s)
    {
        
    }
    public short[][] retbl()
    {
        return this.bl;
    }
    public void afisbl()
    {
        for(int l=0;l<=3;l++)
        {
            System.out.print("\n|");
            for(int c=0;c<=3;c++)
                System.out.print(bl[l][c]+"|");
        }
    }
}
