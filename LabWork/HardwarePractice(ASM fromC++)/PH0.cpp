#include<iostream>

using namespace std;

int max(int a,int b,int c)
{
	_asm
	{
	mov ebx,b
	mov ecx,c

	cmp a,ebx
	jg amax
	jmp bmax

amax: mov eax,a
	  cmp a,ecx
	  jg exout

bmax: mov eax,b
	  cmp ebx,c
	  jg exout
	  mov eax,c

exout:
	}
}

int main()
{
	cout<<max(123,45,55);
}