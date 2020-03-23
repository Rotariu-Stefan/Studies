package social;

public class Main {

    public static void main(String[] args)
    {
        Pers stravos=new Pers("StravoS",22,'M',"stravos_11@yahoo.com"); Retea.addPers(stravos);
        Pers gigel=new Pers("Gigel",21,'M');    Retea.addPers(gigel);
        Pers ana=new Pers("Ana",18,'F');        Retea.addPers(ana);
        Pers george=new Pers("George",35,'M');  Retea.addPers(george);
        Pers andrea=new Pers("Andreea",25,'F'); Retea.addPers(andrea);
        Pers xandra=new Pers("Xandra",31,'F');  Retea.addPers(xandra);

        
        Retea.addRel(stravos, ana);
        Retea.addRel(ana, gigel);
        Retea.addRel(gigel, xandra);
        Retea.addRel(xandra,andrea);
        Retea.addRel(andrea,george);
        Retea.addRel(george,stravos);

        System.out.println("\n"+stravos.toString()+" si "+andrea.toString()+" se cunosc prin "+Retea.apropiereB(stravos,andrea)+"persoane");

  //      Retea.addRel(stravos, gigel);
 //       Retea.addRel(andrea,stravos);
//        Retea.addRel(ana,xandra);

        /*for(int i=1;i<Retea.rpers;i++)
            for(int j=i+1;j<Retea.rpers;j++)
                Retea.addRelB(Retea.retea[i],Retea.retea[j]);*/
        Retea.isConex();
     //   Retea.isComplet();

       
    }

}
