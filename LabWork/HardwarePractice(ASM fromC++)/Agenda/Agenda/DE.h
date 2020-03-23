#pragma once
#include "cagenda.h"
#include "afxwin.h"

// DE dialog

class DE : public CDialogEx
{
	DECLARE_DYNAMIC(DE)

public:
	DE(CWnd* pParent = NULL);   // standard constructor
	virtual ~DE();

// Dialog Data
	enum { IDD = IDD_DIALOG6 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
	char index[15];
	afx_msg void OnBnClickedButton1();
	CEdit deid;
	CEdit denume;
	CEdit deprenume;
	CEdit deadresa;
	CEdit degrup;
};
