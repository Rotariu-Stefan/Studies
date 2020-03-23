package arbore;

public interface functie
{
    void modif(nod n);
}

class dublare implements functie
{
    public void modif(nod n)
    {
        n.val*=2;
    }
}
class incrementare implements functie
{
    public void modif(nod n)
    {
        n.val++;
    }
}
class injumatatire implements functie
{
    public void modif(nod n)
    {
        n.val/=2;
    }
}

class scoateCifra implements functie
{
    public void modif(nod n)
    {
        n.val/=10;
    }
}
