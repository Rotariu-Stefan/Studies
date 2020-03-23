// multimi.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include<iostream>

//using namespace std;

class Multime
{
	int MaxEl;
	int Count;
	int *List;
public:
	Multime(int);
	bool Push(int);
	int Len();
	void Intersect(Multime*, Multime*);
	int Get(int);
};

Multime::Multime(int max)
{
	_asm
	{
	mov eax,[ebp+8]
	mov dword ptr[ecx],eax
	mov dword ptr[ecx+4],0
	}
	List = (int *) malloc(sizeof(int)* max);
}
int Multime::Len()
{
//	return Count;
	_asm
	{
	mov eax,[ecx+4]
	}
}
int Multime::Get(int index)
{
//	return List[index];
	_asm
	{
		mov ebx,[ebp+8]
		mov edx,[ecx+8]
		mov eax,[edx+4*ebx]
	}

}
bool Multime::Push(int a)
{
	if(Count==MaxEl)
		return false;
	else
	{
		List[Count]=a;
		Count++;
		return true;
	}
	cmp ecx,
};

void Multime::Intersect(Multime *a,Multime *b)
{
	for(int i=0;i<a->Len();i++)
		for(int j=0;j<b->Len();j++)
			if(a->Get(i)==b->Get(j))
				this->Push(a->Get(i));
};


int _tmain(int argc, _TCHAR* argv[])
{
	Multime* m1=new Multime(5);
	Multime* m2=new Multime(5);
	Multime* m3=new Multime(5);
	m1->Push(1);m1->Push(2);m1->Push(3);m1->Push(4);m1->Push(5);
	m2->Push(3);m2->Push(4);m2->Push(11);
	m3->Intersect(m1,m2);
	printf("%d si %d",m3->Get(0),m3->Get(1));


		return 0;
}

