// DE.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "DE.h"
#include "afxdialogex.h"


// DE dialog

IMPLEMENT_DYNAMIC(DE, CDialogEx)

DE::DE(CWnd* pParent /*=NULL*/)
	: CDialogEx(DE::IDD, pParent)
{
/*	seid.SetWindowTextA(ag.getintrare(index).getid());
	senume.SetWindowTextA(ag.getintrare(index).getnume());
	seprenume.SetWindowTextA(ag.getintrare(index).getprenume());
	seadresa.SetWindowTextA(ag.getintrare(index).getadresa());
	segrup.SetWindowTextA(ag.getintrare(index).getgrup());*/
}

DE::~DE()
{
}

void DE::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
	DDX_Control(pDX, IDC_EDIT1, deid);
	DDX_Control(pDX, IDC_EDIT3, denume);
	DDX_Control(pDX, IDC_EDIT4, deprenume);
	DDX_Control(pDX, IDC_EDIT5, deadresa);
	DDX_Control(pDX, IDC_EDIT6, degrup);
}


BEGIN_MESSAGE_MAP(DE, CDialogEx)
	ON_BN_CLICKED(IDOK, &DE::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &DE::OnBnClickedCancel)
	ON_BN_CLICKED(IDC_BUTTON1, &DE::OnBnClickedButton1)
END_MESSAGE_MAP()


// DE message handlers


void DE::OnBnClickedOk()
{
	for(int i=0;i<ag.getcount();i++)
		if(strcmp(ag.getintrare(i).getid(),index)==0)
			ag.delintrare(i);
	MessageBox("DeleteE->OK");
	CDialogEx::OnOK();
}


void DE::OnBnClickedCancel()
{
	MessageBox("DeleteE->Cancel");
	CDialogEx::OnCancel();
}


void DE::OnBnClickedButton1()
{
	deid.SetWindowTextA(ag.getintrare(index).getid());
	denume.SetWindowTextA(ag.getintrare(index).getnume());
	deprenume.SetWindowTextA(ag.getintrare(index).getprenume());
	deadresa.SetWindowTextA(ag.getintrare(index).getadresa());
	degrup.SetWindowTextA(ag.getintrare(index).getgrup());
}
