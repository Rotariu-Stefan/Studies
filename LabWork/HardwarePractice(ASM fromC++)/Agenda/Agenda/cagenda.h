#pragma once
#include "intrare.h"
class cagenda
{
private:
	intrare x[20];
	int count;
public:
	cagenda()
	{
		count=0;
	}
	void addintrare(char* i,char* n,char* p,char* a,char* g)
	{
		x[count].setintrare(i,n,p,a,g);
		count++;
	}
	void addintrare(intrare i)
	{
		x[count]=i;
		count++;
	}
	intrare getintrare(int c)
	{
		return x[c];
	}
	intrare getintrare(char* pid)
	{
		for(int i=0;i<count;i++)
			if(strcmp(x[i].getid(),pid)==0)
				return x[i];
		return x[count];
	}
	int getcount()
	{
		return count;
	}
	char* retintrare(int c)
	{
		return x[c].retintrare();
	}
	void delintrare(int c)
	{
		for(int i=c;i<count;i++)
			x[i]=x[i+1];
		count--;
	}
	void erase()
	{
		while(count)
			delintrare(0);
	}
	void operator=(cagenda y)
	{
		count=y.getcount();
		for(int i=0;i<count;i++)
			x[i]=y.getintrare(i);
	}
	void savef()
	{
		FILE* ff=fopen("ag11.txt","w");
		int i=0;
		for(i=0;i<count-1;i++)
			fprintf(ff,"%s\n",x[i].retintrare());
		fprintf(ff,"%s",x[i].retintrare());
		fclose(ff);
	}
	void loadf()
	{
		FILE* ff=fopen("ag11.txt","r");
		char i[15],n[10],p[10],a[20],g[10];
		while(!feof(ff))
		{
		fscanf(ff,"%s %s %s %s %s",i,n,p,a,g);
		addintrare(i,n,p,a,g);
		}
		fclose(ff);

	}
	char* view()
	{
		char c[1000];
		strcpy(c,"Na:");
		for(int i=0;i<count;i++)
		{
			strcat(c,retintrare(i));
			strcat(c,"\n");
		}
		return c;
	}
};