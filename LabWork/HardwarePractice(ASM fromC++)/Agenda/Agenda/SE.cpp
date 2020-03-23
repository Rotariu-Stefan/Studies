// SE.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "SE.h"
#include "afxdialogex.h"


// SE dialog

IMPLEMENT_DYNAMIC(SE, CDialogEx)

SE::SE(CWnd* pParent /*=NULL*/)
	: CDialogEx(SE::IDD, pParent)
{
/*	seid.SetWindowTextA(ag.getintrare(index).getid());
	senume.SetWindowTextA(ag.getintrare(index).getnume());
	seprenume.SetWindowTextA(ag.getintrare(index).getprenume());
	seadresa.SetWindowTextA(ag.getintrare(index).getadresa());
	segrup.SetWindowTextA(ag.getintrare(index).getgrup());*/
}

SE::~SE()
{
}

void SE::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, seid);
	DDX_Control(pDX, IDC_EDIT3, senume);
	DDX_Control(pDX, IDC_EDIT4, seprenume);
	DDX_Control(pDX, IDC_EDIT5, seadresa);
	DDX_Control(pDX, IDC_EDIT6, segrup);
}


BEGIN_MESSAGE_MAP(SE, CDialogEx)
	ON_BN_CLICKED(IDOK, &SE::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &SE::OnBnClickedCancel)
	ON_BN_CLICKED(IDC_BUTTON1, &SE::OnBnClickedButton1)
END_MESSAGE_MAP()


// SE message handlers


void SE::OnBnClickedOk()
{
	for(int i=0;i<ag.getcount();i++)
		if(strcmp(ag.getintrare(i).getid(),index)==0)
			ag.delintrare(i);

	char i[15],n[10],p[10],a[20],g[10];
	seid.GetWindowTextA(i,15);
	senume.GetWindowTextA(n,10);
	seprenume.GetWindowTextA(p,10);
	seadresa.GetWindowTextA(a,20);
	segrup.GetWindowTextA(g,10);

	if(i&&n&&p&&a&&g)
		ag.addintrare(i,n,p,a,g);
	else
		MessageBox("Intrare Gresita!!");
	MessageBox("SaveE->OK");
	CDialogEx::OnOK();
}


void SE::OnBnClickedCancel()
{
	MessageBox("SaveE->Cancel");
	CDialogEx::OnCancel();
}


void SE::OnBnClickedButton1()
{
	seid.SetWindowTextA(ag.getintrare(index).getid());
	senume.SetWindowTextA(ag.getintrare(index).getnume());
	seprenume.SetWindowTextA(ag.getintrare(index).getprenume());
	seadresa.SetWindowTextA(ag.getintrare(index).getadresa());
	segrup.SetWindowTextA(ag.getintrare(index).getgrup());
}
