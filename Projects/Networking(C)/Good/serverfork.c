#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <unistd.h>
#include <errno.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>
#include <errno.h>
#include <signal.h>
#include <stdio.h>
#include <time.h>
#include <mysql.h>

#include "transfer.h"
#include "sqlusr.h"
#include "sqlapp.h"
#include "servui.h"


/* IDENTIFICATORII PROPRIETARULUI FIULUI */
char usercode[3];
char username[16];
char password2[8];
char protcall[8];
char filename[32];
char specname[32];
//char tempname[16];

/*                FOR MySql Medium                   */
/* ================================================= */

MYSQL *conn;
char *server2 = "localhost";
char *user = "APPREP2";
char *password = "txoBKKCEpF";
char *database = "APPREP2";


int add_usr (MYSQL *conn2, char user[16], char pass[8]);
int check_usr (MYSQL *conn2, char user[16]);
int del_usr (MYSQL *conn2, char user[16]);
int add_app (MYSQL *conn2,char cod_s[3],char cod_user[3],char name[32],char prod[32],char status[32],char sname[32], char fname[32]);
int check_app (MYSQL *conn2, char app[32]);
int del_app (MYSQL *conn2, char app[32]);
MYSQL_RES * find_app (MYSQL *conn2,char field2[32],char value[32]);
char * return_find(MYSQL_RES * result);
void treatclient(int socket,MYSQL *conn2);

int main(int argc, char *argv[])
{
    
     int sockfd, newsockfd, portno, clilen;
     char buffer[256];
     struct sockaddr_in serv_addr, cli_addr;
     int n,pid;
	 conn = mysql_init(NULL);
     redraw();
	 main_menu();

     sockfd = socket(AF_INET, SOCK_STREAM, 0);
     if (sockfd < 0) error("ERROR opening socket");
    
     bzero((char *) &serv_addr, sizeof(serv_addr));
     serv_addr.sin_family = AF_INET;
     serv_addr.sin_addr.s_addr = htonl(INADDR_ANY);
     serv_addr.sin_port = htons(PORT);
     
	 /* CONECTARE LA BAZA DE DATE */
	 if (!mysql_real_connect(conn, server2,user, password, database, 0, NULL, 0)) 
		{
      		 fprintf(stderr, "%s\n", mysql_error(conn));
      		 exit(1);
   		}
		
	 if (bind(sockfd, (struct sockaddr *) &serv_addr, sizeof(serv_addr)) < 0) 
              error("ERROR on binding");
     
     
	 listen(sockfd,5);
     clilen = sizeof(cli_addr);
     
	 while(1)
     {
     newsockfd = accept(sockfd,(struct sockaddr *) &cli_addr,&clilen);
     if (newsockfd < 0) 
          error("ERROR on accept");
     
     pid = fork();
     if (pid == 0) 
     {
        treatclient(newsockfd,conn);
		mysql_close(conn);
     }	 
     }
     return 0; 
}


void treatclient(int socket,MYSQL *conn2)
	{
	 bzero(protcall,8);
	 bzero(username,16);
	 bzero(password2,8);
	 int RUN=1;
	 int LOGG=1;
	 while (RUN)
		{
		 if (read(socket,protcall,sizeof(protcall))<0) {printf("Eroare la citirea protocolului"); bzero(protcall,8); RUN=0;}
		 
		 /* ANALIZA LOGME */
		 if (strcmp(protcall,"logme")==0)
			do
			{
				printf("\nSemnalul: logme s-a analizat\n");
				bzero(username,16);
				bzero(password2,8);
				if(read(socket,username,sizeof(username))<0) LOGG=0;
				if(read(socket,password2,sizeof(password2))<0) LOGG=0;
				if((strcmp(username,"")!=0) && (strcmp(password2,"")!=0))
					if (login(conn2,username,password2))
							{
							strcpy(usercode,return_usr_id(usr_id (conn2,username)));
							write(socket,"ok",sizeof("ok"));
							LOGG=0;
							}
					else 
							{
							write(socket,"no",sizeof("no"));
							LOGG=1;
							}
			}
			while(LOGG);
		 
		 /* ANALIZA REGME */
		 if (strcmp(protcall,"regme")==0)
			{	
				printf("\nSemnalul: regme s-a analizat\n");
				bzero(username,16);
				bzero(password2,8);
				if(read(socket,username,sizeof(username))<0) printf("Eroare la citire reg usr");
				if(read(socket,password2,sizeof(password2))<0) printf("Eroare la citire reg pas");
				if((strcmp(username,"")!=0) && (strcmp(password2,"")!=0))
					if (add_usr(conn2,username,password2)) write(socket,"ok",sizeof("ok"));
					else write(socket,"no",sizeof("no"));
			}
			
		/* ANALIZA APPLIST */
		if (strcmp(protcall,"applist")==0)
			{
			printf("\nSemnalul: applist s-a analizat\n");
			char buffer[1000];
			bzero(buffer,sizeof(buffer));
			sprintf(buffer,"%s",return_find(find_app (conn2,"1","1")));
			if(write(socket,buffer,sizeof(buffer))<0) {printf("Eroare la scrierea tut app");}
			}
			
		/* ANALIZA USRLIST */
		if (strcmp(protcall,"usrlist")==0)
			{
			printf("\nSemnalul: usrlist s-a analizat\n");
			char buffer[1000];
			bzero(buffer,sizeof(buffer));
			sprintf(buffer,"%s",return_usr_list(list_usr(conn2)));
			if(write(socket,buffer,sizeof(buffer))<0) {printf("Eroare la scrierea tut app");}
			}
			
		/* ANALIZA USRDEL */
		 if (strcmp(protcall,"usrdel")==0)
			{
			    char tempname[16];
				bzero(tempname,16);
				int a;
				printf("\nSemnalul: usrdel s-a analizat\n");
				if(read(socket,tempname,sizeof(tempname))<0) {printf("Eroare la citire tempname");}
				printf(">>%s",tempname);
				if(strcmp(tempname,"")!=0) 
					if ((a=del_usr(conn2,tempname))) 
						if(write(socket,"ok",sizeof("ok"))<0) {printf("Eroare la scrierea ok");}
					else 
						if(write(socket,"no",sizeof("no"))<0) {printf("Eroare la scrierea no");}
			}
		
		/* ANALIZA SRCH */
		if (strcmp(protcall,"srch")==0)
			{
			
			char buffer[1000];
			char value[32];
			char field[32];
			bzero(buffer,sizeof(buffer));
			bzero(value,32);
			bzero(field,32);
			printf("\nSemnalul: srch s-a analizat\n");
			if(read(socket,field,sizeof(field))<0) printf("Eroare la citire field");
			if(read(socket,value,sizeof(value))<0) printf("Eroare la citire value");
			printf("\n>>%s",field);
			printf("\n>>%s",value);
			sprintf(buffer,"%s",return_find(find_app(conn2,field,value)));
				printf("\n>>%s",buffer);
			if(write(socket,buffer,sizeof(buffer))<0) {printf("Eroare la scrierea tut app");}
		
			}
		
		/* ANALIZA addapp */
		if (strcmp(protcall,"addapp")==0)
			{
			char name[32];
			char prod[32];
			char status[32];
			char sname[32];
			char fname[32];
			bzero(specname,32);
			bzero(filename,32);
						
			bzero(name,32);
			if(read(socket,name,sizeof(name))<0) printf("Eroare la citire field");
			printf("\n>name>%s",name);
			
			bzero(prod,32);
			if(read(socket,prod,sizeof(prod))<0) printf("Eroare la citire field");
			printf("\n>prod>%s",prod);
			
			bzero(status,32);
			if(read(socket,status,sizeof(status))<0) printf("Eroare la citire field");
			printf("\n>status>%s",status);
			
			bzero(sname,32);
			if(read(socket,fname,sizeof(fname))<0) printf("Eroare la citire field");
			printf("\n>fname>%s",fname);
			strcpy(filename,fname);
			
			bzero(sname,32);
			strncpy(sname,fname,strlen(fname)-3);
			strcat(sname,"txt");
			
			strcpy(usercode,return_usr_id(usr_id (conn2,username)));
			printf("%s",usercode);
			if(add_app (conn2,"0",usercode,name,prod,status,sname,fname)) 
				if(write(socket,"o",sizeof("o"))<0) {printf("Eroare la scrierea ok");}
			else 
				if(write(socket,"n",sizeof("n"))<0) {printf("Eroare la scrierea no");}
			}
			
			/* ANALIZA sndapp */
			if (strcmp(protcall,"sndapp")==0)
			{
			printf("\nSemnalul: sndapp s-a analizat\n");
			if (receive_file(socket,filename)>0) 
				if(write(socket,"o",sizeof("o"))<0) {printf("Eroare la scrierea ok");}
			else
				if(write(socket,"n",sizeof("n"))<0) {printf("Eroare la scrierea no");}
			
			}
			
		
		/* ANALIZA appdela */
		 if (strcmp(protcall,"appdela")==0)
			{
				char fname[32];
			    char tempname[16];
				int a;
				printf("\nSemnalul: appdela s-a analizat\n");
				if(read(socket,tempname,sizeof(tempname))<0) {printf("Eroare la citire tempname");}
				strcpy(fname,return_dld(find_app(conn2,"name",tempname)));
				if(strcmp(tempname,"")!=0) 
					if ((a=del_app(conn2,tempname))) 
						{
						 if(write(socket,"ok",sizeof("ok"))<0) {printf("Eroare la scrierea ok");}
						 remove(fname);
						 }
					else 
						if(write(socket,"no",sizeof("no"))<0) {printf("Eroare la scrierea no");}
			}
			
			/* ANALIZA appdel */
		 if (strcmp(protcall,"appdel")==0)
			{
				char fname[32];
			    char tempname[16];
				int a;
				printf("\nSemnalul: appdel s-a analizat\n");
				if(read(socket,tempname,sizeof(tempname))<0) {printf("Eroare la citire tempname");}
				strcpy(fname,return_dld(find_app(conn2,"name",tempname)));
				if(strcmp(tempname,"")!=0) 
					if ((a=del_app_usr(conn2,tempname,usercode))) 
						{
						 if(write(socket,"ok",sizeof("ok"))<0) {printf("Eroare la scrierea ok");}
						 remove(fname);
						 }
					else 
						if(write(socket,"no",sizeof("no"))<0) {printf("Eroare la scrierea no");}
						
			}
		
			if (strcmp(protcall,"dldapp")==0)
			{
			printf("\nSemnalul: dldapp s-a analizat\n");
			char name[32];
			char fname[32];
			bzero(name,32);
			if(read(socket,name,sizeof(name))<0) {printf("Eroare la citire tempname");}
			printf("\n>>name: %s",name);
			if (check_app (conn2,name))
				{
				 bzero(filename,sizeof(filename));
				 strcpy(filename,return_dld(find_app(conn2,"name",name)));
				 if(write(socket,filename,sizeof(filename))<0) {printf("Eroare la scrierea no");}
				 printf("\n>>filename: %s,%d",filename,strlen(filename));
				 if (send_file(socket,filename)>0) printf("\nSent item");
				 bzero(filename,sizeof(filename));
				 }
			
			
			}
			
			
		/* ANALIZA EXIT */
		if (strcmp(protcall,"exit")==0)
			{
			 printf("\nSemnalul: exit s-a analizat\n");
			 close(socket);
			 kill(getpid(), 9);
			}
		bzero(protcall,8);
		}
	}