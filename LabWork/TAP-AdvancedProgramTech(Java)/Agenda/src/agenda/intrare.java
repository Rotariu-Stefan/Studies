package agenda;

enum zodie{berbec,taur,gemeni,rac,leu,fecioara,balanta,scorpion,sagetator,capricorn,varsator,pesti};


public class intrare    //o intrare in agenda(persoana/prieten)
{
    int id=0;
    boolean mark=false;

    String nume;
    Data data;
    char sex;
    
    zodie zod;

    String adresa;
    String tel;
    String email;
    String web;

    intrare(){}
    intrare(String n, Data d, char s, String a, String t, String e, String w)   //constr. cu toate datele(cu contacte)
    {
        nume=n;
        data=d;
        sex=s;
        adresa=a;
        tel=t;
        email=e;
        web=w;
        calcZodie();
    }
    intrare(String n, Data d, char s)   //constr. cu datele doar despre persoane
    {
        this(n,d,s,null,null,null,null);
    }

    void afis()     //afisare date(de la al 2-lea constr.)
    {
        if(id==0)   System.out.println("Aceasta persoana nu este intrata in agenda !!");
        System.out.println("Nume: "+nume+"  Data: "+data.toString()+"  Sex: "+sex+"  Zodie: "+zod);
    }
    void afisTot()  //afisare date(de la primul constr.)
    {
        afis();
        System.out.println("Adresa: "+adresa+"  Telefon: "+tel+"  Email: "+email+"  Adresa Web: "+web);
        calcZodie();
    }

    private void calcZodie()        //calcularea zodiei(prin data de nastere)
    {
        if((data.luna==3 && data.zi>20)||(data.luna==4&&data.zi<21))
            zod=zodie.berbec;
        if((data.luna==4 && data.zi>20)||(data.luna==5&&data.zi<22))
            zod=zodie.taur;
        if((data.luna==5 && data.zi>21)||(data.luna==6&&data.zi<20))
            zod=zodie.gemeni;
        if((data.luna==6 && data.zi>21)||(data.luna==7&&data.zi<21))
            zod=zodie.rac;
        if((data.luna==7 && data.zi>22)||(data.luna==8&&data.zi<21))
            zod=zodie.leu;
        if((data.luna==8 && data.zi>22)||(data.luna==9&&data.zi<20))
            zod=zodie.fecioara;
        if((data.luna==9 && data.zi>21)||(data.luna==10&&data.zi<21))
            zod=zodie.balanta;
        if((data.luna==10 && data.zi>22)||(data.luna==11&&data.zi<20))
            zod=zodie.scorpion;
        if((data.luna==11 && data.zi>21)||(data.luna==12&&data.zi<19))
            zod=zodie.sagetator;
        if((data.luna==12 && data.zi>20)||(data.luna==1&&data.zi<18))
            zod=zodie.capricorn;
        if((data.luna==1 && data.zi>19)||(data.luna==2&&data.zi<17))
            zod=zodie.varsator;
        if((data.luna==2 && data.zi>19)||(data.luna==3&&data.zi<19))
            zod=zodie.pesti;
    }

    @Override
    public String toString()    //redare string cu nume,sex,zodie
    {
        return nume+"_"+sex+"_"+zod;
    }

    /*public String zzz(zodie x)
    {
        switch(x)
        {
            case berbec: return "Berbec";
            case leu: return "Leu";
            case fecioara: return "Fecioara";
            case capricorn: return "Capricorn";
            case varsator: return "Varsator";
            case pesti: return "Pesti";
            case sagetator: return "Sagetator";
            case balanta: return "Balanta";
            case rac: return "Rac";
            case taur: return "Taur";
            case gemeni: return "Gemeni";
            case scorpion: return "Scorpion";
            default: return "";
        }
    }*/










}
