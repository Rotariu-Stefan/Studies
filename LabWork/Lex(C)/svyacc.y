%{
#include <stdio.h>
#include <string.h>
extern FILE* yyin;
extern char* yytext;
extern int yylineno;
#define YYSTYPE char *


 
int yyerror(char * s){
printf("eroare: %s la linia:%d\n",s,yylineno);
}

 
int yywrap()
{
        return 1;
} 
  
int main(int argc, char** argv){
yyin=fopen("sv.txt","r");
yyparse();
} 

%}
%token FELTIP LIB LOAD NR SIR TIPNR TIPSR IDENT VBL OPL READ WRITE FOR WHILE LOOP IF ELSE CHECK GOTO EXIT TIPB NIDENT SIDENT BIDENT DECLARE STRCONV

%left OPL
%left '-' '+'
%left '*' '/'
%left '%'

%%
start:
	comm {printf("program corect!");}
	;
comm:
	comanda ';'
	|comm comanda ';'
	;
comanda:
	v_decl_atrib
	|lib_load
	|operatie_nr
	|operatie_sr
	|operatie_bool
	|cond
	|c_if
	|c_loop
	|c_while
	|c_for
	|c_read
	|c_write
	|c_check
	|c_goto
	|c_exit
	|function_decl
	|function_apel
	|c_declare
	;

v_decl_atrib:
	FELTIP decl_atrib
	|decl_atrib
	;
decl_atrib:
	TIPNR NIDENT
	|TIPNR vecnr
	|TIPSR SIDENT 
	|TIPSR vecsr
	|TIPB BIDENT
	|TIPB vecb
	|TIPNR NIDENT '=' opnr
	|TIPSR SIDENT '=' opsr
	|TIPB BIDENT '=' cond
	;


lib_load:
	LOAD '<' LIB '>'
	;

operatie_bool:
	BIDENT '=' cond
	|vecb '=' cond
	;
operatie_nr:
	NIDENT '=' opnr
	|vecnr '=' opnr
	;
opnr:
	opnr '+' opnr
	|opnr '-' opnr
	|opnr '/' opnr
	|opnr '*' opnr
	|opnr '%' opnr
	|'(' opnr ')'
	|vecnr
	|iNR
	|function_apel
	;
vecnr:	NIDENT '[' opnr ']' ;

iNR:
	NR|NIDENT;

operatie_sr:
	SIDENT '=' opsr
	|vecsr '=' opsr
	;
opsr:
	opsr '|' iSIR
	|vecsr
	|iSIR
	;
vecsr:	SIDENT '[' opnr ']' ;

iSIR:
	SIR|SIDENT
	|STRCONV '(' opnr ')'
	;

vecb: BIDENT '[' opnr ']' ;

cond:
	cond OPL cond
	|'!' cd
	|cd
	;
cd:
	iNR '>' iNR
	|iNR '<' iNR
	|iNR '<' '=' iNR
	|iNR '>' '=' iNR
	|iNR ':' '=' iNR
	|iNR '<' '>' iNR
	|'(' cd ')'
	|BIDENT
	|vecb
	|VBL
	;
c_if:
	IF '(' cond ')' block c_else
	|IF '(' cond ')' block
	;
c_else:
	ELSE c_if
	|ELSE block
	;
c_loop:
	LOOP '(' cond ')' block
	;
c_while:
	WHILE '(' cond ',' iNR ')' block
 	|WHILE '(' cond ')' block
	;
c_for:
	FOR '(' decl_atrib ';' cond ';' operatie_nr ')' block
	|FOR '(' operatie_nr ';' cond ';' operatie_nr ')' block
	;
c_read:
	READ '(' lista_pd ')'
	;
lista_pd:
	|lista_pd ',' parapel
	|parapel
	;
parapel:
	TIPNR NIDENT
	|TIPSR SIDENT
	|TIPB BIDENT
	|IDENT
	;
c_write:
	WRITE '(' opsr ')'
	;
c_check:
	CHECK '<' NR '>'
	;
c_goto:
	GOTO '<' NR '>'
	;
c_exit:
	EXIT '(' NR ')'
	;

function_decl:
	TIPNR IDENT '(' lista_pd ')' block
	|TIPSR IDENT '(' lista_pd ')' block
	|IDENT '(' lista_pd ')' block
	;

function_apel:
	IDENT '(' lista_pa ')'
	;
lista_pa:
	 lista_pa ',' opnr
	|lista_pa ',' opsr
	|opnr
	|opsr

block:
	'{' '}'
	|'{' comm '}'
	;

c_declare:
	DECLARE '(' NIDENT ')'
	|DECLARE '(' SIDENT ')'
	|DECLARE '(' BIDENT ')'
	;
%%
