%{

#include <stdio.h>
#include <string.h>
extern FILE* yyin;
extern char* yytext;
extern int yylineno;

 
int yyerror(char * s){
printf("eroare: %s la linia:%d\n",s,yylineno);
}

 
int yywrap()
{
        return 1;
} 
  
int main(int argc, char** argv){
yyparse();
} 

%}


%union 
{
	int nmr;
	char* str;
}

%token <str> SIR
%token <nmr> NR

%type <str> opsr
%type <nmr> opnr

%nonassoc '|' '(' ')' EQ
%left '-' '+'
%left '#' '`' '*'
%left '~'


%%

start:
	start opsr ';'	{ printf("Rez: %s\n",$2);}
	|start opnr ';'	{ printf("Rez: %d\n",$2);}
	|opsr ';'	{ printf("Rez: %s\n",$1);}
	|opnr ';'	{ printf("Rez: %d\n",$1);}
	;


opsr:
	opsr '+' opsr
	{
		$$=strcat($1, $3);
	}
	|opsr '-' opsr
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
	|opsr '*' opnr
	{
			char* s = malloc(sizeof(char)*strlen($1)*$3);
			int i;
			for(i=0;i<$3;i++)
				strcat(s,$1);
			$$=s;
	}
	|opsr '#' opnr
	{
		if(strlen($1)>=$3)
		{
			char *s = malloc(sizeof(char)*$3+1);
			strncpy (s,$1+strlen($1)-$3,$3);
			s[$3]='\0';
			$$=s;
		}
		else
			$$=$1;
	}
	|opnr '`' opsr
	{
		char *s = malloc(sizeof(char)*$1+1);
		if(strlen($3)>=$1)
		{
			strncpy (s,$3,$1);
			s[$1]='\0';
			$$=s;
		}
		else
			$$=$3;
	}
	|'~' opsr
	{
   		int i, j, len;
    		char temp;
    		char *ptr=NULL;
    		i=j=len=temp=0;

    		len=strlen($2);
    		ptr=malloc(sizeof(char)*(len+1));
    		ptr=strcpy(ptr,$2);
    		for (i=0, j=len-1; i<=j; i++, j--)
    		{
        		temp=ptr[i];
        		ptr[i]=ptr[j];
        		ptr[j]=temp;
    		}
    		$$=ptr;
	}
	|'(' opsr ')'
	{
		$$=$2;
	}
	|SIR
	{
		$$=$1;
	}
	;

opnr:
	'(' opnr ')'
	{
		$$=$2;
	}
	|opsr EQ opsr
	{
		$$=!strcmp($1,$3);
	}
	|'|' opsr '|'
	{
		$$=strlen($2);
	}
	|NR
	{
		$$=$1;
	}
	;



%%