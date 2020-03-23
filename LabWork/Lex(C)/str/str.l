%{
#include <stdio.h>
#include <string.h>
#include "y.tab.h"


%}

%%

"==" 	   {return EQ;}
([0-9]+)  {	yylval.nmr=atoi(yytext);	  	return NR;}
[a-zA-Z]+ {
			yylval.str = (char*)malloc( strlen(yytext) + 1); 
			strcpy(yylval.str,yytext);
			return SIR;
			}

\n
[ \t]+
.               return (int) yytext[0];
%%