package jocv;
import java.io.*;

public class Main {

    public static void main(String[] args)
    {
        try
        {
        Joc J=new Joc();
        J.setSize(6, 6);
        J.setLoc(0,2);J.setLoc(0,1);J.setLoc(0,4);J.setLoc(1,4);J.setLoc(1,2);
        J.setLoc(1,0);J.setLoc(1,5);J.setLoc(2,1);J.setLoc(2,3);J.setLoc(2,2);
        J.setLoc(3,2);J.setLoc(3,5);J.setLoc(4,1);J.setLoc(4,4);J.setLoc(5,5);
        //J.setLoc(1,1);J.setLoc(1,2);J.setLoc(2,1);J.setLoc(2,2);
        
        J.afis(J.net);
    //    J.save();
        J.repeatCiclu(4);
        J.afis(J.net);
    //    J=Joc.load();
        J.afis(J.net);
        
        }catch(Exception e)
        {
            System.out.println("ERR !!");
        }

    }

}
