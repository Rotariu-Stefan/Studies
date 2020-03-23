// test0.cpp : Defines the entry point for the console application.
#include "stdafx.h"
#include<iostream>

using namespace std;

class Multime
{
	int MaxEl;
	int Count;
	int *List;
public:
	Multime(int);
	int Len();
	bool Push(int);
	int Get(int);
	void Inter(Multime*, Multime*);
	void Write();

};

Multime::Multime(int a)
{
	//MaxEl=a;
	//Count=0;
	_asm
	{
		mov eax,[ebp+8]
		mov dword ptr[ecx],eax
		mov dword ptr[ecx+4],0
	}

	List=new int(a);
	//List = (int *) malloc(sizeof(int)* a);
}
int Multime::Len()
{
	//return Count;
	_asm
		mov eax,dword ptr[ecx+4]
}
bool Multime::Push(int a)
{
/*	if(Count<MaxEl)
	{
		List[Count]=a;
		Count++;
		return true;
	}
	else
	{
		cout<<"Nu mai exista loc!!";
		return false;
	}*/
	_asm
	{
		mov ebx, dword ptr[ecx+4]
		mov edx, dword ptr[ecx]
		cmp edx,ebx
		ja dabun
		jmp nubun

dabun:	mov ebx,dword ptr[ebp+8]
		mov eax,dword ptr[ecx+4]
		mov edx,[ecx+8]
		mov [edx+4*eax], ebx
		inc dword ptr[ecx+4]
		mov eax,1
		jmp exout
nubun:	mov eax,0
		jmp exout
exout:
	}
}
int Multime::Get(int a)
{
	//return List[a];
	_asm
	{
		mov ebx,[ebp+8]
		mov edx,[ecx+8]
		mov eax,[edx+4*ebx]
	}
}
void Multime::Inter(Multime *a, Multime *b)
{
	for(int i=0;i<a->Len();i++)
		for(int j=0;j<b->Len();j++)
			if(a->Get(i)==b->Get(j))
				Push(a->Get(i)); 
/*	_asm
	{
		mov ebx,[ebp+8]
		mov eax,dword ptr[ebx+4]
		mov ebx,-1
		mov esi,[ebp+8]
		mov edi,dword ptr[esi+4]
		jmp for1

for1:	cmp ebx,eax
		jg iesire
		inc ebx
		mov esi,-1
		jmp for2

for2:	cmp esi,edi
		jg for1
		inc esi

		cmp {get a},{get b}
		jne for2	
		mov [ebp+8],1	//if
		call dabun				//--pushu--
		jmp for2

iesire:
	}*/

}
void Multime::Write()
{
	cout<<"{{ ";
	for(int i=0;i<Count;i++)
		printf("%d, ",this->Get(i));
	cout<<"}}\n";
}

int _tmain(int argc, _TCHAR* argv[])
{
	Multime m1(6),m2(5),m3(5);
	m1.Push(1); m1.Push(2); m1.Push(3); m1.Push(4); m1.Push(5);
	m2.Push(4); m2.Push(5); m2.Push(1); m2.Push(11); m2.Push(12);
	m3.Inter(&m1,&m2);
	m3.Write();









	return 0;
}

