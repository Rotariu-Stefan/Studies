package social;

public class Coada      //coada(fifo) cu persoane folosita la calculareaa apropierii
{
    private Pers coada[]=new Pers[100];     //vector persoane
    int clen=1;     //nr. de pers aflate in vector

    Coada(){}
    Coada(Pers[] v)     //constructor cu vector
    {
        clen=v.length;
        for(int i=0;i<clen;i++)
            coada[i]=v[i];
        
    }

    Pers poptest()  //returneaza doar "valoarea" urmatoarei persoane din coada
    {
        return coada[1];
    }
    
    Pers pop()      //returneaza Si Scoate urmatoarea persoana din coada
    {
        Pers aux=coada[1];
        for(int i=1;i<clen;i++)
                coada[i]=coada[i+1];
        clen--;
        return aux;
    }
    void push(Pers x)   //adauga pe x in coada
    {
        coada[clen]=x;
        clen++;
    }

}
