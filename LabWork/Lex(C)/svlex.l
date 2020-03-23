%{
#include <stdio.h>
#include <string.h>
#include "y.tab.h"
extern YYSTYPE yylval;

%}

litera [a-zA-Z]
cifra [0-9]
cifre ({cifra})+
semn [+-]

ident ({litera})({litera}|{cifra}|[_])*
numar {cifre}|({semn})({cifre})
operB [+-/*<>=]
operC [<>]|(<>)|(<=)|(>=)
operL and|or|xor
parantz [\(\)\[\]\{\}]
sir (\".+\")
coment ((\/\/.*)\n)|(\/\*(.|\n)*\*\/)

tipnrvar int|int2|real|real2
tipsrvar char|string|date|file
tipbvar bool
felvar static|public|private|limited
library (stdio\.sv)|(manstr\.sv)|(manfile\.sv)
cuzrez checkpoint|goto|fread|fwrite

paramdec ({tipnrvar}{ident})|({tipsrvar}{ident})
paramapel {ident}|{numar}|{sir}            
%%

[0-9]+     yylval=atoi(yytext); return NR;
{sir}      yylval=strdup(yytext); return SIR;
(true)|(false) yylval=strdup(yytext); return VBL;
{operL}    return OPL;
{library}  return LIB;

{felvar}   return FELTIP;
{tipnrvar} yylval=strdup(yytext); return TIPNR;
{tipsrvar} yylval=strdup(yytext); return TIPSR;
{tipbvar}  yylval=strdup(yytext); return TIPB;


load	    return LOAD;
if          return IF;
else	    return ELSE;
while       return WHILE;
loop	    return LOOP;
for	        return FOR;
read	    return READ;
write       return WRITE;
checkpoint  return CHECK;
goto        return GOTO;
exit	    return EXIT;
declare	return DECLARE;
strconv	return STRCONV;

"n_"{ident} yylval=strdup(yytext); return NIDENT;
"s_"{ident} yylval=strdup(yytext); return SIDENT;
"b_"{ident} yylval=strdup(yytext); return BIDENT;
{ident}    yylval=strdup(yytext); return IDENT;


\n
[ \t]+
{coment}
.               return (int) yytext[0];
%%
