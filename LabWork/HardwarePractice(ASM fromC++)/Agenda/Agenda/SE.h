#pragma once
#include "cagenda.h"
#include "afxwin.h"

// SE dialog

class SE : public CDialogEx
{
	DECLARE_DYNAMIC(SE)

public:
	SE(CWnd* pParent = NULL);   // standard constructor
	virtual ~SE();

// Dialog Data
	enum { IDD = IDD_DIALOG5 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
	char index[15];
	CEdit seid;
	CEdit senume;
	CEdit seprenume;
	CEdit seadresa;
	CEdit segrup;
	afx_msg void OnBnClickedButton1();
};
