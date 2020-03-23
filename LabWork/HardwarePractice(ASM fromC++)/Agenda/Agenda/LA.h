#pragma once
#include "cagenda.h"

// LA dialog

class LA : public CDialogEx
{
	DECLARE_DYNAMIC(LA)

public:
	LA(CWnd* pParent = NULL);   // standard constructor
	virtual ~LA();

// Dialog Data
	enum { IDD = IDD_DIALOG1 };

protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support

	DECLARE_MESSAGE_MAP()
public:
	afx_msg void OnBnClickedOk();
	afx_msg void OnBnClickedCancel();
	cagenda ag;
};
