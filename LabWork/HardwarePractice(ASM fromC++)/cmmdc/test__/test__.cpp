#include "stdafx.h"

class cmmdc
{
	int x,y;
public:
	cmmdc();
	void SetX(int xx);
	void SetY(int yy);
	int GetCmmdc();
};

cmmdc::cmmdc()
{
	_asm
	{
		mov dword ptr[ecx],0
		mov dword ptr[ecx+4],0
	}

}
void cmmdc::SetX(int xx)
{
	_asm
	{
		mov eax, [ebp+8]
		mov dword ptr[ecx], eax
	}
}

void cmmdc::SetY(int yy)
{
	_asm
	{
		mov eax, [ebp+8]
		mov dword ptr[ecx+4], eax
	}
}

int cmmdc::GetCmmdc()
{

 _asm
 {
	mov eax,[ecx]
	mov ebx,[ecx+4]
compar:
	cmp eax,ebx
	je iesire
	ja scad_a
	sub ebx,eax
	jmp compar
scad_a:
	sub eax,ebx		// returnu ii eax
	jmp compar
iesire:
 }

}


int _tmain(int argc, _TCHAR* argv[])
{
	cmmdc cm;

	cm.SetX(77);
	cm.SetY(33);

	printf("%d\n",cm.GetCmmdc());

	return 0;
}

