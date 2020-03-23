package arbore;

public class nod
{
    int val;
    nod par;
    nod fs;
    nod fd;

    nod()
    {
        this.val=0;
    }
    nod(int v)
    {
        this.val=v;
    }
    nod(int v,nod p)
    {
        this.val=v;
        this.par=p;
        if(p.fs!=null&&p.fd!=null)
            System.out.print("ERR:In PLM asta deja are 2 copchii !!");
        else
            if(p.fs==null)
                p.fs=this;
            else
                p.fd=this;
    }

    public void afis()
    {

        System.out.println("Valoare: "+val+"\tParinte: "+((this.par!=null)?(this.par.val):null)+"\tFiu stang: "+((this.fs!=null)?(this.fs.val):null)+"\tFiu drept: "+((this.fd!=null)?(this.fd.val):null));
    }

    
    

}
