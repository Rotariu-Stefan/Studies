/* Simple C program that connects to MySQL Database server*/
#include <mysql.h>
#include <stdio.h>
#include <stdlib.h>

main() {
   MYSQL *conn;
   MYSQL_RES *res;
   MYSQL_ROW row;

   char *server = "localhost";
   char *user = "APPREP";
   char *password = "de2s7x3xzX"; /* set me first */
   char *database = "APPREP";

   conn = mysql_init(NULL);

   /* Connect to database */
   if (!mysql_real_connect(conn, server,
         user, password, database, 0, NULL, 0)) {
      fprintf(stderr, "%s\n", mysql_error(conn));
      exit(1);
   }

  /*
   if (mysql_query(conn, "INSERT INTO CERINTE VALUES (01,'Test1','Test1')")) {
      fprintf(stderr, "%s\n", mysql_error(conn));
      exit(1);
   }
  */

    /* send SQL query */
   if (mysql_query(conn, "SELECT * FROM CERINTE")) {
      fprintf(stderr, "%s\n", mysql_error(conn));
      exit(1);
   }

   res = mysql_use_result(conn);

   /* output table name */
   printf("MySQL Tables in mysql database:\n");
   while ((row = mysql_fetch_row(res)) != NULL)
      {
	printf("%s %s %s \n", row[0], row[1], row[2]);
	}
   /* close connection */
   mysql_free_result(res);
   mysql_close(conn);
}
