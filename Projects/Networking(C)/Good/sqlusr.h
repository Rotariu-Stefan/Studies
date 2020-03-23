#include <mysql.h>
#include <stdio.h>
#include <stdlib.h>
MYSQL_ROW row;

/* VERIFICA DACA UN USER EXISTA SAU NU (RETURNEAZA 1 DACA EXISTA SI 0 DACA NU EXISTA) */
int check_usr (MYSQL *conn2, char user[16])
	{
	  char cmd[128];
	  MYSQL_RES *result;
	  
	  /* COMPUNEREA COMENZII */
	  sprintf(cmd,"SELECT username FROM users WHERE username='%s';",user);
	  printf("\nSQL>%d>> %s\n",strlen(cmd),cmd);

	  if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
			{
      			fprintf(stderr, "%s\n", mysql_error(conn2));
      			exit(0);
   			}
	 result = mysql_use_result(conn2);
	 if ((row = mysql_fetch_row(result)) == NULL)
		{
		 return 0;
		}
	 else 
		{
		 mysql_free_result(result);
		 return 1;
		}
	}


/* ADAUGA UN USER DACA NU EXISTA, RETURNAND 1 LA SUCCES SI 0 LA ESUARE */
int add_usr (MYSQL *conn2, char user[16],char pass[8])
	{
	  char cmd[128];
	  MYSQL_RES *result;
	
	  /* COMPUNEREA COMENZII */
	  sprintf(cmd,"INSERT INTO users VALUES (0,'%s','%s');",user,pass);
	  printf("\nSQL>%d>> %s\n",strlen(cmd),cmd);
	 
	  /* EXECUTAREA QUERY-ULUI */
	  if (!check_usr(conn2,user))
		{
		 if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
			{
      		fprintf(stderr, "%s\n", mysql_error(conn2));
      		exit(0);
   			}
		 //////mysql_free_result(result);
		 return 1;
		}
	  else 
		{
		 return 0;
		}
	}


/* STERGE UN USER DACA EXISTA, RETURNAND 1 LA REUSITA SI 0 LA ESEC */
int del_usr (MYSQL *conn2, char user[16])
	{
	 char cmd[128];
     MYSQL_RES *result;
	 
	 /* COMPUNEREA COMENZII */
	 sprintf(cmd,"DELETE FROM users WHERE username='%s';",user);
	 printf("\nSQL>%d>> %s\n",strlen(cmd),cmd);

	 if (check_usr(conn2,user))
		{
	 	 if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
				{
      				 fprintf(stderr, "%s\n", mysql_error(conn2));
      				 exit(0);
   				}
	 	 return 1;
		}
	else 
		{
		return 0;
		}
	}
	
int login(MYSQL *conn2, char user[16], char pass[8])
	{
	  char cmd[128];
	  MYSQL_RES *result;
	  
	  /* COMPUNEREA COMENZII */
	  sprintf(cmd,"SELECT username FROM users WHERE username='%s' AND password='%s';",user,pass);
	  printf("\nSQL>%d>> %s\n",strlen(cmd),cmd);

	  if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
			{
      			fprintf(stderr, "%s\n", mysql_error(conn2));
      			exit(0);
   			}
	 result = mysql_use_result(conn2);

	 if ((row = mysql_fetch_row(result)) == NULL)
		{
		 return 0;
		}
	 else 
		{
		 mysql_free_result(result);
		 return 1;
		}
	}
	
/* RETURNEAZA TOTI USERII */
MYSQL_RES * list_usr (MYSQL *conn2)
	{
	 char cmd[128];
	 MYSQL_RES *result;

	 /* COMPUNEREA COMENZII */
	 strcpy(cmd,"SELECT cod_u,username FROM users WHERE 1='1';");
	 printf("\nSQL>> %s\n",cmd);
	
	 /* send SQL query */
   	if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
		{
      		 fprintf(stderr, "%s\n", mysql_error(conn2));
      		 exit(1);
   		}
	return mysql_use_result(conn2);
	}
	
char * return_usr_list(MYSQL_RES * result)
	{	
	char buf[1000];
	bzero(buf,1000);

   	while ((row = mysql_fetch_row(result)) != NULL)
      		{
		strcat(buf,row[0]);
		strcat(buf," ");
		strcat(buf,row[1]);
		strcat(buf,"\n");
		}
	buf[strlen(buf)]='\0';
	mysql_free_result(result);
	return buf;
	}
	
MYSQL_RES * usr_id (MYSQL *conn2,char username[16])
	{
	 char cmd[128];
	 MYSQL_RES *result;

	 /* COMPUNEREA COMENZII */
	 sprintf(cmd,"SELECT cod_u FROM users WHERE username='%s';",username);
	 printf("\nSQL>> %s\n",cmd);
	
	 /* send SQL query */
   	if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
		{
      		 fprintf(stderr, "%s\n", mysql_error(conn2));
      		 exit(1);
   		}
	return mysql_use_result(conn2);
	}
	
char * return_usr_id(MYSQL_RES * result)
	{	
	char buf[1000];
	bzero(buf,1000);

   	while ((row = mysql_fetch_row(result)) != NULL)
      		{
		strcat(buf,row[0]);
		strcat(buf,"\n");
		}
	buf[strlen(buf)]='\0';
	//mysql_free_result(result);
	return buf;
	}


