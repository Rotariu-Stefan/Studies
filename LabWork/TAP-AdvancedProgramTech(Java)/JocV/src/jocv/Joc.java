package jocv;
import java.io.*;

public class Joc implements Serializable
{
    boolean net[][];
    int x,y;
    int pop=0;

    boolean stillife=false;

    Joc(){}

    private String adCharNr(char c, int n)
    {
        String s="";
        for(int i=0;i<n;i++)
            s=s+c;
        return s;
    }

    public void setSize(int x1, int y1)
    {
        x=x1;
        y=y1;
        net=new boolean[x][y];
        for(int i=0;i<x;i++)
            for(int j=0;j<y;j++)
                net[i][j]=false;
    }
    public int nrVec(int a, int b)
    {
        int cv=0;
        if(a!=x-1)
            if(net[a+1][b]==true)
                cv++;
        if(b!=y-1)
            if(net[a][b+1]==true)
                cv++;
        if(a!=x-1 && b!=y-1)
            if(net[a+1][b+1]==true)
                cv++;
        if(a!=0)
            if(net[a-1][b]==true)
                cv++;
        if(b!=0)
            if(net[a][b-1]==true)
                cv++;
        if(a!=0 && b!=0)
            if(net[a-1][b-1]==true)
                cv++;
        if(a!=x-1 && b!=0)
            if(net[a+1][b-1]==true)
                cv++;
        if(a!=0 && b!=y-1)
            if(net[a-1][b+1]==true)
                cv++;
        return cv;
    }

    public void afis(boolean[][] a)
    {
        if(stillife==true)
    	    System.out.println("Stil-Life !");
        System.out.println(adCharNr('-',y*2+1)+" Populatie: "+pop);        
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                if(net[i][j]==true)
                    System.out.print("|*");
                else
                    System.out.print("| ");
            }
            System.out.println('|');
        }
        System.out.println(adCharNr('-',y*2+1));
    }

    public void setLoc(int a, int b)
    {
        net[a][b]=true;
        pop++;
    }

    private void cicluMod()
    {
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                if(net[i][j]==false)
                {
                    if(nrVec(i,j)==3)
                    {
                        pop++;
                        net[i][j]=true;
                    }
                }
                else
                {
                    if(nrVec(i,j)<2 || nrVec(i,j)>=4)
                    {
                        pop--;
                        net[i][j]=false;
                    }
                }
            }
        }
    }
    public void repeatCicluMod(int n)
    {
        for(int i=0;i<n;i++)
            cicluMod();
    }

    private void ciclu()
    {
        boolean anet[][]=new boolean[x][y];
        
        for(int i=0;i<x;i++)
        {
            for(int j=0;j<y;j++)
            {
                if(net[i][j]==false)
                {
                    if(nrVec(i,j)==3)
                    {
                        pop++;
                        anet[i][j]=true;
                    }
                    else
                        anet[i][j]=false;
                }
                else
                {
                    if(nrVec(i,j)<2 || nrVec(i,j)>=4)
                    {
                        pop--;
                        anet[i][j]=false;
                    }
                    else
                        anet[i][j]=true;
                }
            }
        }
        if(net==anet)
            stillife=true;
        else
    	    net=anet;
    }
    public void repeatCiclu(int n)
    {
        for(int i=0;i<n;i++)
            ciclu();
    }

    public void save() throws IOException
    {
	FileOutputStream sout = new FileOutputStream("stariJoc");
	ObjectOutputStream s = new ObjectOutputStream(sout);
        s.writeObject(this);
        s.close();
    }
    public static Joc load() throws IOException,ClassNotFoundException
    {
        FileInputStream sin = new FileInputStream("stariJoc");
	ObjectInputStream s = new ObjectInputStream(sin);
        Joc X=(Joc)s.readObject();
        s.close();
        return X;
    }

}
