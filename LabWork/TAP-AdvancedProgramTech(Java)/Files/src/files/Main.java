package files;
import java.io.*;
public class Main {

    public static void main(String[] args)
    {

        
        File fis=new File("U:\\stravos\\java");  //directorul in care se cauta
        try
        {
        Procese.parcurs(fis,"(\\w+\\.java)");   //pattern pt. potrivirea cu fisiere .java
        }catch(IOException e)
            {
            System.out.println("IOException !!");
            }















        

    }

}
