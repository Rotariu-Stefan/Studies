// LA.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "LA.h"
#include "afxdialogex.h"


// LA dialog

IMPLEMENT_DYNAMIC(LA, CDialogEx)

LA::LA(CWnd* pParent /*=NULL*/)
	: CDialogEx(LA::IDD, pParent)
{
//	ag=cagenda();
}

LA::~LA()
{
}

void LA::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(LA, CDialogEx)
	ON_BN_CLICKED(IDOK, &LA::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &LA::OnBnClickedCancel)
END_MESSAGE_MAP()


// LA message handlers


void LA::OnBnClickedOk()
{
	ag.erase();
	ag.loadf();
	MessageBox("LoadAG->OK");
	CDialogEx::OnOK();
}


void LA::OnBnClickedCancel()
{
	MessageBox("LoadAG->Cancel");
	CDialogEx::OnCancel();
}
