// SA.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "SA.h"
#include "afxdialogex.h"


// SA dialog

IMPLEMENT_DYNAMIC(SA, CDialogEx)

SA::SA(CWnd* pParent /*=NULL*/)
	: CDialogEx(SA::IDD, pParent)
{

}

SA::~SA()
{
}

void SA::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(SA, CDialogEx)
	ON_BN_CLICKED(IDOK, &SA::OnBnClickedOk)
	ON_BN_CLICKED(IDCANCEL, &SA::OnBnClickedCancel)
END_MESSAGE_MAP()


// SA message handlers


void SA::OnBnClickedOk()
{
	ag.savef();
	MessageBox("SaveAG->OK");
	CDialogEx::OnOK();
}


void SA::OnBnClickedCancel()
{
	MessageBox("SaveAG->Cancel");
	CDialogEx::OnCancel();
}
