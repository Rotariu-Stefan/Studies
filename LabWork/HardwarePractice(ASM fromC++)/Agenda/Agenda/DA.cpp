// DA.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "DA.h"
#include "afxdialogex.h"


// DA dialog

IMPLEMENT_DYNAMIC(DA, CDialogEx)

DA::DA(CWnd* pParent /*=NULL*/)
	: CDialogEx(DA::IDD, pParent)
{

}

DA::~DA()
{
}

void DA::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(DA, CDialogEx)
	ON_BN_CLICKED(IDOK, &DA::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &DA::OnBnClickedCancel)
END_MESSAGE_MAP()


// DA message handlers


void DA::OnBnClickedOk()
{
	ag.erase();
	MessageBox("DeleteAG->OK");
	CDialogEx::OnOK();
}


void DA::OnBnClickedCancel()
{
	MessageBox("DeleteAG->Cancel");
	CDialogEx::OnCancel();
}
