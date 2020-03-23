// IE.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "IE.h"
#include "afxdialogex.h"


// IE dialog

IMPLEMENT_DYNAMIC(IE, CDialogEx)

IE::IE(CWnd* pParent /*=NULL*/)
	: CDialogEx(IE::IDD, pParent)
{

}

IE::~IE()
{
}

void IE::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT3, ienume);
	DDX_Control(pDX, IDC_EDIT4, ieprenume);
	DDX_Control(pDX, IDC_EDIT5, ieadresa);
	DDX_Control(pDX, IDC_EDIT6, iegrup);
	DDX_Control(pDX, IDC_EDIT1, ieid);
}


BEGIN_MESSAGE_MAP(IE, CDialogEx)
	ON_BN_CLICKED(IDOK, &IE::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &IE::OnBnClickedCancel)
END_MESSAGE_MAP()


// IE message handlers


void IE::OnBnClickedOk()
{
	char i[15],n[10],p[10],a[20],g[10];
	ieid.GetWindowTextA(i,15);
	ienume.GetWindowTextA(n,10);
	ieprenume.GetWindowTextA(p,10);
	ieadresa.GetWindowTextA(a,20);
	iegrup.GetWindowTextA(g,10);

	if(i&&n&&p&&a&&g)
		ag.addintrare(i,n,p,a,g);
	else
		MessageBox("Intrare Gresita!!");
	MessageBox("InsertE->OK");
	CDialogEx::OnOK();
}


void IE::OnBnClickedCancel()
{
	MessageBox("InserE->Cancel");
	CDialogEx::OnCancel();
}
