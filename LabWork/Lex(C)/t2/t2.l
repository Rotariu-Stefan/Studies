%{

#include<stdio.h>
FILE *fin,*fout,*ffa,*ffd,*ffu;
char buffer[100];

%}

litera [a-zA-Z]
cifra [0-9]
cifre ({cifra})+
semn [+-]
tip class|struct|union

cuvrez include|auto|double|int|struct|break|else|long|switch|case|enum|register|typedef|case|char|extern|return|union|const|float|short|unsigned|continue|for|signed|void|default|goto|sizeof|volatile|do|if|static|while
sir (\".+\")|(\/\/.*)|(\/\*.*\*\/)
ident ({litera})({litera}|{cifra}|[_])*
cnum {cifre}|({semn})({cifre})
oper [+-/*<>=]|(\+\+)|(\-\-)|(\+=)|(\-=)|(\*=)|(\/=)
parantz [\(\)\[\]\{\}]




param {ident}|{cnum}
paramT {ident}|{cnum}|{sir}
dparam ({cuvrez}).({ident})

fc {ident}\(({param}(,{param})*)\)
fcT {ident}\(({paramT}(,{paramT})*)\)
fcvid {ident}\(\)

functie {fc}|{fcvid}
functieT {fcT}|{fcvid}
decfunctie {ident}\(({dparam}(,{dparam})*)\)


%%
{cuvrez} {fprintf(fout,"<font color=\"blue\">%s</font>", yytext);}
{ident} {fprintf(fout,"<font color=\"red\">%s</font>", yytext);}
{cnum} {fprintf(fout,"<font color=\"turquoise\">%s</font>", yytext);}
{sir} {fprintf(fout,"<font color=\"grey\">%s</font>", yytext);}
{oper} {fprintf(fout,"<font color=\"green\">%s</font>", yytext);}
{parantz} {fprintf(fout,"<font color=\"purple\">%s</font>", yytext);}
\n	{fprintf(fout,"<br/>");}

{functie} {fprintf(ffa,"%s\n", yytext);
		fprintf(fout,"<font color=\"green\">%s</font>", yytext);}
{decfunctie} {fprintf(ffd,"%s\n", yytext);
		fprintf(fout,"<font color=\"green\">%s</font>", yytext);}


%%

main( )
{
	if(fin=fopen("in.txt","r"))
		yyin=fin;
	else
		printf("Eroare deschidere in.txt !");
	fout=fopen("out.html","w");
	if(fout==NULL)
		printf("Eroare deschidere out.txt !");
	else
		yyout=fout;
	ffa=fopen("ffa.txt","w");
	fprintf(ffa,"Apelurile de functii:\n");
	ffd=fopen("ffd.txt","w");
	fprintf(ffd,"Declaratii de functii:\n");
	ffu=fopen("ffu.txt","w");
	fprintf(ffu,"Tipuri definite de utilizatori:\n");


	yylex( );
	fclose(fin); fclose(fout);	fclose(ffa);	fclose(ffd); fclose(ffu);


}








