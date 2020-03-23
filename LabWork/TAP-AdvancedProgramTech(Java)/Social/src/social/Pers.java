package social;


public class Pers
{
    static int npers=1;     //numarul total de persoane existenete

    public int pid=0;   //id personal cu care se identifica persoana in vecotorii de relatii
    public boolean mark=false;  //folosit la calcularea apropierii

    final String nume;      //numele
    final int varsta;   //varsta in ani
    final char sex;     //sex
    private String email;   //email

    int cunoaste=0;     //nr pers cunoscute
    int popular=0;      //nr pers care o cunosc

    boolean vRel[]=new boolean[100];    //contine pidurile persoanlor shtiute de pers asta (relatiile Cu)

    Pers(String pnume,int pvarsta, char psex,String pemail) //constructor cu date despre persoana(in plus email)
    {
        //pid=npers;
        nume=pnume;
        varsta=pvarsta;
        sex=psex;
        email=pemail;

        //Retea.retea[pid]=this;

        npers++;
    }
    Pers(String pnume,int pvarsta, char psex)   //constructor cu date despre persoana
    {
        this(pnume,pvarsta,psex,null);
    }


    public void afisPers()     //afiseaza informatii despre persoana
    {
        if(pid==0)
            System.out.print("\nERR:Persoana nu este in Retea !!");

        System.out.println("\nPersoana"+pid+"--->Nume: "+nume+"  Vasta: "+varsta+"  Sex: "+sex);
        System.out.print("         --->Popularitate "+popular+"  Cunoaste: "+cunoaste);
        
    }
    public void afisRelKnow()  //afiseaza numele persoanelor pe care le cunoaste persoana asta
    {
        if(pid==0)
            System.out.print("\nERR:Persoana nu este in Retea !!");
        {
        System.out.print("\n"+nume+" cunoaste pe:");
        for(int i=1;i<Retea.rpers;i++)
            if(vRel[i]==true)
                System.out.print("  "+Retea.retea[i].nume);
        }
    }
    public void afisRelKnown() //afiseaza numele persoanelor ce cunosc persoana respectiva
    {
        if(pid==0)
            System.out.print("\nERR:Persoana nu este in Retea !!");
        else
        {
        System.out.print("\n"+nume+" e cunoscut de:");
        for(int i=1;i<Retea.rpers;i++)
            if(Retea.retea[i].vRel[pid]==true)
                System.out.print("  "+Retea.retea[i].nume);
        }
    }
    public void stats()        //afiseaza toate informatiile despre o persoana(personale si relatii)
    {
        afisPers();
        if(pid!=0)
        {
            afisRelKnow();
            afisRelKnown();
        }
        System.out.println();
    }
    
    public void afisRel()      //afiseaza relatiile unei persoane(de cine e stiuta si cine o stie)
    {
        afisRelKnow();
        if(pid!=0)
            afisRelKnown();
    }

    @Override
    public String toString()    //returneaza un string pt. o persoana (cu nume,sex,varsta)
    {
       return nume+'_'+sex+'_'+varsta;
    }

    public int grad()   //nr pers stiute+care le stie
    {
        return popular+cunoaste;
    }

}
