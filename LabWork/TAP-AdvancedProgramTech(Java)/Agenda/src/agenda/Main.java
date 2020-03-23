package agenda;
import java.io.*;

public class Main {
    public static void main(String[] args)
    {
        Data d1=new Data(17,12,1987);        intrare stravos=new intrare("Stravos",d1,'M');  agenda.addInt(stravos);
        Data d2=new Data(28,1,1960);        intrare ana=new intrare("Ana",d2,'F');           agenda.addInt(ana);
        Data d3=new Data(7,3,1991);        intrare maria=new intrare("Maria",d3,'F');       agenda.addInt(maria);
        Data d4=new Data(12,6,1986);        intrare alex=new intrare("Alex",d4,'M');          agenda.addInt(alex);
        Data d5=new Data(27,1,1976);        intrare andrei=new intrare("Andrei",d5,'M');    agenda.addInt(andrei);
        Data d6=new Data(11,6,2001);        intrare sandra=new intrare("Sandra",d6,'F');     agenda.addInt(sandra);
 //       agenda.delInt(ana);
 //       agenda.delInt(andrei);


        agenda.afis();

        try
        {
        agenda.dupaZodie();
        }catch(IOException e) {e.printStackTrace();}


    }

}
