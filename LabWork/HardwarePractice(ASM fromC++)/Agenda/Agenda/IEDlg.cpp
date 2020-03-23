// IEDlg.cpp : implementation file
//

#include "stdafx.h"
#include "Agenda.h"
#include "IEDlg.h"
#include "afxdialogex.h"


// IEDlg dialog

IMPLEMENT_DYNAMIC(IEDlg, CDialogEx)

IEDlg::IEDlg(CWnd* pParent /*=NULL*/)
	: CDialogEx(IEDlg::IDD, pParent)
{

}

IEDlg::~IEDlg()
{
}

void IEDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialogEx::DoDataExchange(pDX);
}


BEGIN_MESSAGE_MAP(IEDlg, CDialogEx)
END_MESSAGE_MAP()
// IEDlg message handlers
