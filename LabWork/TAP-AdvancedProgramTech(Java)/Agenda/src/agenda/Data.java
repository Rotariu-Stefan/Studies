package agenda;

public class Data       //tipul datei de nastere
{
    int an;
    int zi;
    int luna;

    Data()      //constr. implicit ->1-1-1
    {
        an=1;
        zi=1;
        luna=1;
    }
    Data(int z,int l,int a) //constr. (zi,luna,an)
    {
        boolean valid=true;
        an=a;

        if(0<l && l<13)
            luna=l;
        else    valid=false;

        if(l==2)
            if(a%4==0)
            {
                if(0<z && z<30)
                    zi=z;
            }
            else
                if(0<z && z<29)
                    zi=z; else{}
        else
            if(l==4 || l==6 ||l==9 || l==11)
            {
                if(0<z && z<31)
                    zi=z;
            }
            else
                if(0<z && z<32)
                    zi=z;
                else
                    valid=false;
        if(valid==false)
            System.out.println("Data nu este corecta !!");
     }

    @Override
    public String toString()    //string cu data : zz-mm-yyyy
    {
        return zi+"-"+luna+"-"+an;
    }



}
