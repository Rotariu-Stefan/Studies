// E.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "E.h"
#include "afxdialogex.h"


// CE dialog

IMPLEMENT_DYNAMIC(CE, CDialogEx)

CE::CE(CWnd* pParent /*=NULL*/)
	: CDialogEx(CE::IDD, pParent)
{

}

CE::~CE()
{
}

void CE::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(CE, CDialogEx)
	ON_BN_CLICKED(IDOK, &CE::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &CE::OnBnClickedCancel)
END_MESSAGE_MAP()


// CE message handlers


void CE::OnBnClickedOk()
{
	MessageBox("CancelE->OK");
	CDialogEx::OnOK();
}


void CE::OnBnClickedCancel()
{
	MessageBox("CencelE->Cancel");
	CDialogEx::OnCancel();
}
