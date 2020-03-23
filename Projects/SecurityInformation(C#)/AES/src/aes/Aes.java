package aes;

public class Aes 
{

    public static void main(String[] args) 
    {
        short a[]={0x1,0xa,0x1a,0x1b,0x2b,0x7f,0x56,0xff,1,1,1,1,1,1,1,1};
        bloc b=new bloc(a);
        b.afisbl();
    }
}
