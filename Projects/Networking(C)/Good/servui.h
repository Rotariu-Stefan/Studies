#include <stdio.h>
#include <time.h>
#include <termios.h>
#include <unistd.h>

/* PORTUL SI STAREA SERVERULUI */
int PORT=3333;
void change_port ();
void redraw()
	{
	 system("clear");
	 time_t t = time(0);
	 printf("%s\n",ctime(&t));	
	 printf("WELCOME!\n");
	 printf("========\n");
	 printf("\n* Application Repository Server ver. 1.1 * \n");
	 printf("\n  Server's default port is 3333 \n"); 
	 
	}

void main_menu()
	{
	 printf("\nMENU:\n");
	 printf("======\n");
	 printf("\t1. Change port\n");
	 printf("\t2. Procede with this settings\n");
	 int c;
	 scanf("%d",&c);
	 switch (c)
		{
		 case 1: change_port(); printf("\n\tServer port: %d",PORT); break;
		 case 2: redraw(); printf("\n\tGoing on default settings. \n\tServer port: %d",PORT); break;
		}
	printf("\n\n ========================================== \n");
	}

void change_port ()
	{
	 printf("\tNew port number: ");
	 int new_port;
	 scanf("%d",&new_port);
	 PORT=new_port;
	 redraw();
	}