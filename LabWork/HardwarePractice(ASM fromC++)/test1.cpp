// test1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include<iostream>

using namespace std;
int maxSir()
{
/*	int m=0,i=0;
	while(s[i])
	{
		if(s[i]>m)
			m=s[i];
		i++;
	}
	return m; */
	_asm
	{
		mov dword ptr[ecx],0
		mov dword ptr[ebx],0


for1:	mov dword ptr[edx],ebx
		add dword ptr[edx],5
		mov dword ptr[ecx+4*ebx],edx
		inc ebx
		cmp dword ptr[ebx],10
		je lolol
		jmp for1

lolol:
		mov dword ptr[eax],0
		mov dword ptr[ebx],0
//		mov ecx,[ebp+8]

bucla:	mov edx,dword ptr[ecx+4*ebx]
		cmp dword ptr[edx],0
		je iesire
		cmp edx,dword ptr[eax]
		jb alocare

alocare:	mov dword ptr[eax],edx
			inc ebx
			jmp bucla
iesire:
	}
}


int _tmain(int argc, _TCHAR* argv[])
{
/*	int *s;
	s=new int(10);
	for(int i=0;i<10;i++)
		s[i]=i+5;
	printf("Maximul este: %d\n",maxSir(s));*/




	printf("Maximul este: %d\n",maxSir());


	return 0;
}

