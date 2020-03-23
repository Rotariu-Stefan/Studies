#include <mysql.h>
#include <stdio.h>
#include <stdlib.h>
MYSQL_ROW row;

/* VERIFICA DACA O APLICATIE EXISTA SAU NU (RETURNEAZA 1 DACA EXISTA SI 0 DACA NU EXISTA) */
int check_app (MYSQL *conn2, char app[32])
	{
	  char cmd[128];
	  MYSQL_RES *result;

	  /* COMPUNEREA COMENZII */
	  sprintf(cmd,"SELECT name FROM apl WHERE name='%s';",app);
	  printf("\nSQL>> %s\n",cmd);

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

/* ADAUGA O APLICATIE DACA NU EXISTA, RETURNAND 1 LA SUCCES SI 0 LA ESUARE */
int add_app (MYSQL *conn2,char cod_s[3],char cod_user[3],char name[32],char prod[32],char status[32],char sname[32], char fname[32])
	{
	  char cmd[254];
	
	  MYSQL_RES *result;
	  bzero(cmd,254);
	  /* COMPUNEREA COMENZII */
	  sprintf(cmd,"INSERT INTO apl VALUES(%s,%s,'%s','%s','%s','%s','%s');",cod_s,cod_user,name,prod,status,sname,fname);
	  printf("\nSQL>> %s\n",cmd);

	  /* EXECUTAREA QUERY-ULUI */
	  if (!check_app(conn2,name))
		{
		 if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd)))
			{
      			fprintf(stderr, "%s\n", mysql_error(conn2));
				exit(0);
   			}
		 //mysql_free_result(result);
		 return 1;
		}
	  else 
		{
		 return 0;
		}
	}



/* STERGE O APLICATIE DACA EXISTA, RETURNAND 1 LA REUSITA SI 0 LA ESEC */
int del_app (MYSQL *conn2, char app[32])
	{
	 char cmd[128];
        MYSQL_RES *result;
	 
	 /* COMPUNEREA COMENZII */
	 sprintf(cmd,"DELETE FROM apl WHERE name='%s';",app);
	 printf("\nSQL>> %s\n",cmd);
	 if (check_app(conn2,app))
		{
	 	 if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
				{
      				 fprintf(stderr, "%s\n", mysql_error(conn2));
      				 exit(0);
   				}
		 mysql_commit(conn2);
	 	 return 1;
		}
	else return 0;
	}

/* CAUTA O APLICATIE DUPA CAMPUL SI CRITERIUL SPECIFICAT */
MYSQL_RES * find_app (MYSQL *conn2,char field2[32],char value[32])
	{
	 char cmd[128];
	 MYSQL_RES *result;

	 /* COMPUNEREA COMENZII */
	 sprintf(cmd,"SELECT * FROM apl WHERE %s='%s';",field2,value);
	 printf("\nSQL>> %s\n",cmd);
	
	 /* send SQL query */
   	if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
		{
      		 fprintf(stderr, "%s\n", mysql_error(conn2));
      		 exit(1);
   		}
	return mysql_use_result(conn2);
	}


/* RETURNEAZA UN SIR DE CARACTERE FORMATAT CU RASPUNSUL SELECTIEI */
char * return_find(MYSQL_RES * result)
	{	
	char buf[1000];
	bzero(buf,1000);

   	while ((row = mysql_fetch_row(result)) != NULL)
      		{
		strcat(buf,row[0]);
		strcat(buf," ");
		strcat(buf,row[1]);
		strcat(buf," ");
		strcat(buf,row[2]);
		strcat(buf," ");
		strcat(buf,row[3]);
		strcat(buf," ");
		strcat(buf,row[4]);
		strcat(buf," ");
		strcat(buf,row[6]);
		strcat(buf,"\n");
		}
	buf[strlen(buf)]='\0';
	//mysql_free_result(result);
	return buf;
	}
	
/* STERGE O APLICATIE CU USER SPECIFIC DACA EXISTA, RETURNAND 1 LA REUSITA SI 0 LA ESEC */
int del_app_usr (MYSQL *conn2, char app[32], char usercode[3])
	{
	 char cmd[128];
        MYSQL_RES *result;
	 
	 /* COMPUNEREA COMENZII */
	 sprintf(cmd,"DELETE FROM apl WHERE name='%s' AND cod_u='%s';",app,usercode);
	 printf("\nSQL>> %s\n",cmd);
	 if (check_app(conn2,app))
		{
	 	 if (mysql_real_query(conn2,cmd,(unsigned int) strlen(cmd))) 
				{
      				 fprintf(stderr, "%s\n", mysql_error(conn2));
      				 exit(0);
   				}
		 mysql_commit(conn2);
	 	 return 1;
		}
	else return 0;
	}
	
char * return_dld(MYSQL_RES * result)
	{	
	char buf[1000];
	bzero(buf,1000);

   	while ((row = mysql_fetch_row(result)) != NULL)
      		{
			strcpy(buf,row[6]);
			}
	//mysql_free_result(result);
	return buf;
	}
	

