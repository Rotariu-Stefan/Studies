%{
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
%}
%union {
int nr;
char *str;
}

%start S
%token <nr>NR <str>STR
%token EQUAL

%type <str>EXP 
%type <str>STRING
%type <nr>NR_AUX

%left EQUAL
%left '+' '-'
%left '*'
%left '`' '#'

%nonassoc '|' '(' ')' 
%%

S
	:STRING {printf("RESULT => %s\n",$<str>$);}
	;
	
STRING
	:EXP '+' EXP 
		{
			char* s = malloc(sizeof(char)*(strlen($1)+strlen($3)));
            strcpy(s,$1); 
			strcat(s,$3);
			$$=s;
		}
	|EXP '-' EXP 
		{
			char *s,*s2;
			s = strstr($1,$3);
			if(s)
				{
					s2 = malloc(sizeof(char)*(strlen($1)-strlen($3)+1));
					int aux=strlen(s)-strlen($3)+1;
					int aux2=strlen($1)-strlen(s);
					strncpy(s,s+strlen($3),aux);
					s[aux] = '\0';
					strcat(s2,$1);
					strncpy(s2+aux2,s,strlen(s));
					s2[aux2+strlen(s)]='\0';
				}
			else
				{
					s2 = malloc(sizeof(char)*strlen($1));
					strcpy(s2,$1); 
				}
			$$=s2;
		}
	|EXP '*' NR_AUX 
		{
			char* s = malloc(sizeof(char)*strlen($1)*$3);
			int i;
			for(i=0;i<$3;i++)
				strcat(s,$1);
			$$=s;
		}
	|NR_AUX '*' EXP 
		{
			char* s = malloc(sizeof(char)*strlen($3)*$1);
			int i;
			for(i=0;i<$1;i++)
				strcat(s,$3);
			$$=s;
		}
	|NR_AUX '`' EXP 
		{
			char *s = malloc(sizeof(char)*$1+1);
			strncpy (s,$3,$1);
			s[$1]='\0';
			$$=s;
		}
	|EXP '#' NR_AUX 
		{
			char *s = malloc(sizeof(char)*$3+1);
			strncpy (s,$1+strlen($1)-$3,$3);
			s[$3]='\0';
			$$=s;
		}
	|EXP 
		{ 
			char* s = malloc(sizeof(char)*(strlen($1)));
            strcpy(s,$1); 
			$$=s;
		}
	;
	
EXP
	:EXP '+' EXP 
		{
			char* s = malloc(sizeof(char)*(strlen($1)+strlen($3)));
            strcpy(s,$1); 
			strcat(s,$3);
			$$=s;
		}
	|EXP '-' EXP 
		{
			char *s,*s2;
			s = strstr($1,$3);
			if(s)
				{
					s2 = malloc(sizeof(char)*(strlen($1)-strlen($3)+1));
					int aux=strlen(s)-strlen($3)+1;
					int aux2=strlen($1)-strlen(s);
					strncpy(s,s+strlen($3),aux);
					s[aux] = '\0';
					strcat(s2,$1);
					strncpy(s2+aux2,s,strlen(s));
					s2[aux2+strlen(s)]='\0';
				}
			else
				{
					s2 = malloc(sizeof(char)*strlen($1));
					strcpy(s2,$1); 
				}
			$$=s2;
		}
	|EXP '*' NR_AUX 
		{
			char* s = malloc(sizeof(char)*strlen($1)*$3);
			int i;
			for(i=0;i<$3;i++)
				strcat(s,$1);
			$$=s;
		}
	|NR_AUX '*' EXP 
		{
			char* s = malloc(sizeof(char)*strlen($3)*$1);
			int i;
			for(i=0;i<$1;i++)
				strcat(s,$3);
			$$=s;
		}		
	|NR_AUX '`' EXP 
		{
			char *s = malloc(sizeof(char)*$1+1);
			strncpy (s,$3,$1);
			s[$1]='\0';
			$$=s;
		}
	|EXP '#' NR_AUX 
		{
			char *s = malloc(sizeof(char)*$3+1);
			strncpy (s,$1+strlen($1)-$3,$3);
			s[$3]='\0';
			$$=s;
		}
	|'(' EXP ')'
		{
			char* s = malloc(sizeof(char)*(strlen($2)));
            strcpy(s,$2); 
			$$=s;
		}
	|STR{
			char* s = malloc(sizeof(char)*(strlen($1)));
            strcpy(s,$1); 
			$$=s;
		}
	|NR_AUX 
		{
			char* s = malloc(sizeof(char)*100);
			sprintf(s,"%d",$1);			
			$$=s;
		}
	;

NR_AUX
	:NR 
		{
			$$=$1;
		}
	|'|' EXP '|' 
		{
			char* s = malloc(sizeof(char)*(strlen($2)));
            strcpy(s,$2); 
			//$$=s;
			$$=strlen(s);
		}	
	|EXP EQUAL EXP 
		{
			//printf("ce plm2\n%d\n",(strcmp($1,$3)==0)?1:0);
			if(strcmp($1,$3))
				$$=0;
			else
				$$=1;
			/*
			$$=(strcmp($1,$3)==0)?1:0;
			*/
		}
	| '(' NR_AUX ')' 
		{
			$$=$2;
		}
	;
%%


int yyerror(char * s){
printf("eroare !!!");
}

 
int yywrap()
{
        return 1;
} 
  
int main(int argc, char** argv){

yyparse();
} 




