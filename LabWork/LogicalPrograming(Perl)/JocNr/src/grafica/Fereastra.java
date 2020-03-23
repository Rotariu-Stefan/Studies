package grafica;
import java.awt.*;
import java.awt.event.*;
import jpl.*;
import java.util.*;

class Fereastra extends Frame implements ActionListener
{
        int x=0;
    	
        public Fereastra(String titlu)
        {
		super(titlu);
                this.addWindowListener(new WindowAdapter()
                {
		//corpul clasei anonime
                    public void windowClosing(WindowEvent e)
                    {
                        System.exit(0);
                    }
                });

	}
	
        
        public void initializare()
        {
                new Query("consult('test.pl')").oneSolution();
 
                setLayout(new BorderLayout());
		setSize(290, 240);		

      		Panel maze = new Panel();
		maze.setLayout(new GridLayout(7,7));

                Query q=new Query("pt(X,Y,Z)");
                while (q.hasMoreSolutions())
                {
                    Hashtable hash = q.nextSolution();
                    maze.add(new Label(hash.get("X").toString()));
                }
                Panel SH = new Panel();
                Button S=new Button("Start");
                S.addActionListener(this);
                Button H=new Button("Mazehelp");
                H.addActionListener(this);
                SH.add(S);

                Button b1 = new Button("Sus");
		SH.add(b1);
                b1.addActionListener(this);

                SH.add(H);


                add(maze, BorderLayout.CENTER);
                add(SH, BorderLayout.NORTH);


		Button b2 = new Button("Jos");
		add(b2, BorderLayout.SOUTH);
                b2.addActionListener(this);

                Button b3 = new Button("Dreapta");
		add(b3, BorderLayout.EAST);
                b3.addActionListener(this);

                Button b4 = new Button("Stanga");
		add(b4, BorderLayout.WEST);
                b4.addActionListener(this);
		//deci ascultatorul este instanta curenta: this
	}


        public void actionPerformed(ActionEvent e)
        {
           
                String command = e.getActionCommand();
		//numele comenzii este numele butonului apasat
		System.out.println(e.toString());

                if (command.equals("Start"))
                {
                    Hashtable h=new Query("start(R)").oneSolution();
                    System.out.println(h.get("R"));
                    x=1;
                }

                if (command.equals("Sus"))
                {
                        if(x==0)
                            System.out.println("Apasa intai Start pentru a incepe jocul !");
                        else
                        {
                            Hashtable h=new Query("sus(R)").oneSolution();
                            System.out.println(h.get("R"));
                        }
                }
			
		else
		if (command.equals("Jos"))
                {
                    Hashtable lc = new Query("loc(X,Y)").oneSolution();
                    String tl=lc.get("X").toString()+lc.get("Y").toString();

                        if(x==0)
                            System.out.println("Apasa intai Start pentru a incepe jocul !");
                        else
                        if(tl=="77")
                            System.out.println("Jocul s-a terminat! Apasa Start pentru a incepe din nou !");
                        else
                        {
                            Hashtable h=new Query("jos(R)").oneSolution();
                            System.out.println(h.get("R"));
                        }
                }
               	else
		if (command.equals("Dreapta"))
                {
                        if(x==0)
                            System.out.println("Apasa intai Start pentru a incepe jocul !");
                        else
                        {
                            Hashtable h=new Query("dreapta(R)").oneSolution();
                            System.out.println(h.get("R"));
                        }
                }
                else
		if (command.equals("Stanga"))
                {
                        if(x==0)
                            System.out.println("Apasa intai Start pentru a incepe jocul !");
                        else
                        {
                            Hashtable h=new Query("stanga(R)").oneSolution();
                            System.out.println(h.get("R"));
                        }
                }
                else
                if (command.equals("Mazehelp"))
                {
                    Hashtable h=new Query("mazehelp").oneSolution();
                    
                }
	}
}

