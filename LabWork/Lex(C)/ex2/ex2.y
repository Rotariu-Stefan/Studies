%{
#include <stdio.h>
#include <string.h>
char* heater;
 
void yyerror(const char *str)
{
        fprintf(stderr,"error: %s\n",str);
}
 
int yywrap()
{
        return 1;
} 
  
main()
{
        yyparse();
} 

%}

%union 
{
        int number;
        char *string;
}

%token <number> STATE
%token <number> NUMBER
%token <string> WORD

%%
commands: /* empty */
        | commands command
        ;

command:
        heater_select
        |
        target_set
        ;

heater_select:
        TOKHEATER WORD
        {
                printf("\tSelected heater '%s'\n",$2);
                heater=$2;
        }
        ;


target_set:
        TOKTARGET TOKTEMPERATURE NUMBER
        {
                printf("\tHeater '%s' temperature set to %d\n",heater,$3);
        }
        ;


%%