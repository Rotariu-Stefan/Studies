package nrmari;


public class Main {
    public static void main(String[] args) 
    {
        /*nrmari A=new nrmari(true,8,7);
        A.afisare();
        nrmari B=new nrmari(false,3,6);
        System.out.print(" + ");
        B.afisare();
        nrmari C=A.add(B);
        System.out.print(" = ");
        C.afisare();
        
        System.out.println();
        A.afisare();
        System.out.print(" - ");
        B.afisare();
        nrmari S=A.substract(B);
        System.out.print(" = ");
        S.afisare();
        String sir=B.toString();

        System.out.println("\nLong transformat:"+A.longValue()+"\nSir: "+sir);
        int a[]={2,7,4,6,1};    nrmari x=new nrmari(true,a);
        int b[]={1,0,2};   nrmari y=new nrmari(true,b);
      
        x.afisare();    System.out.print(" * ");
        y.afisare();    System.out.print(" = ");
        nrmari r=x.inm(y);  r.afisare();    System.out.println();
        String s="-945145682";
        nrmari xs=new nrmari(s);
        xs.afisare();*/
        nrmari x=new nrmari("1000000000");
        nrmari y=new nrmari("75000000");
        nrmari c=x.add(y);
        c.afisare();


        
    
    }

}
