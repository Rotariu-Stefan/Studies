%{
#include<stdio.h>
#include<string.h>
#include "y.tab.h"
%}
%%
[1-9][0-9]* {yylval.nr=atoi(yytext); return NR;}
[a-zA-Z]+ {
			yylval.str = (char*)malloc( strlen(yytext) + 1); 
			strcpy(yylval.str,yytext);
			return STR;
			}
"==" {return EQUAL;}
[ \t] ;
\n {return 0;}
. {return yytext[0];}
%%