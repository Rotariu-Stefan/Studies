#pragma once

class intrare
{
private:
	char id[15];
	char nume[10];
	char prenume[10];
	char adresa[20];
	char grup[10];
public:
	intrare()
	{
		strcpy(id,"0");
		strcpy(nume,"0");
		strcpy(prenume,"0");
		strcpy(adresa,"0");
		strcpy(grup,"0");
	}
	intrare(char* i,char* n,char* p,char* a,char* g)
	{
		strcpy(id,i);
		strcpy(nume,n);
		strcpy(prenume,p);
		strcpy(adresa,a);
		strcpy(grup,g);
	}
	void setintrare(char* i,char* n,char* p,char* a,char* g)
	{
		strcpy(id,i);
		strcpy(nume,n);
		strcpy(prenume,p);
		strcpy(adresa,a);
		strcpy(grup,g);
	}
	char* getid()
	{
		return id;
	}
	char* getnume()
	{
		return nume;
	}
	char* getprenume()
	{
		return prenume;
	}
	char* getadresa()
	{
		return adresa;
	}
	char* getgrup()
	{
		return grup;
	}
	char* retintrare()
	{
		char* s=new(char[sizeof(intrare)]);
		sprintf(s,"%s %s %s %s %s",id,nume,prenume,adresa,grup);
		return s;
	}
	void operator=(intrare x)
	{
		setintrare(x.getid(),x.getnume(),x.getprenume(),x.getadresa(),x.getgrup());
	}
};
