#include <stdio.h>
#include <time.h>
#include <termios.h>
#include <unistd.h>
#include <string.h>

void del_usr ();
void list_usr ();
void app_list_all();
void menu_admin();
void menu_opt();
void exit_menu();
void help_menu();
void reg_menu();
void log_menu();
void main_menu();
void redraw();
void search_app(); 
void add_app();
void del_app ();
void dld_app();

/* PORTUL SI STAREA SERVERULUI */
int PORT=3333;
int ADMIN=0;
int sendsucc=0;

extern int sd;

void redraw()
	{
	 system("clear");
	 time_t t = time(0);
	 printf("%s\n",ctime(&t));	
	 printf("WELCOME!\n");
	 printf("========\n");
	 printf("\n* Application Repository Client ver. 1.1 * \n");
	}

void main_menu()
	{
	 redraw();
	 printf("\nSTART MENU\n==========\n\n");
	 printf("\t1.LOGIN\n\t2.REGISTER\n\t3.HELP\n\t4.EXIT\n\n");	 
	 int opt=0;
	 printf(">> ");
	 scanf("%d",&opt);
	 
	 switch (opt)
		{
			case 1: log_menu(); break;
			case 2: reg_menu(); break;
			case 3: help_menu(); break;
			case 4: exit_menu(); break;
			default:main_menu(); break; break;
		}
	}
	
void log_menu()
	{
	 char username[16];
	 char password[8];
	 char protcall[8];
	 int login=1;
	 bzero(protcall,8);
	 bzero(username,16);
	 bzero(password,8);
	 write(sd,"logme",sizeof("logme"));
	 do
		{
		redraw();
		printf("\nLOGIN MENU\n==========\n\n");
		printf("\nUsername: ");
		scanf("%s",username);
		printf("Password: ");
		scanf("%s",password);
		write(sd,username,sizeof(username));
		write(sd,password,sizeof(password));
		read(sd,protcall,sizeof(protcall));
		}
	 while (strcmp(protcall,"ok")!=0);
	 if (strcmp(username,"admin")==0) {ADMIN=1; menu_admin();}
	 else
		{
		 ADMIN=0; 
		 menu_opt();
		 }
	}

void reg_menu()
	{
	 char username[16];
	 char password[8];
	 char protcall[8];
	 bzero(protcall,8);
	 bzero(username,16);
	 bzero(password,8);
	 redraw();
	 printf("\nREGISTER MENU\n=============\n\n");
	 printf("* ADD A NEW USER IN THE SYSTEM *\n");
	 int opt=0;
	 printf("\t1.Add user\n\t2.Back\n");
	 printf(">> ");
	 scanf("%d",&opt);
	 
	 switch (opt)
		{
			case 1 : break;
			case 2 : main_menu(); break;
		}
		
	 redraw();
	 printf("\nREGISTER MENU\n=============\n\n");
	 printf("* ADD A NEW USER IN THE SYSTEM *\n");
	 printf("\nUsername: ");
	 scanf("%s",username);
	 printf("Password: ");
	 scanf("%s",password);
	 if (write(sd,"regme",sizeof("regme"))<0) { printf("eroare la write 'regme'");}
		if (write(sd,username,sizeof(username))<0) { printf("eroare la write 'username'");}
		if (write(sd,password,sizeof(password))<0) { printf("eroare la write 'password'");}
	if (read(sd,protcall,sizeof(protcall))<0) { printf("eroare la write 'protcall'");}
	if (strcmp(protcall,"ok")==0) printf("\n***** USER ADDED *****");
	else 
		if (strcmp(protcall,"no")==0) printf("\n***** USER EXISTS *****");
	printf("\n\t1.Back\n");
	printf(">> ");
	scanf("%s",&opt);
	if (opt==1) main_menu();
	else main_menu();
	}
	
void help_menu()
	{
	 redraw();
	 printf("\nHELP MENU\n=========\n\n");
	 sleep(5);
	 main_menu();
	 //meniu_opt();
	}
	
void exit_menu()
	{
	 write(sd,"exit",sizeof("exit"));
	 printf("\nClient terminated...\n");	 
	 exit(0);
	 }
	 
void menu_opt()
	{
	 redraw();
	 printf("\nMENU\n====\n\n");
	 printf("\t1.Aplication list\n\t2.Search application\n\t3.Add application\n\t4.Delete application\n\t5.Download application\n\t6.Back\n\n");	 
	 int opt=0;
	 printf(">> ");
	 scanf("%d",&opt);
	 
	 switch (opt)
		{
			case 1: app_list_all(); break;
			case 2: search_app(); break;  
			case 3: add_app();  break;
			case 4: del_app(); break;
			case 5: dld_app(); break;
			case 6: ADMIN=0; main_menu(); break;
			default:ADMIN=0; menu_opt(); break; break;
		}
	}
	
void app_list_all()
	{
	 redraw();
	 printf("\nLIST ALL APPLICATIONS\n=====================\n\n");
	 printf("|INDEX APL | COD USER | NAME | PRODUCER | FNAME |\n");
	 printf("=================================================\n");
	 char buffer[1000];
	 bzero(buffer,sizeof(buffer));
	 if (write(sd,"applist",sizeof("applist"))<0) { printf("eroare la write 'applist'");}
	 if (read(sd,buffer,sizeof(buffer))<0) { printf("eroare la read buffer");}
	 printf("%s",buffer);
	 int opt;
	 printf("\n\n\t1.Back\n");
	 printf(">> ");
	 scanf("%s",&opt);
	 if (opt==1) 
		if(ADMIN==1) menu_admin();
		else menu_opt();
	 else if(ADMIN==1) menu_admin();
		  else if (ADMIN==0) menu_opt();
	}
	
void menu_admin()
	{
	 redraw();
	 printf("\nMENU ADMIN\n==========\n\n");
	 printf("\t1.Aplication list\n\t2.Search application\n\t3.Add application\n\t4.Download application\n\t5.List all users\n\t6.Delete user\n\t7.Delete application\n\t8.Back\n\n");	 
	 int opt=0;
	 printf(">> ");
	 scanf("%d",&opt);
	 
	 switch (opt)
		{
			case 1: app_list_all(); break;
			case 2: search_app(); break; 
			case 3: add_app(); break; 
			case 4: dld_app(); break;
			case 5: list_usr(); break;
			case 6: del_usr();  break;
			case 7: del_app(); break;
			case 8: ADMIN=0; main_menu(); break;  
			default:menu_admin(); break; break;
		}
	}
	
void list_usr ()
	{
	 redraw();
	 printf("\nLIST ALL USERS\n=====================\n\n");
	 printf("| COD INDEX | USERNAME |\n");
	 printf("========================\n");
	 char buffer[1000];
	 bzero(buffer,sizeof(buffer));
	 if (write(sd,"usrlist",sizeof("usrlist"))<0) { printf("eroare la write 'usrlist'");}
	 if (read(sd,buffer,sizeof(buffer))<0) { printf("eroare la read buffer");}
	 printf("%s",buffer);
	 int opt;
	 printf("\n\n\t1.Back\n");
	 printf(">> ");
	 scanf("%s",&opt);
	 if (opt==1) 
		if(ADMIN==1) menu_admin();
		else menu_opt();
	 else if(ADMIN==1) menu_admin();
		  else menu_opt();
	}
	
void del_usr ()
	{
	 char username[16];
	 char protcall[8];
	 bzero(protcall,8);
	 bzero(username,16);
	 redraw();
	 printf("\nDELETE USER\n===========\n\n");
	 printf("\nUsername: ");
	 scanf("%s",username);
	 if (write(sd,"usrdel",sizeof("usrdel"))<0) printf("eroare la write usrdel");
	 if (write(sd,username,sizeof(username))<0) printf("eroare la write 'username'");
	 if (read(sd,protcall,sizeof(protcall))<0) printf("eroare la read 'protcall'");
	 if (strcmp(protcall,"ok")==0) printf("\n***** USER DELETED *****");
	 else 
			if (strcmp(protcall,"no")==0) printf("\n***** FAILED DELETING USER *****");
	 printf("\n\t1.Back\n");
	 int opt;
	 printf(">> ");
	 scanf("%s",&opt);
	 if (opt==1) {bzero(protcall,8); menu_admin();}
	 else {bzero(protcall,8); menu_admin();}
	}
	
void search_app ()
	{
	 char value[32];
	 char field[32];
	 char buffer[1000];
	 bzero(value,32);
	 bzero(field,32);
	 bzero(buffer,1000);
	 int opt;
	 redraw();
	 printf("\nSEARCH AN APPLICATION BY FIELD AND VALUE\n========================================\n\n");
	 printf("\nValue of search: ");
	 scanf("%s",value);
	 printf("\nSelect field of search:\n");
	 printf("\n\t1.Name\n\t2.Producer\n\t3.Status");
	 printf("\n>> ");
	 scanf("%d",&opt);
	 if (write(sd,"srch",sizeof("srch"))<0) printf("eroare la write srch");
	 if (opt==1) strcpy(field,"name");
	 else
		if (opt==2) strcpy(field,"prod");
		else 
			if (opt==3) strcpy(field,"status");
	 if (write(sd,field,sizeof(field))<0) printf("eroare la write name");
	 if (write(sd,value,sizeof(value))<0) printf("eroare la write value");
	 if (read(sd,buffer,sizeof(buffer))<0) { printf("eroare la read buffer");}
	 printf("\n|INDEX APL | COD USER | NAME | PRODUCER | FNAME |\n");
	 printf("=================================================\n");
	 printf("\n%s",buffer);
	 printf("\n\t======");
 	 printf("\n\t1.Back\n");
	 printf(">> ");
	 scanf("%s",&opt);
	 if (opt==1) 
		if(ADMIN==1) menu_admin();
		else menu_opt();
	 else if(ADMIN==1) menu_admin();
		  else if (ADMIN==0) menu_opt();
	}
	
void add_app()
	{
	int succeded=0;
    sendsucc=0;
	char name[32];
	char prod[32];
	char status[32];
	char sname[32];
	char fname[32];
	char protcall[8];

	redraw();
	printf("\nADD A NEW APPLICATION TO REPOSITORY\n===================================\n\n");
	
	bzero(name,32);
	printf("\nApplication name: ");
	scanf("%s",name);
	
	bzero(prod,32);
	printf("Producer name: ");
	scanf("%s",prod);
	
	bzero(status,32);
	printf("Application status: ");
	scanf("%s",status);
	
	bzero(fname,32);
	printf("Application file name: ");
	scanf("%s",fname);
	
	/* Generarea numelui fisierului de specificatii */
	if (write(sd,"addapp",sizeof("addapp"))<0) printf("eroare la write addapp");
		if (write(sd,name,sizeof(name))<0) printf("eroare la write 'username'");
		if (write(sd,prod,sizeof(prod))<0) printf("eroare la write 'username'");
		if (write(sd,status,sizeof(status))<0) printf("eroare la write 'username'");
		if (write(sd,fname,sizeof(fname))<0) printf("eroare la write 'username'");
	if (read(sd,protcall,sizeof(protcall))<0) printf("eroare la read 'protcall'");
	if (strcmp(protcall,"o")==0) succeded=1;
	else 
		if (strcmp(protcall,"n")==0) succeded=0;
	sendsucc=send_app(fname);
	if (succeded)  printf("\n***** APPLICATION ADDED *****");
	else 
		printf("\n***** APPLIICATION ADDING FAILED  *****");
	
  	 int opt;
	 printf("\n\n\t1.Back\n");
	 printf(">> ");
	 scanf("%s",&opt);
	 if (opt==1) 
		if(ADMIN==1) menu_admin();
		else menu_opt();
	 else if(ADMIN==1) menu_admin();
		  else menu_opt();
	}
	
void del_app ()
	{
	 char name[32];
	 char protcall[8];
	 bzero(protcall,8);
	 redraw();
	 printf("\nDELETE APLICATION\n=================\n\n");
	 if (ADMIN==1)
		{
		if (write(sd,"appdela",sizeof("appdela"))<0) printf("eroare la write appdela");
		printf("\nApplication name: ");
		scanf("%s",name);
		if (write(sd,name,sizeof(name))<0) printf("eroare la write 'username'");
		if (read(sd,protcall,sizeof(protcall))<0) printf("eroare la read 'protcall'");
		if (strcmp(protcall,"ok")==0) printf("\n***** APPLICATION DELETED *****");
		else 
			if (strcmp(protcall,"no")==0) printf("\n***** FAILED DELETING APPLICATION *****");
		printf("\n\t1.Back\n");
		int opt;
		printf(">> ");
		scanf("%s",&opt);
		if (opt==1) {bzero(protcall,8); menu_admin();}
		else {bzero(protcall,8); menu_admin();}
		}
	 else
		if (ADMIN==0)
			{
			if (write(sd,"appdel",sizeof("appdel"))<0) printf("eroare la write appdela");
			printf("\nApplication name: ");
			scanf("%s",name);
			if (write(sd,name,sizeof(name))<0) printf("eroare la write 'username'");
			if (read(sd,protcall,sizeof(protcall))<0) printf("eroare la read 'protcall'");
			if (strcmp(protcall,"ok")==0) printf("\n***** APPLICATION DELETED *****");
			else 
				if (strcmp(protcall,"no")==0) printf("\n***** FAILED DELETING (NOT THE OWNER!) *****");
			printf("\n\t1.Back\n");
			int opt;
			printf(">> ");
			scanf("%s",&opt);
			if (opt==1) {bzero(protcall,8); menu_admin();}
			else {bzero(protcall,8); menu_admin();}
			}
	}
	
int send_app(char fname[32])
	{
	char protcall[8];
	if (write(sd,"sndapp",sizeof("sndapp"))<0) printf("eroare la write sndapp");
	send_file(sd,fname);
	if (read(sd,protcall,sizeof(protcall))<0) printf("eroare la read 'protcall'");
	if (strcmp(protcall,"o")==0) sendsucc=1;
	else 
		if (strcmp(protcall,"n")==0) sendsucc=0;
	}

	
void dld_app()
	{
	char name[32];
	char fname[32];
	char protcall[8];
	int succes=0;
	redraw();
	printf("\nDOWNLOAD APLICATION\n===================\n\n");
	
	bzero(name,32);
	printf("\nApplication name: ");
	scanf("%s",name);
	if (write(sd,"dldapp",sizeof("dldapp"))<0) printf("eroare la write appdela");
	if (write(sd,name,sizeof(name))<0) printf("eroare la write appdela");
	if (read(sd,fname,sizeof(fname))<0) printf("eroare la read 'protcall'");
	if (receive_file(sd,fname)>0) printf("\n***** APPLICATION DOWNLOADED *****");
	else printf("\n***** APPLICATION DOWNLOADING FAILED *****");
	rename("temp.tmp",fname);

	printf("\n\t1.Back\n");
	int opt;
	printf(">> ");
	scanf("%s",&opt);
	if (opt==1) 
		if(ADMIN==1) menu_admin();
		else menu_opt();
	 else if(ADMIN==1) menu_admin();
		  else menu_opt();
	}
	