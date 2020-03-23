package arbore;
public class Main {

    public static void main(String[] args)
    {
        arbore.addNod(11);
        arbore.addNod(15);
        arbore.addNod(10);
        arbore.addNod(21);
        arbore.addNod(25);
        arbore.addNod(30);
        arbore.addNod(40);
                arbore.addNod(new nod(30,arbore.arb[3]));
        arbore.addNod(new nod(55,arbore.arb[3]));
                arbore.addNod(new nod(30,arbore.arb[4]));
        arbore.addNod(new nod(55,arbore.arb[4]));
                arbore.addNod(new nod(30,arbore.arb[5]));
        arbore.addNod(new nod(55,arbore.arb[5]));
                arbore.addNod(new nod(30,arbore.arb[6]));
        arbore.addNod(new nod(55,arbore.arb[6]));       

        arbore.afis();
        arbore.explorare(new inoMod(), new dublare());
        arbore.afis();

        arbore.actualizare();
        System.out.println("-------------------------------------------------------------------------------");
        arbore.afis();

       // arbore.raport();

     }

}
