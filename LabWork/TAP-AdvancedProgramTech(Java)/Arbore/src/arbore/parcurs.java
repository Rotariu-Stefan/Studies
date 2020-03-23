package arbore;

public interface parcurs
{
    public void cale(nod n, functie f);
}

class preoMod implements parcurs
{
    public void cale(nod r, functie f)       //RSD
    {
        if(r!=null)
        {
            f.modif(r);
            //System.out.println(r.val+"  ");
            cale(r.fs, f);
            cale(r.fd, f);
        }
    }
}

class postoMod implements parcurs
{
    public void cale(nod r, functie f)      //SDR
    {
        if(r!=null)
        {
            cale(r.fs, f);
            cale(r.fd, f);
            f.modif(r);
            //System.out.println(r.val+"  ");
        }
    }
}

class inoMod implements parcurs
{
    public void cale(nod r, functie f)        //SRD
    {
        if(r!=null)
        {
            cale(r.fs, f);
            f.modif(r);
            //System.out.println(r.val+"  ");
            cale(r.fd, f);
        }
    }
}